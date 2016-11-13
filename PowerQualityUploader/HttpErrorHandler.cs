using System.Net;
using System.Windows;

namespace PowerQualityUploader
{
    public class HttpErrorHandler
    {
        public static void OnWebException(HttpStatusCode code)
        {
            switch (code)
            {
                    case HttpStatusCode.NotFound:
                    MessageBox.Show("访问服务器失败，请检查服务器地址设置或服务器运行状态。");
                    break;
            }
        }
    }
}
