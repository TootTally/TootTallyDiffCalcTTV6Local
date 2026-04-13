
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace TootTallyDiffCalcTTV6Local
{
    public partial class LoadingForm : Form
    {
        private System.Timers.Timer _timer;
        private Stopwatch _sw;
        private ConcurrentQueue<string> _descriptionHistoryQueue;

        public LoadingForm()
        {
            InitializeComponent();
            StartTimer();
            _descriptionHistoryQueue = new ConcurrentQueue<string>();
            for (int i = 0; i < 8; i++) _descriptionHistoryQueue.Enqueue(" ");//That's stupid lol
            Show();
        }

        public void StartTimer()
        {
            _sw = Stopwatch.StartNew();
            _timer = new(TimeSpan.FromMilliseconds(100));
            _timer.Elapsed += UpdateTimer;
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
            _timer.Elapsed -= UpdateTimer;
            _timer.Dispose();
            UpdateTimer(this, new EventArgs());
            _sw.Stop();
        }

        public void OnLoadingFinished()
        {
            StopTimer();
            if (InvokeRequired)
            {
                BeginInvoke(Close);
                return;
            }
            this.Close();
        }

        private void UpdateTimer(object sender, EventArgs e)
        {
            if (ElapsedLabel.IsDisposed) return;
            if (!ElapsedLabel.InvokeRequired)
            {
                ElapsedLabel.Text = $"Time: {_sw.Elapsed:mm\\:ss\\.ff}";
                ElapsedLabel.Update();
            }
            else if (!ElapsedLabel.IsDisposed)
                ElapsedLabel.Invoke(delegate
                {
                    ElapsedLabel.Text = $"Time: {_sw.Elapsed:mm\\:ss\\.ff}";
                });
        }

        public void UpdateProgress(float percent)
        {
            if (ProgressBar.IsDisposed || PercentLabel.IsDisposed) return;
            percent *= 100f;
            if (!ProgressBar.InvokeRequired)
            {
                ProgressBar.Value = (int)percent * 100;//Made the progress bar max 10000 for extra precision xd
                ProgressBar.Update();
            }
            else
                ProgressBar.Invoke(delegate
                {
                    ProgressBar.Value = (int)percent * 100;
                });
            if (!PercentLabel.InvokeRequired)
            {
                PercentLabel.Text = $"{percent:0.00}%";
                PercentLabel.Update();
            }
            else
                PercentLabel.Invoke(delegate
                {
                    PercentLabel.Text = $"{percent:0.00}%";
                });
        }

        public void UpdateDescription(string text, bool pushToHistory = false)
        {
            if (DescriptionLabel.IsDisposed || DescriptionHistoryBox.IsDisposed) return;
            if (pushToHistory)
            {
                if (_descriptionHistoryQueue.Count >= 9)
                    _descriptionHistoryQueue.TryDequeue(out var _);
                _descriptionHistoryQueue.Enqueue(text);
                Trace.WriteLine(text);
            }
            if (!DescriptionLabel.InvokeRequired)
            {
                DescriptionLabel.Text = text;
                DescriptionLabel.Update();
            }
            else
                DescriptionLabel.Invoke(delegate
                {
                    DescriptionLabel.Text = text;
                });

            if (!DescriptionHistoryBox.InvokeRequired)
            {
                if (!ShouldUpdateDescriptionHistory()) return;
                DescriptionHistoryBox.Text = string.Join("\n", _descriptionHistoryQueue);
                DescriptionHistoryBox.Update();
            }
            else
                DescriptionHistoryBox.Invoke(delegate
                {
                    if (!ShouldUpdateDescriptionHistory()) return;
                    DescriptionHistoryBox.Text = string.Join("\n", _descriptionHistoryQueue);
                });
        }
        private bool ShouldUpdateDescriptionHistory() => _descriptionHistoryQueue.TryPeek(out var r) && DescriptionHistoryBox.Lines[DescriptionHistoryBox.Lines.Length - 1] != r;
    }
}
