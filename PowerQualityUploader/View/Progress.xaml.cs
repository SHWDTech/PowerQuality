using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace PowerQualityUploader.View
{
    /// <summary>
    /// Interaction logic for Progress.xaml
    /// </summary>
    public partial class Progress
    {
        private const int GwlStyle = -16;
        private const int WsSysmenu = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public Progress()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GwlStyle, GetWindowLong(hwnd, GwlStyle) & ~WsSysmenu);
        }

        public void UpdateProgressBar(double value)
        {
            Dispatcher.Invoke(() =>
            {
                BarUpload.Dispatcher.Invoke(() =>
                {
                    BarUpload.Value = value;
                });
            });
        }

        public void FinishUpload()
        {
            Dispatcher.Invoke(() =>
            {
                BtnConfirm.IsEnabled = true;
            });
        }

        public void BtnClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void UPdateProgressStage(string stage)
        {
            Dispatcher.Invoke(() =>
            {
                LblMessage.Content = stage;
            });
        }
    }
}
