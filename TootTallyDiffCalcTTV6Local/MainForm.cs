using Microsoft.DotNet.DesignTools.Protocol.Endpoints;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing;
using System.Security.Policy;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static TootTallyDiffCalcTTV6Local.ChartPerformances;

namespace TootTallyDiffCalcTTV6Local
{
    public partial class MainForm : Form
    {
        private const bool DO_LEADERBOARDS = true;
        private const int MAX_REQUESTS = 16;

        private LoadingForm _currentLoadingForm;

        private static List<Leaderboard> _leaderboardList;
        private List<Leaderboard.SongInfoFromDB> _fileHashes;
        private List<int> _filteredIDs, _idList;
        private List<Chart> _chartList;



        public MainForm()
        {
            InitializeComponent();
        }

        private async void OnFormShown(object sender, EventArgs e)
        {
            RunRoutine();
        }

        private async void RunRoutine()
        {
            _currentLoadingForm = new LoadingForm();
            _currentLoadingForm.Update();
            GetAllRatedChartIDs();
            GetCachedFileHashes();

            _leaderboardList = new List<Leaderboard>(_idList.Count);
            if (DO_LEADERBOARDS)
                await Task.Run(GetAllRatedLeaderboards).ConfigureAwait(false);
            await Task.Run(DownloadAllTmbs).ConfigureAwait(false);
            await Task.Run(LoadAllCharts).ConfigureAwait(false);
            await Task.Run(DisplayChartData).ConfigureAwait(false);
            _currentLoadingForm.UpdateDescription($"Routine finished running.", true);
            _currentLoadingForm.UpdateDescription($"You can close this form.", true);
            _currentLoadingForm.StopTimer();
            PostRoutine();
        }

        private void PostRoutine()
        {
            if (!ChartViewerComboBox.InvokeRequired)
            {
                ChartViewerComboBox.Items.Add("Select A Chart");
                ChartViewerComboBox.Items.AddRange(_chartList.Select(x => x.shortName).Order().ToArray());
                ChartViewerComboBox.Update();
            }
            else
                ChartViewerComboBox.Invoke(delegate
                {
                    ChartViewerComboBox.Items.Add("Select A Chart");
                    ChartViewerComboBox.Items.AddRange(_chartList.Select(x => x.shortName).Order().ToArray());
                });
            ToggleEnableRecalcChartDataButton(true);
        }

        private void GetAllRatedChartIDs()
        {
            _currentLoadingForm.UpdateDescription("Grabbing all rated chart IDs...", true);
            _idList = TootTallyAPIServices.GetAllRatedChartIDs();
            _currentLoadingForm.UpdateDescription($"Received {_idList.Count} rated charts IDs.", true);
        }

        private void GetCachedFileHashes()
        {
            _fileHashes = ChartReader.GetCachedFileHashes();
            _fileHashes ??= new();
            _filteredIDs = _idList.Where(id => _fileHashes.Any(x => x != null && x.id == id)).ToList();
        }

        private async Task GetAllRatedLeaderboards()
        {
            List<Task> tasks = new List<Task>(_idList.Count);
            _currentLoadingForm.UpdateDescription($"Grabbing all leaderboards from IDs...", true);
            for (int i = 0; i < _idList.Count; i++)
            {
                var index = i;
                tasks.Add(new Task(delegate
                {
                    TootTallyAPIServices.GetLeaderboardFromId(_idList[index], _leaderboardList.Add);
                }));
            }
            for (int i = 0; i < MAX_REQUESTS; i++)
                tasks[i].Start();
            while (tasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(tasks);
                tasks.Remove(finishedTask);
                var t = tasks.FirstOrDefault(x => x.Status == TaskStatus.Created);
                if (t != default)
                    t.Start();
                _currentLoadingForm.UpdateProgress((float)_leaderboardList.Count / _idList.Count);
                _currentLoadingForm.UpdateDescription($"Loaded {_leaderboardList.Count}/{_idList.Count} leaderboards, Pending: {tasks.Count}");
            }
            _currentLoadingForm.UpdateProgress(1);
            _currentLoadingForm.UpdateDescription($"All Leaderboards loaded.", true);
        }

        private async Task DownloadAllTmbs()
        {
            _currentLoadingForm.UpdateDescription($"Filtering already downloaded tmbs...", true);
            List<string> urls = new List<string>(_leaderboardList.Count);
            var filteredLeaderboards = _leaderboardList.Where(l => l.results.Count > 0 && l.song_info != null
            && l.song_info.file_hash != null && l.song_info.track_ref != null &&
            !_filteredIDs.Any(id => l.song_info.id == id)).ToList();
            if (filteredLeaderboards.Count <= 0)
            {
                _currentLoadingForm.UpdateDescription($"Found 0 missing tmb... skipping tmb downloads", true);
                return;
            }

            List<Task> tasks = new List<Task>(filteredLeaderboards.Count);
            _currentLoadingForm.UpdateDescription($"Found {filteredLeaderboards.Count} missing tmbs...", true);
            for (int i = 0; i < filteredLeaderboards.Count; i++)
            {
                var l = filteredLeaderboards[i];
                _fileHashes.Add(new Leaderboard.SongInfoFromDB() { id = l.song_info.id, file_hash = l.song_info.file_hash, track_ref = l.song_info.track_ref });
                var url = $"{l.song_info.file_hash}.tmb";
                tasks.Add(new Task(delegate
                {
                    TootTallyAPIServices.GetTmbJsonFromURL(url, OnTmbDownloadFinishCallback);
                }));
            }
            for (int i = 0; i < MAX_REQUESTS; i++)
                tasks[i].Start();
            while (tasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(tasks);
                tasks.Remove(finishedTask);
                var t = tasks.FirstOrDefault(x => x.Status == TaskStatus.Created);
                if (t != default)
                    t.Start();
                _currentLoadingForm.UpdateProgress((float)(filteredLeaderboards.Count - tasks.Count) / filteredLeaderboards.Count);
                _currentLoadingForm.UpdateDescription($"Downloaded {filteredLeaderboards.Count - tasks.Count}/{filteredLeaderboards.Count} tmbs, Pending: {tasks.Count}");
            }
            _currentLoadingForm.UpdateProgress(1);
            _currentLoadingForm.UpdateDescription($"All Tmbs loaded.", true);
        }

        private void OnTmbDownloadFinishCallback(string json, string filename)
        {
            if (!File.Exists(Program.DOWNLOAD_DIRECTORY + filename))
                ChartReader.SaveChartData(Program.DOWNLOAD_DIRECTORY + filename, json);
        }

        private async Task LoadAllCharts()
        {
            _currentLoadingForm.UpdateDescription($"Starting chart loading...", true);
            var filesList = Directory.GetFiles(Program.DOWNLOAD_DIRECTORY);
            _currentLoadingForm.UpdateDescription($"Found {filesList.Length} charts to load...", true);
            _chartList = new List<Chart>(filesList.Length);
            List<Task> tasks = new List<Task>(filesList.Length);
            for (int i = 0; i < filesList.Length; i++)
            {
                var index = i;
                tasks.Add(new Task(delegate
                {
                    ChartReader.LoadChart(filesList[index], OnChartLoaded);
                }));
            }
            for (int i = 0; i < MAX_REQUESTS * 2; i++)
                tasks[i].Start();
            while (tasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(tasks);
                tasks.Remove(finishedTask);
                var t = tasks.FirstOrDefault(x => x.Status == TaskStatus.Created);
                if (t != default)
                    t.Start();
                _currentLoadingForm.UpdateProgress((float)_chartList.Count / filesList.Length);
                _currentLoadingForm.UpdateDescription($"Downloaded {_chartList.Count}/{filesList.Length} tmbs, Pending: {tasks.Count}");
            }
            _currentLoadingForm.UpdateProgress(1);
            _currentLoadingForm.UpdateDescription($"All Charts loaded.", true);
        }

        private void OnChartLoaded(Chart c)
        {
            c.CalcPerformances();
            if (DO_LEADERBOARDS)
            {
                var lb = _leaderboardList.FirstOrDefault(l => l.song_info.track_ref == c.trackRef);
                if (lb != default)
                {
                    c.leaderboard = lb;
                    c.CalcLeaderboardTT();
                }
            }
            _chartList.Add(c);
        }

        private async Task DisplayChartData()
        {
            _currentLoadingForm.UpdateDescription($"Processing {_chartList.Count} chart's text to display...", true);
            ClearChartInfoTextBox();
            ClearLeaderboardInfoTextBox();
            List<Task> tasks = new List<Task>(_chartList.Count);
            for (int i = 0; i < _chartList.Count; i++)
            {
                var chart = _chartList[i];
                tasks.Add(new Task(delegate
                {
                    WriteToChartInfoTextBox(chart.GetChartInfoToDisplay(true));
                    WriteToLeaderboardInfoTextBox(chart.GetLeaderboardToDisplay());
                }));
            }
            for (int i = 0; i < MAX_REQUESTS * 2; i++)
                tasks[i].Start();
            while (tasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(tasks);
                tasks.Remove(finishedTask);
                var t = tasks.FirstOrDefault(x => x.Status == TaskStatus.Created);
                if (t != default)
                    t.Start();
                _currentLoadingForm.UpdateProgress((float)(_chartList.Count - tasks.Count) / _chartList.Count);
                _currentLoadingForm.UpdateDescription($"Wrote {_chartList.Count - tasks.Count}/{_chartList.Count} charts, Pending: {tasks.Count}");
            }
            _currentLoadingForm.UpdateProgress(1);
            _currentLoadingForm.UpdateDescription($"All Charts Info written.", true);
        }

        private void WriteToChartInfoTextBox(string text) => WriteToTextBox(ChartInfoBox, text);

        private void ClearChartInfoTextBox() => ClearTextBox(ChartInfoBox);

        private void WriteToLeaderboardInfoTextBox(string text) => WriteToTextBox(ChartLeaderboardBox, text);

        private void ClearLeaderboardInfoTextBox() => ClearTextBox(ChartLeaderboardBox);

        private void WriteToTextBox(RichTextBox tb, string text)
        {
            if (!tb.InvokeRequired)
            {
                tb.AppendText(text);
                tb.Update();
            }
            else
                tb.Invoke(delegate { tb.AppendText(text); });
        }

        private void ClearTextBox(RichTextBox tb)
        {
            if (!tb.InvokeRequired)
            {
                tb.Clear();
                tb.Update();
            }
            else
                tb.Invoke(tb.Clear);
        }

        private void OnChartViewerSelectionChanged(object sender, EventArgs e)
        {
            var chart = _chartList.Find(x => x.shortName == ChartViewerComboBox.Text);
            if (chart.shortName != default)
            {
                var g = new ChartVisualizerForm(chart);
                g.Show();
            }
        }

        private async void OnRecalcChartDataClick(object sender, EventArgs e)
        {
            ToggleEnableRecalcChartDataButton(false);
            _currentLoadingForm = new LoadingForm();
            await Task.Run(DisplayChartData).ConfigureAwait(false);
            _currentLoadingForm.OnLoadingFinished();
            ToggleEnableRecalcChartDataButton(true);
        }

        private void ToggleEnableRecalcChartDataButton(bool isEnabled)
        {
            if (!RecalcChartDataButton.InvokeRequired)
                RecalcChartDataButton.Enabled = isEnabled;
            else
                RecalcChartDataButton.Invoke(delegate { RecalcChartDataButton.Enabled = isEnabled; });
        }
    }
}