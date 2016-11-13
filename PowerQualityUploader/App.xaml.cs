using System;
using System.Configuration;
using System.Windows;
using System.Windows.Threading;
using SHWDTech.Platform.Utility;

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
            AppConfig.ServerAddr = ConfigurationManager.AppSettings["serverAddr"];
            AppConfig.MaxUploadThread = int.Parse(ConfigurationManager.AppSettings["maxUploadThread"]);
            base.OnStartup(e);
        }

        /// <summary>
        /// 未处理异常捕获器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            LogService.Instance.Fatal("未处理异常。", (Exception)e.ExceptionObject);
            if (e.IsTerminating)
            {
                MessageBox.Show("系统运行出现严重错误！");
            }
        }

        protected virtual void AppUnhandleExceptionHandler(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            LogService.Instance.Fatal("未处理异常。", e.Exception);

            e.Handled = true;
        }
    }
}
