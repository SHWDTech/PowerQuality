using System.Configuration;
using System.Windows;

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
        }

        private void OnConfirm(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["serverAddr"].Value = AppConfig.ServerAddr = TxtServerAddr.Text;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            Close();
        }
    }
}
