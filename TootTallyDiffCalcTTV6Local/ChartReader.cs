using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using static TootTallyDiffCalcTTV6Local.Leaderboard;

namespace TootTallyDiffCalcTTV6Local
{
    public static class ChartReader
    {

        public static ReplayData LoadReplay(string path)
        {
            ReplayData replay = JsonConvert.DeserializeObject<ReplayData>(File.ReadAllText(path));
            return replay;
        }

        public static ReplayData LoadReplayFromJson(string json)
        {
            ReplayData replay = JsonConvert.DeserializeObject<ReplayData>(json);
            replay.OnDeserialize();
            return replay;
        }

        public static Chart LoadChart(string path)
        {
            StreamReader reader = new StreamReader(path);
            string json = reader.ReadToEnd();
            Chart chart = default;
            try
            {
                chart = JsonConvert.DeserializeObject<Chart>(json);
                chart.OnDeserialize();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"song {path} is not a valid json");
            }
            finally
            {
                reader.Close();
            }
            return chart;
        }

        public static bool LoadChart(string path, Action<Chart> callback)
        {
            StreamReader reader = new StreamReader(path);
            string json = reader.ReadToEnd();
            bool success = false;
            try
            {
                var chart = JsonConvert.DeserializeObject<Chart>(json);
                chart.OnDeserialize();
                callback(chart);
                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"song {path} is not a valid json");
                success = false;
            }
            finally
            {
                reader.Close();
            }
            return success;
        }

        public static void SaveCacheFileHashes(List<Leaderboard.SongInfoFromDB> fileHashes)
        {
            if (File.Exists(Program.CACHE_DIRECTORY + "file_hash.txt"))
                File.Delete(Program.CACHE_DIRECTORY + "file_hash.txt");

            StreamWriter writer = new StreamWriter(Program.CACHE_DIRECTORY + "file_hash.txt");
            writer.WriteLine(JsonConvert.SerializeObject(fileHashes));
            writer.Close();
        }

        public static List<SongInfoFromDB> GetCachedFileHashes()
        {
            if (!File.Exists(Program.CACHE_DIRECTORY + "file_hash.txt"))
                File.Create(Program.CACHE_DIRECTORY + "file_hash.txt").Close();

            StreamReader reader = new StreamReader(Program.CACHE_DIRECTORY + "file_hash.txt");
            string json = reader.ReadToEnd();
            try
            {
                return JsonConvert.DeserializeObject<List<SongInfoFromDB>>(json);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"ERROR - Couldn't load cached files: {e.Message}\n{e.StackTrace}");
                return null;
            }
            finally
            {
                reader.Close();
            }
        }

        public static Chart LoadChartFromJson(string json)
        {
            Chart chart = JsonConvert.DeserializeObject<Chart>(json);
            chart.OnDeserialize();
            return chart;
        }

        public static string CalcSHA256Hash(byte[] data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string ret = "";
                byte[] hashArray = sha256.ComputeHash(data);
                foreach (byte b in hashArray)
                {
                    ret += $"{b:x2}";
                }
                return ret;
            }
        }

        public static void SaveChartData(string path, string json)
        {
            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine(json);
            writer.Close();
        }

        public static void LoadScoresFromCSV(string path, Action<List<ScoreDataFromDB>> callback)
        {
            List<ScoreDataFromDB> scoreList;
            using StreamReader reader = new StreamReader(path);
            using CsvReader csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            csvReader.Context.RegisterClassMap<ScoreMap>();
            //song_id, name, user_id, username, score, modifiers, letter_grade, speed, replay_id, tt, played on
            try
            {
                scoreList = csvReader.GetRecords<ScoreDataFromDB>().ToList();
                callback(scoreList);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"ERROR - {path} couldn't be parsed: {e.Message}\n{e.StackTrace}");
            }
        }

        public sealed class ScoreMap : ClassMap<ScoreDataFromDB>
        {
            public ScoreMap()
            {
                Map(m => m.song_id).Name("song_id");
                Map(m => m.player).Name("username");
                Map(m => m.score).Name("score");
                //Map(m => m.modifiers).Name("modifiers");
                Map(m => m.modifiers).Convert(args =>
                {
                    var val = args.Row.GetField("modifiers");
                    if (string.IsNullOrEmpty(val)) return null;
                    return val.Split(',');
                });
                Map(m => m.grade).Name("letter_grade");
                Map(m => m.replay_speed).Name("speed");
                Map(m => m.tt).Name("tt");
                Map(m => m.perfect).Name("perfects");
                Map(m => m.nice).Name("nices");
                Map(m => m.okay).Name("okays");
                Map(m => m.meh).Name("mehs");
                Map(m => m.nasty).Name("nasties");
            }
        }
    }
}
