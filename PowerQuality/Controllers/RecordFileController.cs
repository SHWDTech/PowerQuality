using System;
using System.IO;
using System.Web.Http;
using PowerQuality.Common;
using PowerQualityModel.ViewModel;
using SHWDTech.Platform.Utility;

namespace PowerQuality.Controllers
{
    public class RecordFileController : ApiController
    {
        public byte Post([FromBody] RecordFile file)
        {
            try
            {
                using (var localFile = File.CreateText($"d:\\power_quality\\app\\data\\{file.FileName}.txt"))
                {
                    var currentIndex = 0;
                    var channel = 0;
                    var currentModel = double.Parse(file.Configs["CurrentModal"]);
                    var voltageStop = double.Parse(file.Configs["VoltageStep"]);
                    while (currentIndex < file.FileDataBytes.Length)
                    {
                        var value = new byte[2];
                        value[0] = file.FileDataBytes[currentIndex];
                        value[1] = file.FileDataBytes[currentIndex + 1];
                        double result;
                        if (channel < 4)
                        {
                            result = Globals.BytesToInt16(value, 0, false) / 32768.0d * 5.0d * currentModel;
                        }
                        else
                        {
                            result = -(Globals.BytesToInt16(value, 0, false) /32768.0d * 5.0d * voltageStop);
                        }
                        localFile.Write(result.ToString("F8"));
                        if (channel == 7)
                        {
                            localFile.Write("\r\n");
                            channel = 0;
                        }
                        else if (currentIndex != file.FileDataBytes.Length - 2)
                        {
                            localFile.Write("\t");
                            channel++;
                        }
                        currentIndex += 2;
                    }
                }
            }
            catch (Exception ex)
            {
                LogService.Instance.Error("文件存储失败。", ex);
                return (byte) RecordUploadResult.Fail;
            }

            return (byte) RecordUploadResult.Completed;
        }
    }
}
