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
            ChartNameInput = new TextBox();
            MinTTInput = new NumericUpDown();
            label2 = new Label();
            StrainMultInput = new NumericUpDown();
            AimMultLabel = new Label();
            label3 = new Label();
            label4 = new Label();
            StaminaMultInput = new NumericUpDown();
            label5 = new Label();
            EnduranceMultInput = new NumericUpDown();
            label6 = new Label();
            TapStaminaDivInput = new NumericUpDown();
            label7 = new Label();
            AimEnduranceDivInput = new NumericUpDown();
            label8 = new Label();
            AimStaminaDivInput = new NumericUpDown();
            label9 = new Label();
            TapEnduranceDivInput = new NumericUpDown();
            label10 = new Label();
            EZAimInput = new NumericUpDown();
            label11 = new Label();
            FLAimInput = new NumericUpDown();
            label12 = new Label();
            HDAimInput = new NumericUpDown();
            EZTapInput = new NumericUpDown();
            FLTapInput = new NumericUpDown();
            HDTapInput = new NumericUpDown();
            ModifierCheckboxInput = new CheckedListBox();
            DiffVisualizerButton = new Button();
            ChartVisualizerButton = new Button();
            ((System.ComponentModel.ISupportInitialize)MinTTInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)StrainMultInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)StaminaMultInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EnduranceMultInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TapStaminaDivInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AimEnduranceDivInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AimStaminaDivInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TapEnduranceDivInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EZAimInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FLAimInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HDAimInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EZTapInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FLTapInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HDTapInput).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            label1.Location = new Point(12, 102);
            label1.Name = "label1";
            label1.Size = new Size(126, 23);
            label1.TabIndex = 1;
            label1.Text = "Chart Name:";
            label1.TextAlign = ContentAlignment.MiddleRight;
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
            ChartViewerComboBox.Size = new Size(277, 23);
            ChartViewerComboBox.TabIndex = 2;
            ChartViewerComboBox.SelectedValueChanged += OnChartViewerSelectionChanged;
            // 
            // ChartInfoBox
            // 
            ChartInfoBox.BackColor = Color.Black;
            ChartInfoBox.BorderStyle = BorderStyle.None;
            ChartInfoBox.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ChartInfoBox.ForeColor = Color.White;
            ChartInfoBox.Location = new Point(295, 12);
            ChartInfoBox.Name = "ChartInfoBox";
            ChartInfoBox.ReadOnly = true;
            ChartInfoBox.Size = new Size(486, 598);
            ChartInfoBox.TabIndex = 5;
            ChartInfoBox.Text = "Place Holder";
            // 
            // ChartLeaderboardBox
            // 
            ChartLeaderboardBox.BackColor = Color.Black;
            ChartLeaderboardBox.BorderStyle = BorderStyle.None;
            ChartLeaderboardBox.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ChartLeaderboardBox.ForeColor = Color.White;
            ChartLeaderboardBox.Location = new Point(787, 12);
            ChartLeaderboardBox.Name = "ChartLeaderboardBox";
            ChartLeaderboardBox.ReadOnly = true;
            ChartLeaderboardBox.Size = new Size(896, 598);
            ChartLeaderboardBox.TabIndex = 6;
            ChartLeaderboardBox.Text = "Place Holder";
            // 
            // RecalcChartDataButton
            // 
            RecalcChartDataButton.AutoSize = true;
            RecalcChartDataButton.BackColor = Color.Black;
            RecalcChartDataButton.Enabled = false;
            RecalcChartDataButton.FlatStyle = FlatStyle.Flat;
            RecalcChartDataButton.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
            RecalcChartDataButton.ForeColor = Color.White;
            RecalcChartDataButton.Location = new Point(12, 67);
            RecalcChartDataButton.Name = "RecalcChartDataButton";
            RecalcChartDataButton.Size = new Size(109, 27);
            RecalcChartDataButton.TabIndex = 7;
            RecalcChartDataButton.Text = "Recalc Chart Data";
            RecalcChartDataButton.UseVisualStyleBackColor = false;
            RecalcChartDataButton.Click += OnRecalcChartDataClick;
            // 
            // ChartNameInput
            // 
            ChartNameInput.Location = new Point(144, 102);
            ChartNameInput.Name = "ChartNameInput";
            ChartNameInput.Size = new Size(145, 23);
            ChartNameInput.TabIndex = 8;
            ChartNameInput.KeyPress += OnValidatedChartNameInput;
            // 
            // MinTTInput
            // 
            MinTTInput.BackColor = Color.Black;
            MinTTInput.ForeColor = Color.White;
            MinTTInput.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            MinTTInput.Location = new Point(193, 131);
            MinTTInput.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            MinTTInput.Name = "MinTTInput";
            MinTTInput.Size = new Size(96, 23);
            MinTTInput.TabIndex = 12;
            MinTTInput.ValueChanged += OnValueBoxTextChanged;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            label2.Location = new Point(12, 131);
            label2.Name = "label2";
            label2.Size = new Size(175, 23);
            label2.TabIndex = 13;
            label2.Text = "Min TT:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // StrainMultInput
            // 
            StrainMultInput.BackColor = Color.Black;
            StrainMultInput.DecimalPlaces = 2;
            StrainMultInput.ForeColor = Color.White;
            StrainMultInput.Location = new Point(193, 160);
            StrainMultInput.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            StrainMultInput.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            StrainMultInput.Name = "StrainMultInput";
            StrainMultInput.Size = new Size(96, 23);
            StrainMultInput.TabIndex = 12;
            StrainMultInput.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            StrainMultInput.ValueChanged += OnValueBoxTextChanged;
            // 
            // AimMultLabel
            // 
            AimMultLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            AimMultLabel.Location = new Point(12, 160);
            AimMultLabel.Name = "AimMultLabel";
            AimMultLabel.Size = new Size(175, 23);
            AimMultLabel.TabIndex = 13;
            AimMultLabel.Text = "Strain Mult:";
            AimMultLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(12, 12);
            label3.Name = "label3";
            label3.Size = new Size(277, 23);
            label3.TabIndex = 14;
            label3.Text = "Chart Viewer";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            label4.Location = new Point(12, 183);
            label4.Name = "label4";
            label4.Size = new Size(175, 23);
            label4.TabIndex = 16;
            label4.Text = "Stamina Mult:";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // StaminaMultInput
            // 
            StaminaMultInput.BackColor = Color.Black;
            StaminaMultInput.DecimalPlaces = 2;
            StaminaMultInput.ForeColor = Color.White;
            StaminaMultInput.Location = new Point(193, 183);
            StaminaMultInput.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            StaminaMultInput.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            StaminaMultInput.Name = "StaminaMultInput";
            StaminaMultInput.Size = new Size(96, 23);
            StaminaMultInput.TabIndex = 15;
            StaminaMultInput.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            StaminaMultInput.ValueChanged += OnValueBoxTextChanged;
            // 
            // label5
            // 
            label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            label5.Location = new Point(12, 206);
            label5.Name = "label5";
            label5.Size = new Size(175, 23);
            label5.TabIndex = 18;
            label5.Text = "Endurance Mult:";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // EnduranceMultInput
            // 
            EnduranceMultInput.BackColor = Color.Black;
            EnduranceMultInput.DecimalPlaces = 2;
            EnduranceMultInput.ForeColor = Color.White;
            EnduranceMultInput.Location = new Point(193, 206);
            EnduranceMultInput.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            EnduranceMultInput.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            EnduranceMultInput.Name = "EnduranceMultInput";
            EnduranceMultInput.Size = new Size(96, 23);
            EnduranceMultInput.TabIndex = 17;
            EnduranceMultInput.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            EnduranceMultInput.ValueChanged += OnValueBoxTextChanged;
            // 
            // label6
            // 
            label6.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            label6.Location = new Point(12, 292);
            label6.Name = "label6";
            label6.Size = new Size(175, 23);
            label6.TabIndex = 24;
            label6.Text = "Tap Stamina Div:";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TapStaminaDivInput
            // 
            TapStaminaDivInput.BackColor = Color.Black;
            TapStaminaDivInput.DecimalPlaces = 2;
            TapStaminaDivInput.ForeColor = Color.White;
            TapStaminaDivInput.Location = new Point(193, 292);
            TapStaminaDivInput.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            TapStaminaDivInput.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            TapStaminaDivInput.Name = "TapStaminaDivInput";
            TapStaminaDivInput.Size = new Size(96, 23);
            TapStaminaDivInput.TabIndex = 23;
            TapStaminaDivInput.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            TapStaminaDivInput.ValueChanged += OnRecalcChartDataClick;
            // 
            // label7
            // 
            label7.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            label7.Location = new Point(12, 263);
            label7.Name = "label7";
            label7.Size = new Size(175, 23);
            label7.TabIndex = 22;
            label7.Text = "Aim Endurance Div:";
            label7.TextAlign = ContentAlignment.MiddleRight;
            // 
            // AimEnduranceDivInput
            // 
            AimEnduranceDivInput.BackColor = Color.Black;
            AimEnduranceDivInput.DecimalPlaces = 2;
            AimEnduranceDivInput.ForeColor = Color.White;
            AimEnduranceDivInput.Location = new Point(193, 263);
            AimEnduranceDivInput.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            AimEnduranceDivInput.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            AimEnduranceDivInput.Name = "AimEnduranceDivInput";
            AimEnduranceDivInput.Size = new Size(96, 23);
            AimEnduranceDivInput.TabIndex = 21;
            AimEnduranceDivInput.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            AimEnduranceDivInput.ValueChanged += OnRecalcChartDataClick;
            // 
            // label8
            // 
            label8.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            label8.Location = new Point(12, 240);
            label8.Name = "label8";
            label8.Size = new Size(175, 23);
            label8.TabIndex = 20;
            label8.Text = "Aim Stamina Div:";
            label8.TextAlign = ContentAlignment.MiddleRight;
            // 
            // AimStaminaDivInput
            // 
            AimStaminaDivInput.BackColor = Color.Black;
            AimStaminaDivInput.DecimalPlaces = 2;
            AimStaminaDivInput.ForeColor = Color.White;
            AimStaminaDivInput.Location = new Point(193, 240);
            AimStaminaDivInput.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            AimStaminaDivInput.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            AimStaminaDivInput.Name = "AimStaminaDivInput";
            AimStaminaDivInput.Size = new Size(96, 23);
            AimStaminaDivInput.TabIndex = 19;
            AimStaminaDivInput.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            AimStaminaDivInput.ValueChanged += OnRecalcChartDataClick;
            // 
            // label9
            // 
            label9.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            label9.Location = new Point(12, 315);
            label9.Name = "label9";
            label9.Size = new Size(175, 23);
            label9.TabIndex = 26;
            label9.Text = "Tap Endurance Div:";
            label9.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TapEnduranceDivInput
            // 
            TapEnduranceDivInput.BackColor = Color.Black;
            TapEnduranceDivInput.DecimalPlaces = 2;
            TapEnduranceDivInput.ForeColor = Color.White;
            TapEnduranceDivInput.Location = new Point(193, 315);
            TapEnduranceDivInput.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            TapEnduranceDivInput.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            TapEnduranceDivInput.Name = "TapEnduranceDivInput";
            TapEnduranceDivInput.Size = new Size(96, 23);
            TapEnduranceDivInput.TabIndex = 25;
            TapEnduranceDivInput.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            TapEnduranceDivInput.ValueChanged += OnRecalcChartDataClick;
            // 
            // label10
            // 
            label10.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            label10.Location = new Point(12, 393);
            label10.Name = "label10";
            label10.Size = new Size(126, 23);
            label10.TabIndex = 32;
            label10.Text = "EZ Mults:";
            label10.TextAlign = ContentAlignment.MiddleRight;
            // 
            // EZAimInput
            // 
            EZAimInput.BackColor = Color.Black;
            EZAimInput.DecimalPlaces = 2;
            EZAimInput.ForeColor = Color.White;
            EZAimInput.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            EZAimInput.Location = new Point(144, 393);
            EZAimInput.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            EZAimInput.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            EZAimInput.Name = "EZAimInput";
            EZAimInput.Size = new Size(64, 23);
            EZAimInput.TabIndex = 31;
            EZAimInput.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            EZAimInput.ValueChanged += OnValueBoxTextChanged;
            // 
            // label11
            // 
            label11.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            label11.Location = new Point(12, 370);
            label11.Name = "label11";
            label11.Size = new Size(126, 23);
            label11.TabIndex = 30;
            label11.Text = "FL Mults:";
            label11.TextAlign = ContentAlignment.MiddleRight;
            // 
            // FLAimInput
            // 
            FLAimInput.BackColor = Color.Black;
            FLAimInput.DecimalPlaces = 2;
            FLAimInput.ForeColor = Color.White;
            FLAimInput.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            FLAimInput.Location = new Point(144, 370);
            FLAimInput.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            FLAimInput.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            FLAimInput.Name = "FLAimInput";
            FLAimInput.Size = new Size(64, 23);
            FLAimInput.TabIndex = 29;
            FLAimInput.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            FLAimInput.ValueChanged += OnValueBoxTextChanged;
            // 
            // label12
            // 
            label12.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            label12.Location = new Point(12, 347);
            label12.Name = "label12";
            label12.Size = new Size(126, 23);
            label12.TabIndex = 28;
            label12.Text = "HD Mults:";
            label12.TextAlign = ContentAlignment.MiddleRight;
            // 
            // HDAimInput
            // 
            HDAimInput.BackColor = Color.Black;
            HDAimInput.DecimalPlaces = 2;
            HDAimInput.ForeColor = Color.White;
            HDAimInput.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            HDAimInput.Location = new Point(144, 347);
            HDAimInput.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            HDAimInput.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            HDAimInput.Name = "HDAimInput";
            HDAimInput.Size = new Size(64, 23);
            HDAimInput.TabIndex = 27;
            HDAimInput.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            HDAimInput.ValueChanged += OnValueBoxTextChanged;
            // 
            // EZTapInput
            // 
            EZTapInput.BackColor = Color.Black;
            EZTapInput.DecimalPlaces = 2;
            EZTapInput.ForeColor = Color.White;
            EZTapInput.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            EZTapInput.Location = new Point(214, 393);
            EZTapInput.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            EZTapInput.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            EZTapInput.Name = "EZTapInput";
            EZTapInput.Size = new Size(64, 23);
            EZTapInput.TabIndex = 35;
            EZTapInput.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            EZTapInput.ValueChanged += OnValueBoxTextChanged;
            // 
            // FLTapInput
            // 
            FLTapInput.BackColor = Color.Black;
            FLTapInput.DecimalPlaces = 2;
            FLTapInput.ForeColor = Color.White;
            FLTapInput.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            FLTapInput.Location = new Point(214, 370);
            FLTapInput.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            FLTapInput.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            FLTapInput.Name = "FLTapInput";
            FLTapInput.Size = new Size(64, 23);
            FLTapInput.TabIndex = 34;
            FLTapInput.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            FLTapInput.ValueChanged += OnValueBoxTextChanged;
            // 
            // HDTapInput
            // 
            HDTapInput.BackColor = Color.Black;
            HDTapInput.DecimalPlaces = 2;
            HDTapInput.ForeColor = Color.White;
            HDTapInput.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            HDTapInput.Location = new Point(214, 347);
            HDTapInput.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            HDTapInput.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            HDTapInput.Name = "HDTapInput";
            HDTapInput.Size = new Size(64, 23);
            HDTapInput.TabIndex = 33;
            HDTapInput.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            HDTapInput.ValueChanged += OnValueBoxTextChanged;
            // 
            // ModifierCheckboxInput
            // 
            ModifierCheckboxInput.BackColor = Color.Black;
            ModifierCheckboxInput.CheckOnClick = true;
            ModifierCheckboxInput.ForeColor = Color.White;
            ModifierCheckboxInput.FormattingEnabled = true;
            ModifierCheckboxInput.Items.AddRange(new object[] { "NONE", "EZ", "HD", "FL", "MR" });
            ModifierCheckboxInput.Location = new Point(214, 422);
            ModifierCheckboxInput.Name = "ModifierCheckboxInput";
            ModifierCheckboxInput.Size = new Size(64, 94);
            ModifierCheckboxInput.TabIndex = 36;
            // 
            // DiffVisualizerButton
            // 
            DiffVisualizerButton.AutoSize = true;
            DiffVisualizerButton.BackColor = Color.Black;
            DiffVisualizerButton.Enabled = false;
            DiffVisualizerButton.FlatStyle = FlatStyle.Flat;
            DiffVisualizerButton.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
            DiffVisualizerButton.ForeColor = Color.White;
            DiffVisualizerButton.Location = new Point(127, 67);
            DiffVisualizerButton.Name = "DiffVisualizerButton";
            DiffVisualizerButton.Size = new Size(73, 27);
            DiffVisualizerButton.TabIndex = 37;
            DiffVisualizerButton.Text = "Diff Graph";
            DiffVisualizerButton.UseVisualStyleBackColor = false;
            DiffVisualizerButton.Click += OnDiffGraphButtonClick;
            // 
            // ChartVisualizerButton
            // 
            ChartVisualizerButton.AutoSize = true;
            ChartVisualizerButton.BackColor = Color.Black;
            ChartVisualizerButton.Enabled = false;
            ChartVisualizerButton.FlatStyle = FlatStyle.Flat;
            ChartVisualizerButton.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
            ChartVisualizerButton.ForeColor = Color.White;
            ChartVisualizerButton.Location = new Point(206, 67);
            ChartVisualizerButton.Name = "ChartVisualizerButton";
            ChartVisualizerButton.Size = new Size(83, 27);
            ChartVisualizerButton.TabIndex = 38;
            ChartVisualizerButton.Text = "Chart Graph";
            ChartVisualizerButton.UseVisualStyleBackColor = false;
            ChartVisualizerButton.Click += OnChartGraphButtonClick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1695, 622);
            Controls.Add(ChartVisualizerButton);
            Controls.Add(DiffVisualizerButton);
            Controls.Add(ModifierCheckboxInput);
            Controls.Add(EZTapInput);
            Controls.Add(FLTapInput);
            Controls.Add(HDTapInput);
            Controls.Add(label10);
            Controls.Add(EZAimInput);
            Controls.Add(label11);
            Controls.Add(FLAimInput);
            Controls.Add(label12);
            Controls.Add(HDAimInput);
            Controls.Add(label9);
            Controls.Add(TapEnduranceDivInput);
            Controls.Add(label6);
            Controls.Add(TapStaminaDivInput);
            Controls.Add(label7);
            Controls.Add(AimEnduranceDivInput);
            Controls.Add(label8);
            Controls.Add(AimStaminaDivInput);
            Controls.Add(label5);
            Controls.Add(EnduranceMultInput);
            Controls.Add(label4);
            Controls.Add(StaminaMultInput);
            Controls.Add(label3);
            Controls.Add(AimMultLabel);
            Controls.Add(label2);
            Controls.Add(StrainMultInput);
            Controls.Add(MinTTInput);
            Controls.Add(ChartNameInput);
            Controls.Add(RecalcChartDataButton);
            Controls.Add(ChartLeaderboardBox);
            Controls.Add(ChartInfoBox);
            Controls.Add(ChartViewerComboBox);
            Controls.Add(label1);
            ForeColor = Color.White;
            Name = "MainForm";
            Text = "Local TTV6 Algo";
            Shown += OnFormShown;
            ((System.ComponentModel.ISupportInitialize)MinTTInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)StrainMultInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)StaminaMultInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)EnduranceMultInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)TapStaminaDivInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)AimEnduranceDivInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)AimStaminaDivInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)TapEnduranceDivInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)EZAimInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)FLAimInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)HDAimInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)EZTapInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)FLTapInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)HDTapInput).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private ComboBox ChartViewerComboBox;
        public RichTextBox ChartInfoBox;
        public RichTextBox ChartLeaderboardBox;
        private Button RecalcChartDataButton;
        private TextBox ChartNameInput;
        private NumericUpDown MinTTInput;
        private Label label2;
        private NumericUpDown StrainMultInput;
        private Label AimMultLabel;
        private Label label3;
        private Label label4;
        private NumericUpDown StaminaMultInput;
        private Label label5;
        private NumericUpDown EnduranceMultInput;
        private Label label6;
        private NumericUpDown TapStaminaDivInput;
        private Label label7;
        private NumericUpDown AimEnduranceDivInput;
        private Label label8;
        private NumericUpDown AimStaminaDivInput;
        private Label label9;
        private NumericUpDown TapEnduranceDivInput;
        private Label label10;
        private NumericUpDown EZAimInput;
        private Label label11;
        private NumericUpDown FLAimInput;
        private Label label12;
        private NumericUpDown HDAimInput;
        private NumericUpDown EZTapInput;
        private NumericUpDown FLTapInput;
        private NumericUpDown HDTapInput;
        private CheckedListBox ModifierCheckboxInput;
        private Button DiffVisualizerButton;
        private Button ChartVisualizerButton;
    }
}
