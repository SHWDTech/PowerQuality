namespace PowerQualityModel.DataModel
{
    /// <summary>
    /// 接线类型
    /// </summary>
    public enum LineType : byte
    {
        /// <summary>
        /// 三角接线
        /// </summary>
        Triangle = 0x01,

        /// <summary>
        /// 星形带中线
        /// </summary>
        StarWithMiddle = 0x02,

        /// <summary>
        /// 星形不带中线
        /// </summary>
        Start = 0x03
    }
}
