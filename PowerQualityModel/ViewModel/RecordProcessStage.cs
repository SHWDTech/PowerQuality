namespace PowerQualityModel.ViewModel
{
    public class RecordProcessStage
    {
        public const string OnCreatingRecord = "正在创建记录。";

        public const string OnCaclating = "正在计算数据。";

        public const string OnAfterCaclating = "正在生成统计数据。";

        public const string NotProcessing = "未找到处理信息，数据处理已经停止，请联系管理员。";

        public const string Failed = "处理过程中出现异常，数据处理已经停止！";

        public const string ProcessCompleted = "数据处理完成！";

        public const string MissingFile = "数据文件未全部上传！";
    }
}
