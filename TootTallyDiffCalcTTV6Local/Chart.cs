using Syncfusion.Windows.Forms;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using static TootTallyDiffCalcTTV6Local.ChartPerformances;

namespace TootTallyDiffCalcTTV6Local
{
    public struct Chart
    {
        public float[][] notes;
        public Note[] notesArray;
        public float tempo;
        public string trackRef;
        public string name;
        public string shortName;
        public float maxScore;
        public float gameMaxScore;

        public Leaderboard leaderboard;

        public ChartPerformances performances;
        public List<RatingCriterias.RatingError> ratingErrors;

        public TimeSpan calculationTime, criteriaCalculationTime;
        public int noteCount;
        public float songLength;

        public void OnDeserialize()
        {
            notesArray = new Note[notes.Length + 1];
            var noteCount = 0;
            notesArray[0] = new Note(0, 0, .015f, 0, 0, 0, false);
            var sortedNotes = notes.OrderBy(x => x[0]).ToArray();
            for (int i = 0; i < sortedNotes.Length; i++)
            {
                float length = sortedNotes[i][1];
                if (length <= 0)//minLength only applies if the note is less or equal to 0 beats, else it keeps its "lower than minimum" length
                    length = 0.015f;
                bool isSlider = i + 1 < sortedNotes.Length && IsSlider(sortedNotes[i], sortedNotes[i + 1]);
                if (i == 0 && !isSlider)
                    noteCount++;
                notesArray[i + 1] = new Note(i + 1, BeatToSeconds2(sortedNotes[i][0], tempo), BeatToSeconds2(length, tempo), sortedNotes[i][2], sortedNotes[i][3], sortedNotes[i][4], isSlider);
            }

            this.noteCount = noteCount;
            CalcScores();
            if (notesArray.Length > 2)
                songLength = notesArray.Last().position - notesArray[1].position;
            if (songLength < 1) songLength = 1;

            performances = new ChartPerformances(this);

            Stopwatch stopwatch = Stopwatch.StartNew();
            ratingErrors = RatingCriterias.GetRatingErrors(this);
            stopwatch.Stop();
            criteriaCalculationTime = stopwatch.Elapsed;
            notes = null;
        }

        public static float GetLength(float length) => Math.Clamp(length, .2f, 5f) * 8f + 10f;

        public void CalcScores()
        {
            maxScore = 0;
            gameMaxScore = 0;
            var noteCount = 0;
            for (int i = 0; i < notes.Length; i++)
            {
                var length = notes[i][1];
                while (i + 1 < notes.Length && notes[i][0] + notes[i][1] + .025f >= notes[i + 1][0])
                {
                    length += notes[i + 1][1];
                    i++;
                }
                var champBonus = noteCount > 23 ? 1.5d : 0d;
                var realCoefficient = (Math.Min(noteCount, 10) + champBonus) * 0.1d + 1d;
                var clampedLength = GetLength(length);
                var noteScore = (int)(Math.Floor((float)((double)clampedLength * 100d * realCoefficient)) * 10f);
                maxScore += noteScore;
                gameMaxScore += (int)Math.Floor(Math.Floor(clampedLength * 100f * 1.315f) * 10f);
                noteCount++;
            }
        }

        public void CalcPerformances()
        {
            performances = new ChartPerformances(this);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < Utils.GAME_SPEED.Length; i++)
            {
                performances.CalculatePerformance(i);
                performances.CalculateAnalytics(i);
                performances.CalculateRatings(i);
            }
            stopwatch.Stop();
            calculationTime = stopwatch.Elapsed;
        }

        public void CalcLeaderboardTT()
        {
            if (leaderboard == null || leaderboard.results.Count == 0) return;
            for (int i = 0; i < leaderboard.results.Count; i++)
                CalculateScoreTT(leaderboard.results[i]);
        }

        // between 0.5f to 2f
        //public float GetBaseTT(float speed) => Utils.CalculateBaseTT(GetDiffRating(Math.Clamp(speed, 0.5f, 2f)));

        //Returns the lerped star rating
        //public float GetDiffRating(float speed) => performances.GetDiffRating(Math.Clamp(speed, 0.5f, 2f));

        public float GetDynamicDiffRating(float speed, int hitCount, string[] modifiers = null) => performances.GetDynamicDiffRating(hitCount, speed, modifiers);

        //public float GetLerpedStarRating(float speed) => performances.GetDiffRating(Math.Clamp(speed, 0.5f, 2f));

        public float GetAimPerformance(float speed) => performances.aimAnalyticsArray[SpeedToIndex(speed)].perfWeightedAverage;
        public float GetTapPerformance(float speed) => performances.tapAnalyticsArray[SpeedToIndex(speed)].perfWeightedAverage;

        public float GetStarRating(float speed) => performances.starRatingDict[SpeedToIndex(speed)];

        public int SpeedToIndex(float speed) => (int)((Math.Clamp(speed, 0.5f, 2f) - 0.5f) / .25f);

        public void CalculateScoreTT(Leaderboard.ScoreDataFromDB score)
        {
            var percent = score.percentage / 100f;
            score.tt = percent * GetDynamicDiffRating(score.replay_speed, score.GetHitCount, score.modifiers);
        }

        public static float BeatToSeconds2(float beat, float bpm) => 60f / bpm * beat;


        public static int GetConvertionVersion(ReplayData replay)
        {
            if (replay.version == "0.0.0")
                return replay.notedata.First().Length >= 6 ? 0 : -1;
            else
                return string.Compare(replay.version, "2.0.0") < 0 ? 1 : 2;
        }

        public ReplayData TryConvertReplay(ReplayData replay)
        {
            var id = GetConvertionVersion(replay);
            if (id == -1)
            {
                Console.WriteLine($"Replay {replay.uuid} cannot be converted.");
                return replay;
            }
            else
                return id == 0 || id == 1 ? ConvertReplayV1(replay) : ConvertReplayV2(replay);
        }


        public ReplayData ConvertReplayV2(ReplayData replay)
        {
            bool wasSlider = false;
            bool releasedBetweenNotes;
            int currentScore = 0;
            float health = 0; // 0 to 100
            int combo = 0;
            int highestCombo = 0;
            int multiplier = 0; // 0 to 10
            int[] noteTally = new int[5];

            List<dynamic[]> convertedNoteData = new List<dynamic[]>();
            float[] nextNote = null;
            //Loop through all the notes in a chart
            for (int i = 0; i < notes.Length; i++)
            {
                wasSlider = false;
                releasedBetweenNotes = (int)replay.notedata[i][1] == 1;
                float[] currNote = notes[i];
                if (i + 1 < notes.Length)
                    nextNote = notes[i + 1];
                List<LengthAccPair> noteLengths = new List<LengthAccPair>()
                {
                    new LengthAccPair(currNote[1], (float)replay.notedata[i][0])
                };

                //Scroll forward until the next note is no longer a slider
                while (i + 1 < notes.Length && nextNote != null && IsSlider(currNote, nextNote))
                {
                    wasSlider = true;
                    currNote = notes[++i];
                    noteLengths.Add(new LengthAccPair(currNote[1], (float)replay.notedata[i][0])); //Create note length and note acc pair to weight later
                    if (i + 1 >= notes.Length)
                        break;
                    nextNote = notes[i + 1];
                }

                float noteAcc = 0f;
                float totalLength = 0f;
                if (wasSlider)
                {
                    //Get total length of all slider bodies
                    totalLength = noteLengths.Select(x => x.length).Sum();
                    for (int j = 0; j < noteLengths.Count; j++)
                        noteAcc += noteLengths[j].acc * (noteLengths[j].length / totalLength); //Length weighted acc sum of all slider bodies
                }
                else
                {
                    //If its not a slider, just take the acc and length of it
                    noteAcc = (float)replay.notedata[i][0];
                    totalLength = currNote[1];
                }

                //Calc the score before doing the combo and health because fucking base game logic is MIND BLOWING I know
                currentScore += GetScore(noteAcc, totalLength, multiplier, health == 100);

                //Calc new health
                var healthDiff = releasedBetweenNotes ? GetHealthDiff(noteAcc) : -15f;

                if (health == 100 && healthDiff < 0)
                    health = 0;
                else if (health != 100)
                    health += healthDiff;
                health = Math.Clamp(health, 0, 100);

                //Get the note tally
                int tally = 0;
                if (noteAcc > 95f) tally = 4;
                else if (noteAcc > 88f) tally = 3;
                else if (noteAcc > 79f) tally = 2;
                else if (noteAcc > 70f) tally = 1;
                noteTally[4 - tally]++;
                //Only increase combo if you get more than 79% acc + update highest if needed
                if (tally > 2 && releasedBetweenNotes)
                {
                    if (++combo > highestCombo)
                        highestCombo = combo;
                }
                else
                    combo = 0;

                multiplier = Math.Min(combo, 10);

                convertedNoteData.Add(new dynamic[9]
                {
                    noteAcc,
                    releasedBetweenNotes ? 1 : 0,
                    i,
                    combo,
                    multiplier,
                    currentScore,
                    health,
                    highestCombo,
                    tally
                });
            }

            replay.notedata = convertedNoteData;
            replay.finalnotetallies = noteTally;
            replay.finalscore = convertedNoteData.Last()[5];
            replay.maxcombo = highestCombo;
            replay.version = "2.0.9";

            return replay;
        }

        public ReplayData ConvertReplayV1(ReplayData replay)
        {
            bool wasSlider = false;
            bool releasedBetweenNotes;
            int currentScore = 0;
            float health = 0; // 0 to 100
            float previousHealth = 0;
            int combo = 0;
            int highestCombo = 0;
            int multiplier = 0; // 0 to 10
            int[] noteTally = new int[5];

            List<dynamic[]> convertedNoteData = new List<dynamic[]>();
            float[] nextNote = null;
            //Loop through all the notes in a chart
            for (int i = 0; i < notes.Length; i++)
            {
                wasSlider = false;
                var replayHealth = (int)replay.notedata[i][3];
                releasedBetweenNotes = !(replayHealth < previousHealth && ((float)replay.notedata[i][5] / 1000f) > 79f);
                previousHealth = replayHealth;

                float[] currNote = notes[i];
                if (i + 1 < notes.Length)
                    nextNote = notes[i + 1];
                List<LengthAccPair> noteLengths = new List<LengthAccPair>
                {
                    new LengthAccPair(currNote[1], (float)replay.notedata[i][5] / 1000f)
                };

                //Scroll forward until the next note is no longer a slider
                while (i + 1 < notes.Length && nextNote != null && IsSlider(currNote, nextNote))
                {
                    wasSlider = true;
                    currNote = notes[++i];
                    noteLengths.Add(new LengthAccPair(currNote[1], (float)replay.notedata[i][5] / 1000f)); //Create note length and note acc pair to weight later
                    if (i + 1 >= notes.Length)
                        break;
                    nextNote = notes[i + 1];
                }

                float noteAcc = 0f;
                float totalLength = 0f;
                if (wasSlider)
                {
                    //Get total length of all slider bodies
                    totalLength = noteLengths.Select(x => x.length).Sum();
                    for (int j = 0; j < noteLengths.Count; j++)
                        noteAcc += noteLengths[j].acc * (noteLengths[j].length / totalLength); //Length weighted acc sum of all slider bodies
                }
                else
                {
                    //If its not a slider, just take the acc and length of it
                    noteAcc = (float)replay.notedata[i][5] / 1000f;
                    totalLength = currNote[1];
                }

                //Calc the score before doing the combo and health because fucking base game logic is MIND BLOWING I know
                currentScore += GetScore(noteAcc, totalLength, multiplier, health == 100);

                //Calc new health
                var healthDiff = releasedBetweenNotes ? GetHealthDiff(noteAcc) : -15f;

                if (health == 100 && healthDiff < 0)
                    health = 0;
                else if (health != 100)
                    health += healthDiff;
                health = Math.Clamp(health, 0, 100);

                //Get the note tally
                int tally = 0;
                if (noteAcc > 95f) tally = 4;
                else if (noteAcc > 88f) tally = 3;
                else if (noteAcc > 79f) tally = 2;
                else if (noteAcc > 70f) tally = 1;
                noteTally[4 - tally]++;
                //Only increase combo if you get more than 79% acc + update highest if needed
                if (tally > 2 && releasedBetweenNotes)
                {
                    if (++combo > highestCombo)
                        highestCombo = combo;
                }
                else
                    combo = 0;

                multiplier = Math.Min(combo, 10);
                convertedNoteData.Add(new dynamic[9]
                {
                    i,
                    currentScore,
                    multiplier,
                    (int)health,
                    tally,
                    (int)(noteAcc * 1000f),
                    combo,
                    releasedBetweenNotes ? 1 : 0,
                    highestCombo
                });
            }

            replay.notedata = convertedNoteData;
            replay.finalnotetallies = noteTally;
            replay.finalscore = convertedNoteData.Last()[1]; //Supposed to be [1]
            replay.maxcombo = highestCombo;
            replay.version = "1.0.9";

            return replay;
        }

        public static bool IsSlider(float[] currNote, float[] nextNote) => currNote[0] + currNote[1] + .025f >= nextNote[0];
        public static float GetHealthDiff(float acc) => Math.Clamp((acc - 79f) * 0.2193f, -15f, 4.34f);
        public static int GetScore(float acc, float totalLength, float mult, bool champ)
        {
            var baseScore = Math.Clamp(totalLength, 0.2f, 5f) * 8f + 10f;
            return (int)Math.Floor(baseScore * acc * ((mult + (champ ? 1.5f : 0f)) * .1f + 1f)) * 10;
        }

        public void Dispose()
        {
            notes = null;
            notesArray = null;
            ratingErrors?.Clear();
            performances.Dispose();
        }

        public class LengthAccPair
        {
            public float length, acc;

            public LengthAccPair(float length, float acc)
            {
                this.length = length;
                this.acc = acc;
            }
        }

        #region ChartDisplay
        public string GetChartInfoToDisplay(bool showAllSpeed)
        {
            return $"{name} processed in {calculationTime.TotalSeconds}s\n" +
                   (showAllSpeed ? GetChartTextAtAllSpeed() : GetChartTextAtSpeed(2)) +
                   $"===============================================================\n";
        }

        public string GetChartTextAtAllSpeed()
        {
            string text = "";
            for (int i = 0; i < Utils.GAME_SPEED.Length; i++)
                text += GetChartTextAtSpeed(i);
            return text;
        }

        public string GetChartTextAtSpeed(int speedIndex)
        {
            return $"SPEED: {Utils.GAME_SPEED[speedIndex]:0.00}x rated {GetStarRating(speedIndex):0.0000})\n" +
                       $"  aim: {performances.aimAnalyticsArray[speedIndex].perfWeightedAverage:0.0000}\n" +
                       $"  tap: {performances.tapAnalyticsArray[speedIndex].perfWeightedAverage:0.0000}\n" +
                       $"-------------------------------------------------\n";
        }
        #endregion

        #region LeaderboardDisplay
        public string GetLeaderboardToDisplay()
        {
            if (leaderboard == null || leaderboard.results.Count == 0) return GetNoLeaderboardText();
            var ttOrdered = leaderboard.results.OrderByDescending(s => s.tt).ToArray();
            string text = GetLeaderboardHeader();
            for (int i = 0; i < ttOrdered.Length; i++)
                text += GetDisplayScoreLine(ttOrdered[i], i + 1);
            text += $"==========================================================================================================\n";
            return text;
        }

        private string GetNoLeaderboardText() =>
            $"{name} processed in {calculationTime.TotalSeconds}s\n" +
            $"No leaderboard entry found.\n" +
            $"==========================================================================================================\n";

        private string GetLeaderboardHeader() =>
            $"{name} processed in {calculationTime.TotalSeconds}s\n" +
            GetLeaderboardScoreHeader() +
            $"----------------------------------------------------------------------------------------------------------\n";

        private string GetDisplayScoreLine(Leaderboard.ScoreDataFromDB score, int count) =>
            FormatLeaderboardScore(
                count,
                score.player,
                score.score,
                score.replay_speed,
                score.percentage,
                score.grade,
                score.tt,
                GetDynamicDiffRating(score.replay_speed, score.GetHitCount, score.modifiers),
                score.modifiers
                );

        private string FormatLeaderboardScore(int count, string player, int score, float replaySpeed, float percentage, string grade, float tt, float diff, string[] modifiers)
        {
            return String.Format("{0,-4} | {1,-30} | {2, -11} | {3, -8} | {4, -6} | {5, -5} | {6, -10} | {7, 5} | {8, 4} |\n",
                $"#{count}",
                $"{player}",
                $"{score}",
                $"({replaySpeed:0.00}x)",
                $"{percentage:0.00}%",
                $"{grade}",
                $"{tt:0.00}tt",
                $"{diff:0.00}",
                $"{(modifiers != null ? string.Join(',', modifiers) : "NONE")}"
                );
        }

        private string GetLeaderboardScoreHeader()
        {
            return String.Format("{0,-4} | {1,-30} | {2, -11} | {3, -8} | {4, -6} | {5, -5} | {6, -10} | {7, 5} | {8, 4} |\n",
                $"Rank",
                $"Name",
                $"Score",
                $"Speed",
                $"Perc",
                $"Grade",
                $"TT",
                $"Diff",
                $"Mods"
                );
        }
        #endregion
    }
}
