using System.Configuration;
using System.Windows;

namespace PowerQualityUploader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            FileUpLoader.LoadRequirements();
            FileUpLoader.ServerAddr = ConfigurationManager.AppSettings["serverAddr"];
            base.OnStartup(e);
        }
    }
}
