using System.Windows;
using System.Windows.Forms;

namespace PowerQualityUploader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadFlashRecord(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.OK) return;
            var records = FileUpLoader.LoadRecord(dialog.SelectedPath);
            var recordList = new RecoreLoader(records) {Owner = this};
            recordList.ShowDialog();
        }

        private void OpenSetUp(object sender, RoutedEventArgs e)
        {
            var setUp = new SetUp {Owner = this};
            setUp.ShowDialog();
        }
    }
}
