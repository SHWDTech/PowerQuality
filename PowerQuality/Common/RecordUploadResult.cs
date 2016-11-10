namespace PowerQuality.Common
{
    /// <summary>
    /// 记录文件上传结果
    /// </summary>
    public enum RecordUploadResult : byte
    {
        /// <summary>
        /// 上传失败
        /// </summary>
        Fail = 0x00,

        /// <summary>
        /// 上传完成
        /// </summary>
        Completed = 0xFF
    }
}