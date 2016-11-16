using System;
using System.Collections.Generic;
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
        private readonly Dictionary<Guid, Dictionary<string, string>> _records;
        public RecoreLoader()
        {
            InitializeComponent();
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
            TxtStartDateTime.Text = record["StartDateTime"];
            TxtEndDateTime.Text = record["EndDateTime"];
        }

        private void StartUpLoad(object sender, RoutedEventArgs e)
        {
            if (RecordNameList.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择一个记录！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var record = _records[Guid.Parse(((ListBoxItem)RecordNameList.SelectedItem).Tag.ToString())];

            var progress = new Progress { LblMessage = { Content = "正在上传文件。" } };
            Task.Factory.StartNew(() =>
            {
                FileUpLoader.UploadRecordFiles(record, progress);
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
