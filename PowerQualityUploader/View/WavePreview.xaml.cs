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

        private const ushort SingleFileLine = 16384;

        private double SliderValue => SldTimeRange.Value;

        private readonly DateTime _startDateTime;

        private readonly double _recordTick;

        private readonly double _currentModel;

        private readonly double _voltageStep;

        private readonly double _currentRestore;

        private readonly double _voltageRestore;

        private double _previewSldValue;

        private int DataRange => int.Parse(((ComboBoxItem) CmbRange.SelectedItem).Tag.ToString());

        private int DataRangeEnd => (int) SldTimeRange.Value + DataRange;

        public WavePreview()
        {
            InitializeComponent();
        }

        public WavePreview(Dictionary<string, string> record) : this()
        {
            _record = record;
            _startDateTime = DateTime.Parse(_record["StartDateTime"]);
            _recordTick = 20 * 10000.0 / int.Parse(_record["SampleRate"]);
            _currentModel = double.Parse(record["CurrentModel"]);
            _voltageStep = double.Parse(record["VoltageStep"]);
            if (!double.TryParse(record["CurrentRestore"], out _currentRestore))
            {
                _currentRestore = 1;
            }
            if (!double.TryParse(record["VoltageRestore"], out _voltageRestore))
            {
                _voltageRestore = 1;
            }
            SldTimeRange.Minimum = 1;
            if (record["LineType"] != "34")
            {
                VolA.Content = "AB(V)";
                VolB.Content = "BC(V)";
                VolC.Content = "CA(V)";
                VolN.IsChecked = false;
                VolN.Visibility = Visibility.Hidden;
            }
            PrepareChart();
        }

        private void PrepareChart()
        {
            PvWavePreview.Model = new PlotModel
            {
                Title = "电压电流实时波形"
            };
            PvWavePreview.Model.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "HH:mm:ss.fff",
                Title = "Wave Date",
                MinorIntervalType = DateTimeIntervalType.Milliseconds,
                IntervalType = DateTimeIntervalType.Seconds,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None,
            });
            ResetDateText(null, null);
            var recordFiles = Directory.GetFiles(_record["Directory"], "*.HEX", SearchOption.AllDirectories);
            var lineCount = recordFiles.Length * SingleFileLine;
            SldTimeRange.Maximum = lineCount;
        }

        private void SliderValueChange(object sender, RoutedEventArgs e)
        {
            ResetDateText(sender, e);
        }

        private void ResetDateText(object sender, RoutedEventArgs e)
        {
            if (SldTimeRange == null) return;
            TxtStartTime.Text = $"{_startDateTime + new TimeSpan((long)(SldTimeRange.Value * _recordTick)): yyyy-MM-dd HH:mm:ss.fff}";
            TxtEndTime.Text = $"{_startDateTime + new TimeSpan((long)(_recordTick * DataRangeEnd)):yyyy-MM-dd HH:mm:ss.fff}";
        }

        private void StartTimeTextChanged(object sender, RoutedEventArgs e)
        {
            if (DateTime.TryParse(TxtStartTime.Text, out DateTime inputTime))
            {
                SldTimeRange.ValueChanged -= SliderValueChange;
                SldTimeRange.Value = (int)((inputTime - _startDateTime).Ticks / _recordTick);
                SldTimeRange.ValueChanged += SliderValueChange;
                TxtEndTime.Text = $"{_startDateTime + new TimeSpan((long)(_recordTick * DataRangeEnd)):yyyy-MM-dd HH:mm:ss.fff}";
                ResetDateText(sender, e);
            }
        }

        private void RefreashChartOnChannelChange(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < SpCheck.Children.Count; i++)
            {
                var box = (CheckBox) SpCheck.Children[i];
                box.IsEnabled = false;
            }
            ResfreashChart(sender, e);
            for (var i = 0; i < SpCheck.Children.Count; i++)
            {
                var box = (CheckBox)SpCheck.Children[i];
                box.IsEnabled = true;
            }
        }

        private void ResfreashChart(object sender, RoutedEventArgs e)
        {
            if ((int) SliderValue == (int) SldTimeRange.Maximum) return;
            PvWavePreview.Model.Series.Clear();
            foreach (var child in SpCheck.Children)
            {
                var box = child as CheckBox;
                if (box != null && box.IsChecked == true)
                {
                    PvWavePreview.Model.Series.Add(new LineSeries {Title = box.Content.ToString()});
                }
            }
            var startLine = (int)SliderValue;
            var endLine = startLine + DataRange;
            if (endLine > SldTimeRange.Maximum)
            {
                endLine = (int) SldTimeRange.Maximum;
            }
            var currentFileIndex = startLine / SingleFileLine;
            var offset = (startLine % SingleFileLine);
            var files = Directory.GetFiles(_record["Directory"], "*.HEX", SearchOption.AllDirectories);
            var currentLine = startLine;
            var currentFile = files[currentFileIndex];
            var reader = new BinaryReader(File.Open(currentFile, FileMode.Open));
            reader.BaseStream.Seek(offset * 16, SeekOrigin.Begin);
            var buffer = new byte[16];
            while (currentLine < endLine)
            {
                if (reader.BaseStream.Position >= reader.BaseStream.Length)
                {
                    reader.Dispose();
                    currentFileIndex++;
                    currentFile = files[currentFileIndex];
                    offset = 0;
                    reader = new BinaryReader(File.Open(currentFile, FileMode.Open));
                    reader.BaseStream.Seek(offset * 16, SeekOrigin.Begin);
                }
                reader.Read(buffer, 0, 16);
                UpdatePowerData(buffer, currentLine);
                currentLine++;
            }
            reader.Dispose();
            PvWavePreview.InvalidatePlot();
        }

        private void UpdatePowerData(byte[] fileBytes, int currentLine)
        {
            var seriesIndex = 0;
            for (var i = 0; i < SpCheck.Children.Count; i++)
            {
                var box = (CheckBox)SpCheck.Children[i];
                if (box.IsChecked == false) continue;
                if (int.Parse(box.Tag.ToString()) < 4)
                {
                    ((LineSeries) PvWavePreview.Model.Series[seriesIndex]).Points.Add(
                        new DataPoint(DateTimeAxis.ToDouble(_startDateTime.AddTicks((long) (currentLine*_recordTick))),
                            (Globals.BytesToInt16(fileBytes, i * 2, false) / 32768.0d * 5.0 - 0.262) * _currentModel * _currentRestore));
                }
                else
                {
                    ((LineSeries)PvWavePreview.Model.Series[seriesIndex]).Points.Add(
                         new DataPoint(DateTimeAxis.ToDouble(_startDateTime.AddTicks((long)(currentLine * _recordTick))),
                             -Globals.BytesToInt16(fileBytes, i * 2, false) / 32768.0d * 5.0 * _voltageStep * _voltageRestore));
                }
                seriesIndex++;
            }
        }

        private void SliderMouseDown(object sender, RoutedEventArgs e)
        {
            _previewSldValue = SldTimeRange.Value;
        }

        private void SliderMouseUp(object sender, RoutedEventArgs e)
        {
            if ((int) _previewSldValue != (int) SldTimeRange.Value)
            {
                ResfreashChart(sender, e);
            }
        }
    }
}
