namespace Extensions.ChangeDataLog.Sql.Options
{
    /// <summary>
    /// پراپرتی های که نمیخواهیم در تغییرات ببینیم
    /// </summary>
    public class ChangeDataLogHamsterOptions
    {
        /// <summary>
        /// نام هر فیلد که توی لیست مورد نظر باشد را در تغییرات لحاظ نمیکند
        /// </summary>
        public List<string> propertyForReject { get; set; } = new List<string>
            {
                "CreatedByUserId",
                "CreatedDateTime",
                "ModifiedByUserId",
                "ModifiedDateTime"
            };
        /// <summary>
        /// کلید بیزنسی
        /// </summary>
        public string BusinessIdFieldName { get; set; } = "BusinessId";

    }
}
