using System;
using System.Diagnostics;

namespace TootTallyDiffCalcTTV6Local
{
    public partial class MainForm : Form
    {
        private const bool DO_LEADERBOARDS = false;
        private const bool DO_LOCAL_SCORES = true;
        private const bool DO_DOWNLOADS = true;
        private const int MAX_REQUESTS = 16;

        private LoadingForm _currentLoadingForm;
        public static MainForm Instance;
        public static float[] WEIGHTS;
        public static int LEADERBOARD_ENTRY_COUNT;

        private static List<Leaderboard> _leaderboardList;
        private List<Leaderboard.SongInfoFromDB> _cachedFileHashes;
        private List<int> _filteredIDs, _idList;

        public static List<string> GetModifiersToShow;
        private Chart[] _chartList;
        private State _currentState;

        #region Getters
        public float GetMinTT() => (float)MinTTInput.Value;
        public float GetStrainMult() => (float)StrainMultInput.Value;
        public float GetStaminaMult() => (float)StaminaMultInput.Value;
        public float GetEnduranceMult() => (float)EnduranceMultInput.Value;
        public float GetAimStaminaDiv() => (float)AimStaminaDivInput.Value;
        public float GetAimEnduranceDiv() => (float)AimEnduranceDivInput.Value;
        public float GetTapStaminaDiv() => (float)TapStaminaDivInput.Value;
        public float GetTapEnduranceDiv() => (float)TapEnduranceDivInput.Value;
        public float GetHDAimMult() => (float)HDAimInput.Value;
        public float GetHDTapMult() => (float)HDTapInput.Value;
        public float GetFLAimMult() => (float)FLAimInput.Value;
        public float GetFLTapMult() => (float)FLTapInput.Value;
        public float GetEZAimMult() => (float)EZAimInput.Value;
        public float GetEZTapMult() => (float)EZTapInput.Value;
        #endregion

        public MainForm()
        {
            WEIGHTS = new float[64];
            for (int i = 0; i < WEIGHTS.Length; i++)
                WEIGHTS[i] = Utils.FastPow(.9f, i);
            Instance ??= this;
            GetModifiersToShow = new List<string>(4);
            InitializeComponent();
            SetInputDefaultValues();
        }

        private void SetInputDefaultValues()
        {
            MinTTInput.Value = 1000;

            StrainMultInput.Value = 315;
            StaminaMultInput.Value = 255;
            EnduranceMultInput.Value = 255;

            AimStaminaDivInput.Value = 300;
            AimEnduranceDivInput.Value = 350;

            TapStaminaDivInput.Value = 100;
            TapEnduranceDivInput.Value = 125;

            HDAimInput.Value = .11m;
            HDTapInput.Value = .09m;
            FLAimInput.Value = .12m;
            FLTapInput.Value = .1m;
            EZAimInput.Value = .5m;
            EZTapInput.Value = .4m;

            for (int i = 0; i < ModifierCheckboxInput.Items.Count; i++)
            {
                ModifierCheckboxInput.SetItemChecked(i, true);
                GetModifiersToShow.Add(((Modifiers)i).ToString());
            }
            ModifierCheckboxInput.ItemCheck += OnModifierCheckChange;
            DiffVisualizerButton.Enabled = false;
            ChartVisualizerButton.Enabled = false;
        }


        private async void OnFormShown(object sender, EventArgs e)
        {
            RunRoutine();
        }

        private async void RunRoutine()
        {
            _currentState = State.RunningTasks;
            _currentLoadingForm = new LoadingForm();
            _currentLoadingForm.Update();
            GetAllRatedChartIDs();
            GetCachedFileHashes();

            _leaderboardList = new List<Leaderboard>(_idList.Count);
            if (DO_LEADERBOARDS)
                await Task.Run(GetAllRatedLeaderboards).ConfigureAwait(false);
            if (DO_DOWNLOADS)
                await Task.Run(DownloadAllTmbs).ConfigureAwait(false);
            await Task.Run(LoadAllCharts).ConfigureAwait(false);
            if (!DO_LEADERBOARDS)
                await Task.Run(SetupBlankLeaderboards).ConfigureAwait(false); //I have to do this *after* the charts are loaded to add leaderboards to them ^^'
            if (DO_LOCAL_SCORES)
                await Task.Run(GetAllLocalScores).ConfigureAwait(false);
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
            _currentState = State.Idle;
        }

        private void GetAllRatedChartIDs()
        {
            _currentLoadingForm.UpdateDescription("Grabbing all rated chart IDs...", true);
            _idList = TootTallyAPIServices.GetAllRatedChartIDs();
            _currentLoadingForm.UpdateDescription($"Received {_idList.Count} rated charts IDs.", true);
        }

        private void GetCachedFileHashes()
        {
            _cachedFileHashes = ChartReader.GetCachedFileHashes();
            _cachedFileHashes ??= new();
            Trace.WriteLine($"Found {_cachedFileHashes.Count} cached hashes.");
            _filteredIDs = _idList.Where(id => _cachedFileHashes.Any(x => x != null && x.id == id)).ToList();
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
            for (int i = 0; i < tasks.Count && i < MAX_REQUESTS; i++)
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

        private async Task SetupBlankLeaderboards()
        {
            List<Task> tasks = new List<Task>(_chartList.Length);
            _currentLoadingForm.UpdateDescription($"Creating blank leaderboards...", true);
            for (int i = 0; i < _chartList.Length; i++)
            {
                var index = i;
                tasks.Add(new Task(delegate
                {
                    var cachedInfo = _cachedFileHashes.FirstOrDefault(s => s.track_ref == _chartList[index].trackRef);
                    if (cachedInfo != null)
                    {
                        _chartList[index].leaderboard = new Leaderboard()
                        {
                            results = new List<Leaderboard.ScoreDataFromDB>(),
                            song_info = cachedInfo,
                        };
                        _leaderboardList.Add(_chartList[index].leaderboard);
                    }
                }));
            }
            for (int i = 0; i < tasks.Count && i < MAX_REQUESTS; i++)
                tasks[i].Start();
            while (tasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(tasks);
                tasks.Remove(finishedTask);
                var t = tasks.FirstOrDefault(x => x.Status == TaskStatus.Created);
                if (t != default)
                    t.Start();
                _currentLoadingForm.UpdateProgress((float)_leaderboardList.Count / _chartList.Length);
                _currentLoadingForm.UpdateDescription($"Created {_leaderboardList.Count}/{_chartList.Length} leaderboards, Pending: {tasks.Count}");
            }

            _currentLoadingForm.UpdateProgress(1);
            _currentLoadingForm.UpdateDescription($"All Leaderboards created.", true);
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
                _cachedFileHashes.Add(new Leaderboard.SongInfoFromDB() { id = l.song_info.id, file_hash = l.song_info.file_hash, track_ref = l.song_info.track_ref });
                var url = $"{l.song_info.file_hash}.tmb";
                tasks.Add(new Task(delegate
                {
                    TootTallyAPIServices.GetTmbJsonFromURL(url, OnTmbDownloadFinishCallback);
                }));
            }
            for (int i = 0; i < tasks.Count && i < MAX_REQUESTS; i++)
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
            if (filteredLeaderboards.Count != 0)
            {
                ChartReader.SaveCacheFileHashes(_cachedFileHashes);
                _currentLoadingForm.UpdateDescription($"{filteredLeaderboards.Count} new leaderboards saved to cache.", true);
            }
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
            _chartList = new Chart[filesList.Length];
            List<Task> tasks = new List<Task>(filesList.Length);
            for (int i = 0; i < filesList.Length; i++)
            {
                var index = i;
                tasks.Add(new Task(delegate
                {
                    ChartReader.LoadChart(filesList[index], c => { OnChartLoaded(c, index); });
                }));
            }
            for (int i = 0; i < tasks.Count && i < MAX_REQUESTS * 2; i++)
                tasks[i].Start();
            while (tasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(tasks);
                tasks.Remove(finishedTask);
                var t = tasks.FirstOrDefault(x => x.Status == TaskStatus.Created);
                if (t != default)
                    t.Start();
                _currentLoadingForm.UpdateProgress((float)_chartList.Length / filesList.Length);
                _currentLoadingForm.UpdateDescription($"Downloaded {_chartList.Length}/{filesList.Length} tmbs, Pending: {tasks.Count}");
            }
            _currentLoadingForm.UpdateProgress(1);
            _currentLoadingForm.UpdateDescription($"All Charts loaded.", true);
        }

        private async Task GetAllLocalScores()
        {
            _currentLoadingForm.UpdateDescription($"Starting local scores loading...", true);
            List<Task> tasks = new List<Task>();
            int localScoreCount = 0, loadedScoreCount = 0;
            ChartReader.LoadScoresFromCSV("scores.csv", scoreList =>
            {
                localScoreCount = scoreList.Count;
                _currentLoadingForm.UpdateDescription($"Found {localScoreCount} scores to load...", true);
                foreach (var keypair in scoreList.GroupBy(score => score.song_id))
                {
                    tasks.Add(new Task(delegate
                    {
                        loadedScoreCount += keypair.Count();
                        var index = Array.FindIndex(_chartList, l => l.leaderboard != null && l.leaderboard.song_info.id == keypair.Key);
                        if (index != -1)
                            _chartList[index].leaderboard?.results.AddRange(keypair);
                    }));
                }
            });
            if (tasks.Count <= 0)
                _currentLoadingForm.UpdateDescription($"Local scores loading failed...", true);

            for (int i = 0; i < tasks.Count && i < MAX_REQUESTS * 2; i++)
                tasks[i].Start();
            while (tasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(tasks);
                tasks.Remove(finishedTask);
                var t = tasks.FirstOrDefault(x => x.Status == TaskStatus.Created);
                if (t != default)
                    t.Start();
                _currentLoadingForm.UpdateProgress((float)loadedScoreCount / localScoreCount);
                _currentLoadingForm.UpdateDescription($"Loaded {loadedScoreCount}/{localScoreCount} local scores, Pending: {tasks.Count}");
            }
            _currentLoadingForm.UpdateProgress(1);
            _currentLoadingForm.UpdateDescription($"All local scores loaded.", true);
        }

        private void OnChartLoaded(Chart c, int index)
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
            _chartList[index] = c;
        }

        private bool _writeChartInfo = true, _writeLeaderboardInfo = true;

        private async Task UpdateChartRatings()
        {
            _currentLoadingForm.UpdateDescription($"Processing {_chartList.Length} chart's ratings...", true);
            List<Task> tasks = new List<Task>(_chartList.Length);
            for (int i = 0; i < _chartList.Length; i++)
            {
                var index = i;
                tasks.Add(new Task(delegate
                {
                    _chartList[index].CalcPerformances();
                    _chartList[index].CalcLeaderboardTT();
                }));
            }
            for (int i = 0; i < tasks.Count && i < MAX_REQUESTS * 2; i++)
                tasks[i].Start();
            while (tasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(tasks);
                tasks.Remove(finishedTask);
                var t = tasks.FirstOrDefault(x => x.Status == TaskStatus.Created);
                if (t != default)
                    t.Start();
                _currentLoadingForm.UpdateProgress((float)(_chartList.Length - tasks.Count) / _chartList.Length);
                _currentLoadingForm.UpdateDescription($"Calculated {_chartList.Length - tasks.Count}/{_chartList.Length} charts, Pending: {tasks.Count}");
            }
            _currentLoadingForm.UpdateProgress(1);
            _currentLoadingForm.UpdateDescription($"All Charts Ratings calculated.", true);
        }

        private async Task DisplayChartData()
        {
            _currentLoadingForm.UpdateDescription($"Processing {_chartList.Length} chart's text to display...", true);
            LEADERBOARD_ENTRY_COUNT = 0;
            if (_writeChartInfo)
                ClearChartInfoTextBox();
            if (_writeLeaderboardInfo)
                ClearLeaderboardInfoTextBox();
            var filteredChartList = _chartList.Where(c => c.name.Contains(ChartNameInput.Text, StringComparison.CurrentCultureIgnoreCase)).ToArray();
            List<Task> tasks = new List<Task>(filteredChartList.Length);
            for (int i = 0; i < filteredChartList.Length; i++)
            {
                var chart = filteredChartList[i];
                tasks.Add(new Task(delegate
                {
                    if (_writeChartInfo)
                        WriteToChartInfoTextBox(chart.GetChartInfoToDisplay(true));
                    if (_writeLeaderboardInfo)
                        WriteToLeaderboardInfoTextBox(chart.GetLeaderboardToDisplay(true));
                }));
            }
            for (int i = 0; i < tasks.Count && i < MAX_REQUESTS * 2; i++)
                tasks[i].Start();
            while (tasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(tasks);
                tasks.Remove(finishedTask);
                var t = tasks.FirstOrDefault(x => x.Status == TaskStatus.Created);
                if (t != default)
                    t.Start();
                _currentLoadingForm.UpdateProgress((float)(filteredChartList.Length - tasks.Count) / filteredChartList.Length);
                _currentLoadingForm.UpdateDescription($"Wrote {filteredChartList.Length - tasks.Count}/{filteredChartList.Length} charts, Pending: {tasks.Count}");
            }
            _currentLoadingForm.UpdateProgress(1);
            _currentLoadingForm.UpdateDescription($"All Charts Info written.", true);
            WriteToTextBoxHeader(ChartLeaderboardBox, $"Score Calculation for {LEADERBOARD_ENTRY_COUNT} entries.\n");
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

        private void WriteToTextBoxHeader(RichTextBox tb, string text)
        {
            if (!tb.InvokeRequired)
            {
                tb.Text = text + tb.Text;
                tb.Update();
            }
            else
                tb.Invoke(delegate { tb.Text = text + tb.Text; });
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

        private Chart _savedSelectedChart;

        private void OnChartViewerSelectionChanged(object sender, EventArgs e)
        {
            var c = _chartList.FirstOrDefault(x => x.shortName == ChartViewerComboBox.Text);
            _savedSelectedChart = c;
            DiffVisualizerButton.Enabled = !c.Equals(default);
            ChartVisualizerButton.Enabled = !c.Equals(default);

        }

        private async void OnRecalcChartDataClick(object sender, EventArgs e) =>
            OnEventRefreshDisplays(true, true);

        private async void OnValueBoxTextChanged(object sender, EventArgs e) =>
            OnEventRefreshDisplays(false, true);

        private void ToggleEnableRecalcChartDataButton(bool isEnabled)
        {
            if (!RecalcChartDataButton.InvokeRequired)
            {
                RecalcChartDataButton.Enabled = isEnabled;
            }
            else
            {
                RecalcChartDataButton.Invoke(delegate { RecalcChartDataButton.Enabled = isEnabled; });
            }
        }

        private async void OnEventRefreshDisplays(bool writeChartInfo, bool writeLeaderboardInfo)
        {
            if (_currentState != State.Idle) return;
            _currentState = State.RunningTasks;
            _writeChartInfo = writeChartInfo;
            _writeLeaderboardInfo = writeLeaderboardInfo;
            ToggleEnableRecalcChartDataButton(false);
            await Task.Run(DisplayChartData).ConfigureAwait(false);
            _currentLoadingForm.OnLoadingFinished();
            ToggleEnableRecalcChartDataButton(true);
            _currentState = State.Idle;
        }

        private void OnValidatedChartNameInput(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                OnEventRefreshDisplays(true, true);
        }

        private void OnModifierCheckChange(object sender, ItemCheckEventArgs e)
        {
            var modifierString = ((Modifiers)e.Index).ToString();
            if (e.NewValue == CheckState.Unchecked && GetModifiersToShow.Any(modifierString.Contains))
            {
                GetModifiersToShow.Remove(modifierString);
                OnEventRefreshDisplays(false, true);
            }
            else if (e.NewValue == CheckState.Checked && !GetModifiersToShow.Any(modifierString.Contains))
            {
                GetModifiersToShow.Add(modifierString);
                OnEventRefreshDisplays(false, true);
            }
        }

        private void OnDiffGraphButtonClick(object sender, EventArgs e)
        {
            if (!_savedSelectedChart.Equals(default))
                new DiffVisualizerForm(_savedSelectedChart, 6).Show();
        }

        private void OnChartGraphButtonClick(object sender, EventArgs e)
        {
            if (!_savedSelectedChart.Equals(default))
                new ChartVisualizerForm(_savedSelectedChart, 6).Show();
        }

        public enum State
        {
            RunningTasks,
            Idle,
        }
        public enum Modifiers
        {
            NONE,
            EZ,
            HD,
            FL,
            MR
        }
    }
}