using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SHWDTech.Platform.Utility;

namespace PowerQualityUploader.View
{
    /// <summary>
    /// Interaction logic for WavePreview.xaml
    /// </summary>
    public partial class WavePreview
    {
        private readonly Dictionary<string, string> _record;

        private const ushort SingleFileLine = 16385;

        private double _sliderMaxValue;

        private double _sliderMinValue;

        private TextBox _currentTimeBox;

        private readonly DateTime _startDateTime;

        private readonly double _recordTick;

        private readonly double _currentModel;

        private readonly double _voltageStep;

        private readonly double _currentRestore;

        private readonly double _voltageRestore;

        public WavePreview()
        {
            InitializeComponent();
        }

        public WavePreview(Dictionary<string, string> record) : this()
        {
            _record = record;
            _startDateTime = DateTime.Parse(_record["StartDateTime"]);
            _recordTick = 20 * 10000.0 / int.Parse(_record["SampleRate"]);
            _currentModel = double.Parse(record["CurrentModal"]);
            _voltageStep = double.Parse(record["VoltageStep"]);
            _currentRestore = double.Parse(record["CurrentRestore"]);
            _voltageRestore = double.Parse(record["VoltageRestore"]);
            PrepareChart();
        }

        private void PrepareChart()
        {
            PvWavePreview.Model = new PlotModel();
            PvWavePreview.Model.Series.Add(new LineSeries());
            TxtStartTime.Text = _record["StartDateTime"];
            TxtEndTime.Text = _record["EndDateTime"];
            var recordFiles = Directory.GetFiles(_record["Directory"], "*.CSV", SearchOption.AllDirectories);
            var lineCount = recordFiles.Length * SingleFileLine;
            SldTimeRange.Maximum = _sliderMinValue = lineCount;
            _currentTimeBox = TxtStartTime;
        }

        private void FocusOnTime(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox)) return;
            var txtBox = (TextBox)sender;
            LblSlider.Content = txtBox.Name == "TxtStartTime" ? "开始时间" : "结束时间";
            SldTimeRange.Value = txtBox.Name == "TxtStartTime" ? _sliderMaxValue : _sliderMinValue;
            _currentTimeBox = txtBox;
        }

        private void AdjustTextTimeRange(object sender, RoutedEventArgs e)
        {
            if (!SldTimeRange.IsFocused) return;
            _currentTimeBox.Text = $"{_startDateTime + new TimeSpan((long)(SldTimeRange.Value * _recordTick)): yyyy-MM-dd HH:mm:ss fff}";
            if (_currentTimeBox.Name == "TxtStartTime")
            {
                _sliderMaxValue = SldTimeRange.Value;
            }
            else
            {
                _sliderMinValue = SldTimeRange.Value;
            }
        }

        private void ResfreashChart(object sender, RoutedEventArgs e)
        {
            PvWavePreview.Model.Series.Clear();
            foreach (var child in SpCheck.Children)
            {
                var box = child as CheckBox;
                if (box != null && box.IsChecked == true)
                {
                    PvWavePreview.Model.Series.Add(new LineSeries());
                }
            }
            var startLine = _sliderMaxValue < _sliderMinValue ? (int)_sliderMaxValue : (int)_sliderMinValue;
            var endLine = _sliderMaxValue < _sliderMinValue ? (int)_sliderMinValue : (int)_sliderMaxValue;
            var currentFileIndex = startLine / SingleFileLine;
            var offset = (startLine % SingleFileLine);
            var files = Directory.GetFiles(_record["Directory"], "*.CSV", SearchOption.AllDirectories);
            var currentLine = startLine + 1;
            while (currentLine < endLine)
            {
                var currentFile = files[currentFileIndex];
                using (var reader = new BinaryReader(File.Open(currentFile, FileMode.Open)))
                {
                    var buffer = new byte[16];
                    reader.BaseStream.Seek(offset * 16, SeekOrigin.Begin);
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        reader.Read(buffer, 0, 16);
                        UpdatePowerData(buffer, currentLine);
                        currentLine++;
                    }
                    currentFileIndex++;
                    offset = 0;
                }
            }

            PvWavePreview.InvalidatePlot();
        }

        private void UpdatePowerData(byte[] fileBytes, int currentLine)
        {
            for (var i = 0; i < SpCheck.Children.Count; i++)
            {
                var box = (CheckBox)SpCheck.Children[i];
                if (box.IsChecked == false) continue;
                if (int.Parse(box.Tag.ToString()) < 4)
                {
                    ((LineSeries) PvWavePreview.Model.Series[i]).Points.Add(
                        new DataPoint(DateTimeAxis.ToDouble(_startDateTime.AddTicks((long) (currentLine*_recordTick))),
                            Globals.BytesToInt16(fileBytes, 0, false) / 32768.0d * 5.0 * _currentModel * _currentRestore));
                }
                else
                {
                    ((LineSeries)PvWavePreview.Model.Series[i]).Points.Add(
                         new DataPoint(DateTimeAxis.ToDouble(_startDateTime.AddTicks((long)(currentLine * _recordTick))),
                             -Globals.BytesToInt16(fileBytes, 0, false) / 32768.0d * 5.0 * _voltageStep * _voltageRestore));
                }
            }
        }
    }
}
