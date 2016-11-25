using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PowerQualityUploader.Controller;

namespace PowerQualityUploader.View
{
    /// <summary>
    /// Interaction logic for RecoreLoader.xaml
    /// </summary>
    public partial class RecoreLoader
    {
        private const ushort SingleFileLine = 16384;

        private double _recordTick;

        private DateTime _startDateTime;

        private readonly Dictionary<Guid, Dictionary<string, string>> _records;
        public RecoreLoader()
        {
            InitializeComponent();
            SldUpload.Minimum = 0;
        }

        public RecoreLoader(Dictionary<Guid, Dictionary<string, string>> records) : this()
        {
            foreach (var record in records)
            {
                RecordNameList.Items.Add(new ListBoxItem() {Content = record.Value["RecordName"], Tag = record.Key});
            }

            _records = records;
        }

        private void ViewRecordDetail(object sender, RoutedEventArgs e)
        {
            var record = _records[Guid.Parse(((ListBoxItem) RecordNameList.SelectedItem).Tag.ToString())];
            TxtLineType.Text = FileUpLoader.ConfigDictionary[record["LineType"]];
            TxtPeroid.Text = $"{record["Period"]}ms";
            TxtDuration.Text = record["Duration"];
            TxtStartDateTime.Text = TxtUploadStartDate.Text = record["StartDateTime"];
            TxtEndDateTime.Text = record["EndDateTime"];
            _recordTick = 20 * 10000.0 / int.Parse(record["SampleRate"]);
            var recordFiles = Directory.GetFiles(record["Directory"], "*.HEX", SearchOption.AllDirectories);
            var lineCount = recordFiles.Length * SingleFileLine;
            SldUpload.Maximum = lineCount;
            _startDateTime = DateTime.Parse(record["StartDateTime"]);
            SldUpload.Value = 0;
        }

        private void SetStartDate(object sender, RoutedEventArgs e)
        {
            if (_startDateTime == DateTime.MinValue) return;
            TxtUploadStartDate.Text = $"{_startDateTime + new TimeSpan((long)(SldUpload.Value * _recordTick)): yyyy-MM-dd HH:mm:ss.fff}";
        }

        private void StartUpLoad(object sender, RoutedEventArgs e)
        {
            if (RecordNameList.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择一个记录！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var record = _records[Guid.Parse(((ListBoxItem)RecordNameList.SelectedItem).Tag.ToString())];
            var start = (int) SldUpload.Value;
            var endMark = new TimeSpan(0, int.Parse(TxtUploadLength.Text), 0).Ticks / _recordTick  + SldUpload.Value;
            var end = endMark > SldUpload.Maximum ? (int) SldUpload.Maximum : (int) endMark;
            var startDateTime = _startDateTime + new TimeSpan((long) (SldUpload.Value*_recordTick));
            var duration = new TimeSpan((long) ((end - start)*_recordTick));
            record["Duration"] = string.Format("{0:dd}d {0:hh}h {0:mm}m {0:ss}s {0:fff}ms", duration);
            record["RecordDuration"] = $"{duration:G}";
            record["EndDateTime"] = $"{startDateTime + duration: yyyy-MM-dd HH:mm:ss.fff}";

            var progress = new Progress { LblMessage = { Content = "正在上传文件。" } };
            Task.Factory.StartNew(() =>
            {
                FileUpLoader.UploadRecordFiles(record, progress, start, end);
            });
            progress.Owner = Application.Current.MainWindow;
            progress.ShowDialog();
        }

        private void StartPreView(object sender, RoutedEventArgs e)
        {
            if (RecordNameList.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择一个记录！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var record = _records[Guid.Parse(((ListBoxItem)RecordNameList.SelectedItem).Tag.ToString())];
            var wavePreview = new WavePreview(record) {Owner = Owner};
            Visibility = Visibility.Hidden;
            wavePreview.ShowDialog();
            Visibility = Visibility.Visible;
        }
    }
}
