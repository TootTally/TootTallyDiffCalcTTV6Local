namespace TootTallyDiffCalcTTV6Local
{
    partial class DiffVisualizerForm
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
            Syncfusion.Windows.Forms.Chart.ChartToolBarSaveItem chartToolBarSaveItem1 = new Syncfusion.Windows.Forms.Chart.ChartToolBarSaveItem();
            Syncfusion.Windows.Forms.Chart.ChartToolBarCopyItem chartToolBarCopyItem1 = new Syncfusion.Windows.Forms.Chart.ChartToolBarCopyItem();
            Syncfusion.Windows.Forms.Chart.ChartToolBarPrintItem chartToolBarPrintItem1 = new Syncfusion.Windows.Forms.Chart.ChartToolBarPrintItem();
            Syncfusion.Windows.Forms.Chart.ChartToolBarPrintPreviewItem chartToolBarPrintPreviewItem1 = new Syncfusion.Windows.Forms.Chart.ChartToolBarPrintPreviewItem();
            Syncfusion.Windows.Forms.Chart.ChartToolBarSplitter chartToolBarSplitter1 = new Syncfusion.Windows.Forms.Chart.ChartToolBarSplitter();
            Syncfusion.Windows.Forms.Chart.ChartToolBarPaletteItem chartToolBarPaletteItem1 = new Syncfusion.Windows.Forms.Chart.ChartToolBarPaletteItem();
            Syncfusion.Windows.Forms.Chart.ChartToolBarStyleItem chartToolBarStyleItem1 = new Syncfusion.Windows.Forms.Chart.ChartToolBarStyleItem();
            Syncfusion.Windows.Forms.Chart.ChartToolBarTypeItem chartToolBarTypeItem1 = new Syncfusion.Windows.Forms.Chart.ChartToolBarTypeItem();
            Syncfusion.Windows.Forms.Chart.ChartToolBarSeries3DItem chartToolBarSeries3dItem1 = new Syncfusion.Windows.Forms.Chart.ChartToolBarSeries3DItem();
            Syncfusion.Windows.Forms.Chart.ChartToolBarShowLegendItem chartToolBarShowLegendItem1 = new Syncfusion.Windows.Forms.Chart.ChartToolBarShowLegendItem();
            DiffVisualizerGraph = new Syncfusion.Windows.Forms.Chart.ChartControl();
            chartTitle1 = new Syncfusion.Windows.Forms.Chart.ChartTitle();
            DiffVisualizerGraph.SuspendLayout();
            SuspendLayout();
            // 
            // DiffVisualizerGraph
            // 
            DiffVisualizerGraph.AddRandomSeries = false;
            DiffVisualizerGraph.AllowGradientPalette = true;
            DiffVisualizerGraph.AllowUserEditStyles = true;
            DiffVisualizerGraph.BackInterior = new Syncfusion.Drawing.BrushInfo(Color.FromArgb(64, 64, 64));
            DiffVisualizerGraph.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(Color.Transparent);
            DiffVisualizerGraph.ChartArea.BorderColor = Color.Black;
            DiffVisualizerGraph.ChartArea.CursorLocation = new Point(0, 0);
            DiffVisualizerGraph.ChartArea.CursorReDraw = false;
            DiffVisualizerGraph.ChartInterior = new Syncfusion.Drawing.BrushInfo(Color.DimGray);
            DiffVisualizerGraph.Dock = DockStyle.Fill;
            DiffVisualizerGraph.EnableXZooming = true;
            DiffVisualizerGraph.ForeColor = Color.White;
            DiffVisualizerGraph.ImprovePerformance = true;
            DiffVisualizerGraph.KeyZoom = true;
            // 
            // 
            // 
            DiffVisualizerGraph.Legend.Enabled = false;
            DiffVisualizerGraph.Legend.Location = new Point(1316, 92);
            DiffVisualizerGraph.Location = new Point(0, 0);
            DiffVisualizerGraph.Name = "DiffVisualizerGraph";
            DiffVisualizerGraph.NeedPerformance = true;
            DiffVisualizerGraph.PrimaryXAxis.DrawGrid = false;
            DiffVisualizerGraph.PrimaryXAxis.Font = new Font("Verdana", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DiffVisualizerGraph.PrimaryXAxis.IntervalType = Syncfusion.Windows.Forms.Chart.ChartDateTimeIntervalType.Seconds;
            DiffVisualizerGraph.PrimaryXAxis.LogLabelsDisplayMode = Syncfusion.Windows.Forms.Chart.LogLabelsDisplayMode.Default;
            DiffVisualizerGraph.PrimaryXAxis.Margin = true;
            DiffVisualizerGraph.PrimaryYAxis.Font = new Font("Verdana", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DiffVisualizerGraph.PrimaryYAxis.ForeColor = Color.White;
            DiffVisualizerGraph.PrimaryYAxis.GridLineType.BackColor = Color.FromArgb(224, 224, 224);
            DiffVisualizerGraph.PrimaryYAxis.GridLineType.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            DiffVisualizerGraph.PrimaryYAxis.GridLineType.ForeColor = Color.Silver;
            DiffVisualizerGraph.PrimaryYAxis.LogLabelsDisplayMode = Syncfusion.Windows.Forms.Chart.LogLabelsDisplayMode.Default;
            DiffVisualizerGraph.PrimaryYAxis.Margin = true;
            DiffVisualizerGraph.ScrollPrecision = 10000;
            DiffVisualizerGraph.Size = new Size(1395, 612);
            DiffVisualizerGraph.TabIndex = 1;
            DiffVisualizerGraph.Text = "Diff Visualizer";
            // 
            // 
            // 
            DiffVisualizerGraph.Title.Margin = 0;
            DiffVisualizerGraph.Title.Name = "Default";
            DiffVisualizerGraph.Titles.Add(DiffVisualizerGraph.Title);
            DiffVisualizerGraph.Titles.Add(chartTitle1);
            DiffVisualizerGraph.ToolBar.EnableDefaultItems = false;
            DiffVisualizerGraph.ToolBar.Items.Add(chartToolBarSaveItem1);
            DiffVisualizerGraph.ToolBar.Items.Add(chartToolBarCopyItem1);
            DiffVisualizerGraph.ToolBar.Items.Add(chartToolBarPrintItem1);
            DiffVisualizerGraph.ToolBar.Items.Add(chartToolBarPrintPreviewItem1);
            DiffVisualizerGraph.ToolBar.Items.Add(chartToolBarSplitter1);
            DiffVisualizerGraph.ToolBar.Items.Add(chartToolBarPaletteItem1);
            DiffVisualizerGraph.ToolBar.Items.Add(chartToolBarStyleItem1);
            DiffVisualizerGraph.ToolBar.Items.Add(chartToolBarTypeItem1);
            DiffVisualizerGraph.ToolBar.Items.Add(chartToolBarSeries3dItem1);
            DiffVisualizerGraph.ToolBar.Items.Add(chartToolBarShowLegendItem1);
            DiffVisualizerGraph.ZoomOutIncrement = 0.1D;
            DiffVisualizerGraph.ZoomType = Syncfusion.Windows.Forms.Chart.ZoomType.MouseWheelZooming;
            // 
            // chartTitle1
            // 
            chartTitle1.Font = new Font("Verdana", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chartTitle1.Margin = 0;
            chartTitle1.Name = "chartTitle1";
            chartTitle1.Text = "chartTitle1";
            // 
            // DiffVisualizerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1395, 612);
            Controls.Add(DiffVisualizerGraph);
            Name = "DiffVisualizerForm";
            Text = "DiffVisualizerForm";
            DiffVisualizerGraph.ResumeLayout(false);
            DiffVisualizerGraph.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.Windows.Forms.Chart.ChartControl DiffVisualizerGraph;
        private Syncfusion.Windows.Forms.Chart.ChartTitle chartTitle1;
    }
}