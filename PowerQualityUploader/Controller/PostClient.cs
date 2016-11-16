using System;
using System.IO;
using System.Net;
using SHWDTech.Platform.Utility;

namespace PowerQualityUploader.Controller
{
    public class PostClient
    {
        private readonly HttpWebRequest _request;

        private Exception _requestExceptions;

        public string PostData { get; private set; }

        public PostClient(string requestUrl)
        {
            _request = (HttpWebRequest)WebRequest.Create(requestUrl);
            _request.ContentType = "application/json";
        }

        public string Post(string postData)
        {
            try
            {
                PostData = postData;
                _request.Method = "POST";
                using (var streamWriter = new StreamWriter(_request.GetRequestStream()))
                {
                    streamWriter.Write(PostData);
                    streamWriter.Flush();
                    streamWriter.Close();

                    var streamReader = ((HttpWebResponse) _request.GetResponse()).GetResponseStream();
                    return streamReader != null ? new StreamReader(streamReader).ReadToEnd() : string.Empty;
                }
            }
            catch (Exception ex)
            {
                _requestExceptions = ex;
                LogService.Instance.Error("HTTP POST请求失败。", ex);
                return "error";
            }
        }

        public string Get()
        {
            try
            {
                _request.Method = "GET";
                var streamReader = ((HttpWebResponse)_request.GetResponse()).GetResponseStream();
                return streamReader != null ? new StreamReader(streamReader).ReadToEnd() : string.Empty;
            }
            catch (Exception ex)
            {
                _requestExceptions = ex;
                LogService.Instance.Error("HTTP GET请求失败。", ex);
                return "error";
            }
        }

        public void SetParams(string paramData)
        {
            PostData = paramData;
        }

        public Exception LastException()
            => _requestExceptions;
    }
}
