namespace Extensions.Caching.Distributed.Sql.Options;

public class DistributedSqlCacheOptions
{
    public bool AutoCreateTable { get; set; } = true;
    public string ConnectionString { get; set; } = string.Empty;
    public string SchemaName { get; set; } = "Cache";
    public string TableName { get; set; } = "Caching";
}