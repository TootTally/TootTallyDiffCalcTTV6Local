using Syncfusion.Windows.Forms.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TootTallyDiffCalcTTV6Local
{
    public partial class ChartVisualizerForm : Form
    {
        private Chart _chart;
        private int _speedIndex;
        public ChartVisualizerForm(Chart c, int speedIndex = 2)
        {
            _chart = c;
            _speedIndex = speedIndex;
            InitializeComponent();
            this.Text = _chart.shortName;
            ChartVisualizerGraph.Title.Text = _chart.shortName;
            ChartVisualizerGraph.ShowLegend = false;
            var noteSerie = new ChartSeries("Note Series", ChartSeriesType.Scatter);
            //noteSerie.Style.Symbol = ChartSymbolInfo.Default;
            noteSerie.Style.Symbol.Shape = ChartSymbolShape.Circle;
            noteSerie.Style.Symbol.Color = Color.DeepPink;
            noteSerie.Style.Symbol.Border = new ChartLineInfo() 
            { 
                Width = 2,
                Color = Color.White 
            };
            var bodySerie = new ChartSeries("Body Series", ChartSeriesType.Line);
            bodySerie.Style.Border = new ChartLineInfo()
            {
                Width = 5,
                Color = Color.DeepSkyBlue
            };
            for (int i = 1; i < _chart.notesArray.Length; i++)
            {
                var n = _chart.notesArray[i];
                noteSerie.Points.Add(n.position, n.pitchStart);
                bodySerie.Points.Add(n.position, n.pitchStart);
                bodySerie.Points.Add(n.position + n.length, n.pitchEnd);
                if (!n.isSlider)
                    bodySerie.Points.Add(n.position + n.length + .001, double.NaN);
            }
            ChartVisualizerGraph.Series.Add(noteSerie);
            ChartVisualizerGraph.Series.Add(bodySerie);
            ChartVisualizerGraph.ZoomFactorX = 2f / _chart.songLength;
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            var convertedPoint = ChartVisualizerGraph.ChartArea.GetValueByPoint(e.Location);
            Trace.WriteLine($"Clicked at {convertedPoint}");
            //Note note = _chart.notesArray.Where(x => MathF.Pow(x.position - (float)convertedPoint.X, 2) + MathF.Pow(x.pitchStart - (float)convertedPoint.YValues[0], 2) <= 10);
            var note = _chart.notesArray.Select(note => (MathF.Pow(note.position - (float)convertedPoint.X, 2) + MathF.Pow(note.pitchStart - (float)convertedPoint.YValues[0], 2), note))
                .Where(x => x.Item1 <= 7.5)
                .Order().FirstOrDefault().note;

            if (!note.Equals(default(Note)))
            {
                var index = Array.IndexOf(_chart.notesArray, note);
                var extraData = _chart.performances.extraDataVectorMatrix[_speedIndex][index];
                Trace.WriteLine($"Note {note.position} was found: Start:{note.pitchStart} - Length:{note.length} - End:{note.pitchEnd} - Slide:{note.isSlider}");
                Trace.WriteLine($"Extra Data ------------------------------------\n" +
                                $"Distance from prev:{extraData.distanceFromPreviousNote}\n" +
                                $"Distance from next:{extraData.distanceToNextNote}\n" +
                                $"TimeDelt from prev:{extraData.timingFromPreviousNote}\n" +
                                $"TimeDelt from next:{extraData.timingToNextNote}\n" +
                                $"Prev note position:{extraData.prevNotePos}\n" +
                                $"Next note position:{extraData.nextNotePos}\n" +
                                $"Aim -- Strain: {extraData.aimStrain} - Stamina: {extraData.aimSta} - Endurance: {extraData.aimEnd}\n" +
                                $"Tap -- Strain: {extraData.tapStrain} - Stamina: {extraData.tapSta} - Endurance: {extraData.tapEnd}\n" +
                                $"WeightSum: {extraData.weightSum}");
            }
        }
    }
}
