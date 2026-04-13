using Newtonsoft.Json;
using System.Security.Cryptography;

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

        public static List<Leaderboard.SongInfoFromDB> GetCachedFileHashes()
        {
            if (!File.Exists(Program.CACHE_DIRECTORY + "file_hash.txt"))
                File.Create(Program.CACHE_DIRECTORY + "file_hash.txt").Close();

            StreamReader reader = new StreamReader(Program.CACHE_DIRECTORY + "file_hash.txt");
            string json = reader.ReadToEnd();
            try
            {
                return JsonConvert.DeserializeObject<List<Leaderboard.SongInfoFromDB>>(json);
            }
            catch (Exception e)
            {
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
    }
}
