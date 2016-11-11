using System.IO;
using System.Net;

namespace PowerQualityUploader
{
    public class PostClient
    {
        private readonly HttpWebRequest _request;

        public PostClient(string requestUrl)
        {
            _request = (HttpWebRequest)WebRequest.Create(requestUrl);
            _request.ContentType = "application/json";
        }

        public string Post(string postData)
        {
            _request.Method = "POST";
            using (var streamWriter = new StreamWriter(_request.GetRequestStream()))
            {
                streamWriter.Write(postData);
                streamWriter.Flush();
                streamWriter.Close();

                var streamReader = ((HttpWebResponse) _request.GetResponse()).GetResponseStream();
                if (streamReader != null)
                {
                    return new StreamReader(streamReader).ReadToEnd();
                }
            }

            return string.Empty;
        }

        public string Get()
        {
            _request.Method = "GET";
            var streamReader = ((HttpWebResponse)_request.GetResponse()).GetResponseStream();
            return streamReader != null ? new StreamReader(streamReader).ReadToEnd() : string.Empty;
        }
    }
}
