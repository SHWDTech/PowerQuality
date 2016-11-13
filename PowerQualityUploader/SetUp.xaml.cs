using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace PowerQualityUploader
{
    /// <summary>
    /// Interaction logic for SetUp.xaml
    /// </summary>
    public partial class SetUp
    {
        public SetUp()
        {
            InitializeComponent();
            ViewConfig();
        }

        private void ViewConfig()
        {
            TxtServerAddr.Text = AppConfig.ServerAddr;
            TxtMaxThread.Text = $"{AppConfig.MaxUploadThread}";
        }

        private void OnConfirm(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["serverAddr"].Value = AppConfig.ServerAddr = TxtServerAddr.Text;
            AppConfig.MaxUploadThread = int.Parse(TxtMaxThread.Text);
            config.AppSettings.Settings["maxUploadThread"].Value = AppConfig.MaxUploadThread.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            Close();
        }

        private void TextNumCheck(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox)) return;
            var box = (TextBox) sender;
            int num;
            if (int.TryParse(box.Text, out num)) return;
            num = 5;
            box.Text = $"{num}";
        }
    }
}
