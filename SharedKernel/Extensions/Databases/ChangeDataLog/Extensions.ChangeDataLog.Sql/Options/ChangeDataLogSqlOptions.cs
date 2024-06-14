namespace Extensions.ChangeDataLog.Sql.Options
{
    /// <summary>
    /// پارامتر های کانفیگ سرویس جهت مدیریت ارتباط با پایگاه داده و جدول ذخیره کننده
    /// </summary>
    public class ChangeDataLogSqlOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
        public bool AutoCreateSqlTable { get; set; } = true;
        public string EntityTableName { get; set; } = "EntityChageDataLogs";
        public string PropertyTableName { get; set; } = "PropertyChageDataLogs";
        public string SchemaName { get; set; } = "dbo";
    }
}
