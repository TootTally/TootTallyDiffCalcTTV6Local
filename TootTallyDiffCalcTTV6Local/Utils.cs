using Newtonsoft.Json;
using System.Numerics;
using System.Text.Json.Nodes;

namespace TootTallyDiffCalcTTV6Local
{
    public static class Utils
    {
        public static readonly float[] GAME_SPEED = { .5f, .75f, 1f, 1.25f, 1.5f, 1.75f, 2f };

        public static float Lerp(float firstFloat, float secondFloat, float by) //Linear easing
        {
            return firstFloat + (secondFloat - firstFloat) * by;
        }

        public static float FastPow(double num, int exp)
        {
            double result = 1.0;
            while (exp > 0)
            {
                if (exp % 2 == 1)
                    result *= num;
                exp >>= 1;
                num *= num;
            }
            return (float)result;
        }
        public static List<Vector4> ConvertChartToVector(List<Note> notes, string fileName, float lengthMult = 1)
        {
            var list = new List<Vector4>();
            var lastTime = 0f;
            var lastPosition = 0f;
            for (int i = 1; i < notes.Count; i++)
            {
                var spaceDuration = notes[i].position - lastTime;
                lastTime = notes[i].position + notes[i].length;
                var noteDuration = notes[i].length;
                if (spaceDuration != 0f)
                    list.Add(new Vector4(spaceDuration * lengthMult, lastPosition, notes[i].pitchStart, 0));
                list.Add(new Vector4(noteDuration * lengthMult, notes[i].pitchStart, notes[i].pitchEnd, 1));
            }
            
            var json = JsonConvert.SerializeObject(list);
            ChartReader.SaveChartData($"{fileName}.json", json);
            return list;
        }
    }
}
