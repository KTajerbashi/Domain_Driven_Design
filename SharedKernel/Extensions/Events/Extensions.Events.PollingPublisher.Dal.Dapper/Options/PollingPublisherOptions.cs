namespace Extensions.Events.PollingPublisher.Dal.Dapper.Options
{
    public class PollingPublisherDalRedisOptions
    {
        public string ApplicationName { get; set; }
        public string ConnectionString { get; set; }

        /// <summary>
        /// دستور دریافت اطلاعات رویداد های از جدول
        /// </summary>
        public string SelectCommand { get; set; } = "Select top (@Count) * from Event.OutBoxEventItems where IsProcessed = 0";
        /// <summary>
        /// ویرایش کردن رویداد ها به حالت اجرا شده
        /// </summary>
        public string UpdateCommand { get; set; } = "Update Event.OutBoxEventItems set IsProcessed = 1 where OutBoxEventItemId in @Ids";
    }
}
