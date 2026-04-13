namespace TootTallyDiffCalcTTV6Local
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            ChartViewerComboBox = new ComboBox();
            ChartInfoBox = new RichTextBox();
            ChartLeaderboardBox = new RichTextBox();
            RecalcChartDataButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(212, 23);
            label1.TabIndex = 1;
            label1.Text = "Chart Viewer";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ChartViewerComboBox
            // 
            ChartViewerComboBox.BackColor = Color.FromArgb(224, 224, 224);
            ChartViewerComboBox.DropDownHeight = 212;
            ChartViewerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ChartViewerComboBox.FormattingEnabled = true;
            ChartViewerComboBox.IntegralHeight = false;
            ChartViewerComboBox.Location = new Point(12, 38);
            ChartViewerComboBox.Name = "ChartViewerComboBox";
            ChartViewerComboBox.Size = new Size(212, 23);
            ChartViewerComboBox.TabIndex = 2;
            ChartViewerComboBox.SelectedValueChanged += OnChartViewerSelectionChanged;
            // 
            // ChartInfoBox
            // 
            ChartInfoBox.BackColor = Color.Black;
            ChartInfoBox.BorderStyle = BorderStyle.None;
            ChartInfoBox.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ChartInfoBox.ForeColor = Color.White;
            ChartInfoBox.Location = new Point(238, 12);
            ChartInfoBox.Name = "ChartInfoBox";
            ChartInfoBox.ReadOnly = true;
            ChartInfoBox.Size = new Size(475, 598);
            ChartInfoBox.TabIndex = 5;
            ChartInfoBox.Text = "Place Holder";
            // 
            // ChartLeaderboardBox
            // 
            ChartLeaderboardBox.BackColor = Color.Black;
            ChartLeaderboardBox.BorderStyle = BorderStyle.None;
            ChartLeaderboardBox.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ChartLeaderboardBox.ForeColor = Color.White;
            ChartLeaderboardBox.Location = new Point(719, 12);
            ChartLeaderboardBox.Name = "ChartLeaderboardBox";
            ChartLeaderboardBox.ReadOnly = true;
            ChartLeaderboardBox.Size = new Size(792, 598);
            ChartLeaderboardBox.TabIndex = 6;
            ChartLeaderboardBox.Text = "Place Holder";
            // 
            // RecalcChartDataButton
            // 
            RecalcChartDataButton.AutoSize = true;
            RecalcChartDataButton.BackColor = Color.Black;
            RecalcChartDataButton.Enabled = false;
            RecalcChartDataButton.FlatStyle = FlatStyle.Flat;
            RecalcChartDataButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            RecalcChartDataButton.ForeColor = Color.White;
            RecalcChartDataButton.Location = new Point(12, 67);
            RecalcChartDataButton.Name = "RecalcChartDataButton";
            RecalcChartDataButton.Size = new Size(117, 27);
            RecalcChartDataButton.TabIndex = 7;
            RecalcChartDataButton.Text = "Recalc Chart Data";
            RecalcChartDataButton.UseVisualStyleBackColor = false;
            RecalcChartDataButton.Click += OnRecalcChartDataClick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1522, 622);
            Controls.Add(RecalcChartDataButton);
            Controls.Add(ChartLeaderboardBox);
            Controls.Add(ChartInfoBox);
            Controls.Add(ChartViewerComboBox);
            Controls.Add(label1);
            ForeColor = Color.White;
            Name = "MainForm";
            Text = "Local TTV6 Algo";
            Shown += OnFormShown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private ComboBox ChartViewerComboBox;
        public RichTextBox ChartInfoBox;
        public RichTextBox ChartLeaderboardBox;
        private Button RecalcChartDataButton;
    }
}
