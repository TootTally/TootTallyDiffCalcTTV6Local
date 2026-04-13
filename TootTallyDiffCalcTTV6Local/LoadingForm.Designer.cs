namespace TootTallyDiffCalcTTV6Local
{
    partial class LoadingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ProgressBar = new ProgressBar();
            PercentLabel = new Label();
            DescriptionLabel = new Label();
            ElapsedLabel = new Label();
            DescriptionHistoryBox = new RichTextBox();
            SuspendLayout();
            // 
            // ProgressBar
            // 
            ProgressBar.Location = new Point(12, 106);
            ProgressBar.Maximum = 10000;
            ProgressBar.Name = "ProgressBar";
            ProgressBar.Size = new Size(605, 15);
            ProgressBar.Step = 100;
            ProgressBar.TabIndex = 0;
            // 
            // PercentLabel
            // 
            PercentLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PercentLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            PercentLabel.ForeColor = Color.White;
            PercentLabel.ImageAlign = ContentAlignment.MiddleLeft;
            PercentLabel.Location = new Point(12, 9);
            PercentLabel.Name = "PercentLabel";
            PercentLabel.Size = new Size(605, 38);
            PercentLabel.TabIndex = 1;
            PercentLabel.Text = "100.00%";
            PercentLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DescriptionLabel
            // 
            DescriptionLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DescriptionLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DescriptionLabel.ForeColor = Color.White;
            DescriptionLabel.ImageAlign = ContentAlignment.MiddleLeft;
            DescriptionLabel.Location = new Point(12, 48);
            DescriptionLabel.Name = "DescriptionLabel";
            DescriptionLabel.Size = new Size(605, 38);
            DescriptionLabel.TabIndex = 2;
            DescriptionLabel.Text = "Loading something...";
            DescriptionLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ElapsedLabel
            // 
            ElapsedLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ElapsedLabel.BackColor = Color.Transparent;
            ElapsedLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ElapsedLabel.ForeColor = Color.White;
            ElapsedLabel.ImageAlign = ContentAlignment.MiddleLeft;
            ElapsedLabel.Location = new Point(12, 86);
            ElapsedLabel.Name = "ElapsedLabel";
            ElapsedLabel.Size = new Size(605, 17);
            ElapsedLabel.TabIndex = 2;
            ElapsedLabel.Text = "0:00";
            ElapsedLabel.TextAlign = ContentAlignment.BottomRight;
            // 
            // DescriptionHistoryBox
            // 
            DescriptionHistoryBox.BackColor = Color.Gray;
            DescriptionHistoryBox.BorderStyle = BorderStyle.None;
            DescriptionHistoryBox.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DescriptionHistoryBox.ForeColor = Color.White;
            DescriptionHistoryBox.Location = new Point(12, 127);
            DescriptionHistoryBox.Name = "DescriptionHistoryBox";
            DescriptionHistoryBox.ReadOnly = true;
            DescriptionHistoryBox.Size = new Size(605, 118);
            DescriptionHistoryBox.TabIndex = 3;
            DescriptionHistoryBox.Text = "Loading something...\nLoading something...\nLoading something...\nLoading something...\nLoading something...\nLoading something...\nLoading something...\nLoading something...\nLoading something...";
            // 
            // LoadingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(629, 257);
            Controls.Add(DescriptionHistoryBox);
            Controls.Add(ElapsedLabel);
            Controls.Add(DescriptionLabel);
            Controls.Add(PercentLabel);
            Controls.Add(ProgressBar);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximumSize = new Size(645, 296);
            MinimumSize = new Size(645, 296);
            Name = "LoadingForm";
            Text = "LoadingForm";
            ResumeLayout(false);
        }

        #endregion

        private ProgressBar ProgressBar;
        private Label PercentLabel;
        private Label DescriptionLabel;
        private Label ElapsedLabel;
        private RichTextBox DescriptionHistoryBox;
    }
}