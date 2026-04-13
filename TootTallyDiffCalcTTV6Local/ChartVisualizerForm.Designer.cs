namespace TootTallyDiffCalcTTV6Local
{
    partial class ChartVisualizerForm
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
            ChartVisualizerGraph = new Syncfusion.Windows.Forms.Chart.ChartControl();
            SuspendLayout();
            // 
            // ChartVisualizerGraph
            // 
            ChartVisualizerGraph.AddRandomSeries = false;
            ChartVisualizerGraph.AllowGradientPalette = true;
            ChartVisualizerGraph.AllowUserEditStyles = true;
            ChartVisualizerGraph.BackInterior = new Syncfusion.Drawing.BrushInfo(Color.Transparent);
            ChartVisualizerGraph.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(Color.Transparent);
            ChartVisualizerGraph.ChartArea.BorderColor = Color.Black;
            ChartVisualizerGraph.ChartArea.CursorLocation = new Point(0, 0);
            ChartVisualizerGraph.ChartArea.CursorReDraw = false;
            ChartVisualizerGraph.ChartInterior = new Syncfusion.Drawing.BrushInfo(Color.DimGray);
            ChartVisualizerGraph.Dock = DockStyle.Fill;
            ChartVisualizerGraph.EnableXZooming = true;
            ChartVisualizerGraph.ForeColor = Color.White;
            ChartVisualizerGraph.ImprovePerformance = true;
            ChartVisualizerGraph.KeyZoom = true;
            // 
            // 
            // 
            ChartVisualizerGraph.Legend.Enabled = false;
            ChartVisualizerGraph.Legend.Location = new Point(1303, 75);
            ChartVisualizerGraph.Location = new Point(0, 0);
            ChartVisualizerGraph.Name = "ChartVisualizerGraph";
            ChartVisualizerGraph.NeedPerformance = true;
            ChartVisualizerGraph.PrimaryXAxis.DrawGrid = false;
            ChartVisualizerGraph.PrimaryXAxis.Font = new Font("Verdana", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ChartVisualizerGraph.PrimaryXAxis.IntervalType = Syncfusion.Windows.Forms.Chart.ChartDateTimeIntervalType.Seconds;
            ChartVisualizerGraph.PrimaryXAxis.LogLabelsDisplayMode = Syncfusion.Windows.Forms.Chart.LogLabelsDisplayMode.Default;
            ChartVisualizerGraph.PrimaryXAxis.Margin = true;
            ChartVisualizerGraph.PrimaryYAxis.Font = new Font("Verdana", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ChartVisualizerGraph.PrimaryYAxis.ForeColor = Color.White;
            ChartVisualizerGraph.PrimaryYAxis.GridLineType.BackColor = Color.FromArgb(224, 224, 224);
            ChartVisualizerGraph.PrimaryYAxis.GridLineType.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            ChartVisualizerGraph.PrimaryYAxis.GridLineType.ForeColor = Color.Silver;
            ChartVisualizerGraph.PrimaryYAxis.LogLabelsDisplayMode = Syncfusion.Windows.Forms.Chart.LogLabelsDisplayMode.Default;
            ChartVisualizerGraph.PrimaryYAxis.Margin = true;
            ChartVisualizerGraph.PrimaryYAxis.Range = new Syncfusion.Windows.Forms.Chart.MinMaxInfo(-192.5D, 192.5D, 13.75D);
            ChartVisualizerGraph.PrimaryYAxis.RangeType = Syncfusion.Windows.Forms.Chart.ChartAxisRangeType.Set;
            ChartVisualizerGraph.ScrollPrecision = 10000;
            ChartVisualizerGraph.Size = new Size(1382, 618);
            ChartVisualizerGraph.TabIndex = 0;
            ChartVisualizerGraph.Text = "Chart Visualizer";
            // 
            // 
            // 
            ChartVisualizerGraph.Title.Name = "Default";
            ChartVisualizerGraph.Titles.Add(ChartVisualizerGraph.Title);
            ChartVisualizerGraph.ZoomOutIncrement = 0.1D;
            ChartVisualizerGraph.ZoomType = Syncfusion.Windows.Forms.Chart.ZoomType.MouseWheelZooming;
            ChartVisualizerGraph.MouseClick += OnMouseClick;
            // 
            // ChartVisualizerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1382, 618);
            Controls.Add(ChartVisualizerGraph);
            Name = "ChartVisualizerForm";
            Text = "ChartVisualizerForm";
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.Windows.Forms.Chart.ChartControl ChartVisualizerGraph;
    }
}