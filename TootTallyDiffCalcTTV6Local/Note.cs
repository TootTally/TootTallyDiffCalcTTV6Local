namespace TootTallyDiffCalcTTV6Local
{
    public struct Note(int count, float position, float length, float pitchStart, float pitchDelta, float pitchEnd, bool isSlider)
    {
        public int count = count;
        public float position = position;
        public float length = length;
        public float pitchStart = pitchStart;
        public float pitchDelta = pitchDelta;
        public float pitchEnd = pitchEnd;
        public bool isSlider = isSlider;
    }
}
