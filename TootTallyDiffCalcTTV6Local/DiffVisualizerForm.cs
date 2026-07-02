using Syncfusion.Windows.Forms.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TootTallyDiffCalcTTV6Local
{
    public partial class DiffVisualizerForm : Form
    {
        private Chart _chart;
        public DiffVisualizerForm(Chart chart, int speedIndex = 2)
        {
            _chart = chart;
            InitializeComponent();
            DiffVisualizerGraph.Titles[1].Text = _chart.shortName;
            DiffVisualizerGraph.Series.Clear();
            var aimSeries = new ChartSeries("Aim Rating", ChartSeriesType.Line);
            aimSeries.Style.Border = new ChartLineInfo()
            {
                Width = 2,
                Color = Color.Pink
            };
            var aimStaSeries = new ChartSeries("Aim Stamina", ChartSeriesType.Line);
            aimStaSeries.Style.Border = new ChartLineInfo()
            {
                Width = 2,
                Color = Color.Red
            };
            var aimEndSeries = new ChartSeries("Aim Endurance", ChartSeriesType.Line);
            aimEndSeries.Style.Border = new ChartLineInfo()
            {
                Width = 2,
                Color = Color.DarkRed
            };

            var tapSeries = new ChartSeries("Tap Rating", ChartSeriesType.Line);
            tapSeries.Style.Border = new ChartLineInfo()
            {
                Width = 2,
                Color = Color.Cyan
            };
            var tapStaSeries = new ChartSeries("Tap Stamina", ChartSeriesType.Line);
            tapStaSeries.Style.Border = new ChartLineInfo()
            {
                Width = 2,
                Color = Color.Blue
            };
            var tapEndSeries = new ChartSeries("Tap Endurance", ChartSeriesType.Line);
            tapEndSeries.Style.Border = new ChartLineInfo()
            {
                Width = 2,
                Color = Color.DarkBlue
            };

            var starSeries = new ChartSeries("Star Rating", ChartSeriesType.Line);
            starSeries.Style.Border = new ChartLineInfo()
            {
                Width = 2,
                Color = Color.Yellow
            };

            for (int i = 0; i < _chart.performances.aimPerfMatrix[speedIndex].Length; i++)
            {
                var time = _chart.performances.aimPerfMatrix[speedIndex][i].time;
                aimSeries.Points.Add(time, _chart.performances.aimPerfMatrix[speedIndex][i].strain);
                aimStaSeries.Points.Add(time, _chart.performances.aimPerfMatrix[speedIndex][i].stamina);
                aimEndSeries.Points.Add(time, _chart.performances.aimPerfMatrix[speedIndex][i].endurance);
                tapSeries.Points.Add(time, _chart.performances.tapPerfMatrix[speedIndex][i].strain);
                tapStaSeries.Points.Add(time, _chart.performances.tapPerfMatrix[speedIndex][i].stamina);
                tapEndSeries.Points.Add(time, _chart.performances.tapPerfMatrix[speedIndex][i].endurance);
                starSeries.Points.Add(time, 
                    (_chart.performances.aimPerfMatrix[speedIndex][i].strain + _chart.performances.aimPerfMatrix[speedIndex][i].stamina + _chart.performances.aimPerfMatrix[speedIndex][i].endurance + 
                    _chart.performances.tapPerfMatrix[speedIndex][i].strain + _chart.performances.tapPerfMatrix[speedIndex][i].stamina + _chart.performances.tapPerfMatrix[speedIndex][i].endurance) / 2f);
            }

            DiffVisualizerGraph.Series.Add(aimSeries);
            DiffVisualizerGraph.Series.Add(aimStaSeries);
            DiffVisualizerGraph.Series.Add(aimEndSeries);
            DiffVisualizerGraph.Series.Add(tapSeries);
            DiffVisualizerGraph.Series.Add(tapStaSeries);
            DiffVisualizerGraph.Series.Add(tapEndSeries);
            DiffVisualizerGraph.Series.Add(starSeries);
        }  
    }
}
