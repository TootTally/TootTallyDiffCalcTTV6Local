using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TootTallyDiffCalcTTV6Local
{
    public struct ChartPerformances
    {
        public DataVector[][] aimPerfMatrix;
        public DataVectorAnalytics[] aimAnalyticsArray;

        public DataVector[][] tapPerfMatrix;
        public DataVectorAnalytics[] tapAnalyticsArray;

        public ExtraDataVector[][] extraDataVectorMatrix;

        public float[] aimRatingDict;
        public float[] tapRatingDict;
        public float[] starRatingDict;

        private readonly float tempo;
        private readonly int NOTE_COUNT;
        private readonly Note[] _notesArray;

        public ChartPerformances(Chart chart)
        {
            _notesArray = chart.notesArray;
            NOTE_COUNT = chart.notesArray.Length;
            tempo = chart.tempo;

            aimPerfMatrix = new DataVector[Utils.GAME_SPEED.Length][];
            aimAnalyticsArray = new DataVectorAnalytics[Utils.GAME_SPEED.Length];

            tapPerfMatrix = new DataVector[Utils.GAME_SPEED.Length][];
            tapAnalyticsArray = new DataVectorAnalytics[Utils.GAME_SPEED.Length];

            extraDataVectorMatrix = new ExtraDataVector[Utils.GAME_SPEED.Length][];

            aimRatingDict = new float[Utils.GAME_SPEED.Length];
            tapRatingDict = new float[Utils.GAME_SPEED.Length];
            starRatingDict = new float[Utils.GAME_SPEED.Length];

            for (int i = 0; i < Utils.GAME_SPEED.Length; i++)
            {
                extraDataVectorMatrix[i] = new ExtraDataVector[NOTE_COUNT];
                aimPerfMatrix[i] = new DataVector[NOTE_COUNT];
                tapPerfMatrix[i] = new DataVector[NOTE_COUNT];
            }
        }

        public const float MAX_DIST = 1;
        public const float MAX_NOTE_COUNT = 10;
        public const float CHEESABLE_THRESHOLD = 34.375f;

        public void CalculatePerformance(int speedIndex)
        {
            var newTempo = Utils.GAME_SPEED[speedIndex] * tempo;
            int noteCount;
            float aimEnd = 0, aimSta = 0, tapEnd = 0, tapSta = 0;
            for (int i = 0; i < NOTE_COUNT; i++) //Main Forward Loop
            {
                noteCount = 0;
                float aimStrain = 0, tapStrain = 0;
                ConvertNote(_notesArray[i], newTempo, out var n1Current);
                for (int j = i - 1; j >= 0 && noteCount < MAX_NOTE_COUNT && (MathF.Abs(n1Current.position - _notesArray[j].position) <= MAX_DIST || i - j <= 2); j--) //Secondary Backward Loop
                {
                    ConvertNote(_notesArray[j], newTempo, out var n2Prev);
                    ConvertNote(_notesArray[j + 1], newTempo, out var n2Next);
                    var lengthSum = n2Prev.length;
                    if (n2Prev.position >= n2Next.position) break;
                    while (n2Prev.isSlider) //Merge all sliders into one note
                    {
                        if (j-- <= 0)
                            break;
                        ConvertNote(_notesArray[j], newTempo, out n2Prev);
                        ConvertNote(_notesArray[j + 1], newTempo, out n2Next);

                        if (n2Prev.pitchDelta == 0)
                            lengthSum += n2Prev.length;
                        else
                        {
                            var deltaSlide = MathF.Abs(n2Prev.pitchDelta);
                            lengthSum += n2Prev.length;
                        }
                    }
                    var deltaTime = n2Next.position - n2Prev.position;
                    var aimDistance = MathF.Abs(n2Next.pitchStart - n2Prev.pitchEnd);
                    var currVelocity = MathF.Abs(aimDistance / deltaTime);
                    aimStrain = MathF.Sqrt(currVelocity);
                    tapStrain = 1f / deltaTime;
                }
                

                //If you're at the start or end, copy the current note as the previous or next note
                extraDataVectorMatrix[speedIndex][i] = new ExtraDataVector(n1Current, i == _notesArray.Length - 1 ? n1Current : _notesArray[i + 1], i == 0 ? n1Current : _notesArray[i - 1], newTempo);
                
                aimPerfMatrix[speedIndex][i] = new DataVector(n1Current.position, aimStrain, aimSta, aimEnd, 1);
                tapPerfMatrix[speedIndex][i] = new DataVector(n1Current.position, tapStrain, tapSta, tapEnd, 1);
            }
        }

        public static void ConvertNote(Note note, float newTempo, out Note n)
        {
            n = note;
            n.position = BeatToSeconds2(note.position, newTempo);
            n.length = BeatToSeconds2(note.length, newTempo);
        }

        public void CalculateAnalytics(int gamespeed)
        {
            aimAnalyticsArray[gamespeed] = new DataVectorAnalytics(aimPerfMatrix[gamespeed]);
            tapAnalyticsArray[gamespeed] = new DataVectorAnalytics(tapPerfMatrix[gamespeed]);
        }


        public void CalculateRatings(int gamespeed)
        {
            var aimRating = aimRatingDict[gamespeed] = aimAnalyticsArray[gamespeed].perfWeightedAverage + 0.01f;
            var tapRating = tapRatingDict[gamespeed] = tapAnalyticsArray[gamespeed].perfWeightedAverage + 0.01f;

            if (aimRating != 0 && tapRating != 0)
            {
                var totalRating = aimRating + tapRating;
                var aimPerc = aimRating / totalRating;
                var tapPerc = tapRating / totalRating;
                var aimWeight = aimPerc * AIM_WEIGHT;
                var tapWeight = tapPerc * TAP_WEIGHT;
                var totalWeight = aimWeight + tapWeight;
                starRatingDict[gamespeed] = ((aimRating * aimWeight) + (tapRating * tapWeight)) / totalWeight;
            }
            else
                starRatingDict[gamespeed] = 0f;
        }

        public float GetDynamicAimRating(int hitCount, float speed) => GetDynamicSkillRating(hitCount, speed, aimPerfMatrix);
        public float GetDynamicTapRating(int hitCount, float speed) => GetDynamicSkillRating(hitCount, speed, tapPerfMatrix);

        private float GetDynamicSkillRating(int hitCount, float speed, DataVector[][] skillRatingMatrix)
        {
            var index = (int)((speed - 0.5f) / .25f);

            if (skillRatingMatrix[index].Length <= 1 || hitCount <= 0)
                return 0;
            else if (speed % .5f == 0)
                return CalcSkillRating(hitCount, skillRatingMatrix[index]);

            var r1 = CalcSkillRating(hitCount, skillRatingMatrix[index]);
            var r2 = CalcSkillRating(hitCount, skillRatingMatrix[index + 1]);

            var minSpeed = Utils.GAME_SPEED[index];
            var maxSpeed = Utils.GAME_SPEED[index + 1];
            var by = (speed - minSpeed) / (maxSpeed - minSpeed);
            return Utils.Lerp(r1, r2, by);
        }

        public const float MAP = .05f;
        public const float MACC = .5f;

        private float CalcSkillRating(int hitCount, DataVector[] skillRatingArray)
        {
            int maxRange;

            float percent = 1f;
            if (hitCount < NOTE_COUNT)
                percent = MathF.Min((float)hitCount / NOTE_COUNT, 1f);

            if (percent <= MACC)
                maxRange = (int)Math.Clamp(skillRatingArray.Length * (percent * (MAP / MACC)), 1, skillRatingArray.Length);
            else
                maxRange = (int)Math.Clamp(skillRatingArray.Length * ((percent - MACC) * ((1f - MAP) / (1f - MACC)) + MAP), 1, skillRatingArray.Length);

            var array = skillRatingArray.OrderBy(x => x.performance + x.endurance).ToArray()[0..maxRange];
            var analytics = new DataVectorAnalytics(array);
            return analytics.perfWeightedAverage + .01f;
        }

        public const float AIM_WEIGHT = 1f;
        public const float TAP_WEIGHT = 1f;

        public static readonly float[] HDWeights = { .1f, .1f };
        public static readonly float[] FLWeights = { .1f, .1f };
        public static readonly float[] EZWeights = { -.1f, -.1f };

        public float GetDynamicDiffRating(int hitCount, float gamespeed, string[] modifiers = null)
        {
            var aimRating = GetDynamicAimRating(hitCount, gamespeed);
            var tapRating = GetDynamicTapRating(hitCount, gamespeed);

            if (aimRating == 0 && tapRating == 0) return 0f;

            var totalRating = aimRating + tapRating;
            var aimPerc = aimRating / totalRating;
            var tapPerc = tapRating / totalRating;
            var aimWeight = aimPerc * AIM_WEIGHT;
            var tapWeight = tapPerc * TAP_WEIGHT;
            var totalWeight = aimWeight + tapWeight;

            return ((aimRating * aimWeight) + (tapRating * tapWeight)) / totalWeight;
        }

        public void Dispose()
        {
            aimPerfMatrix = null;
            aimAnalyticsArray = null;
            aimRatingDict = null;
            tapPerfMatrix = null;
            tapAnalyticsArray = null;
            tapRatingDict = null;
            starRatingDict = null;
        }

        public struct ExtraDataVector
        {
            public float
                distanceToNextNote, distanceFromPreviousNote,
                timingToNextNote, timingFromPreviousNote,
                nextNotePos, prevNotePos;


            public ExtraDataVector(Note currentNote, Note nextNote, Note previousNote, float newTempo)
            {
                ConvertNote(nextNote, newTempo, out nextNote);
                ConvertNote(previousNote, newTempo, out previousNote);
                nextNotePos = nextNote.position;
                prevNotePos = previousNote.position;
                distanceToNextNote = nextNote.pitchStart - currentNote.pitchEnd;
                distanceFromPreviousNote = previousNote.pitchEnd - currentNote.pitchStart;
                timingToNextNote = nextNote.position - currentNote.position;
                timingFromPreviousNote = currentNote.position - previousNote.position;
            }
        }

        public struct DataVector(float time, float performance, float stamina, float endurance, float weight)
        {
            public float time = time;
            public float stamina = stamina;
            public float endurance = endurance;
            public float performance = performance;
            public float weight = weight;
        }

        public struct DataVectorAnalytics
        {
            public float perfSum, perfWeightedAverage;
            public float weightSum;

            public DataVectorAnalytics(DataVector[] dataVectorList)
            {
                perfSum = perfWeightedAverage = 0;
                weightSum = 1;

                if (dataVectorList == null || dataVectorList.Length <= 0) return;
                CalculateWeightSum(dataVectorList);
                CalculateData(dataVectorList);
            }

            public void CalculateWeightSum(DataVector[] dataVectorList)
            {
                for (int i = 0; i < dataVectorList.Length; i++)
                    weightSum += dataVectorList[i].weight;
            }

            public void CalculateData(DataVector[] dataVectorList)
            {
                for (int i = 0; i < dataVectorList.Length; i++)
                    perfSum += (dataVectorList[i].performance + dataVectorList[i].stamina + dataVectorList[i].endurance) * (dataVectorList[i].weight / weightSum);
                perfWeightedAverage = perfSum;
            }
        }
        public static float BeatToSeconds2(float beat, float bpm) => 60f / bpm * beat;
    }
}
