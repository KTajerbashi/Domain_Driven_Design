using BaseSource.EndPoint.WebApi.Common.Controllers;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.EndPoint.WebApi.Controllers.Test;

[ApiController]
[Route("api/[controller]")]
public class LogsController : BaseController
{
    [HttpGet("TableInfo")]
    public async Task<IActionResult> GetTableInfo([FromQuery] string tableName = null, [FromQuery] string schemaName = null)
    {
        try
        {
            string query = "SELECT * FROM dbo.GetDatabaseTablesInfos() WHERE 1=1";
            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(tableName))
            {
                query += " AND TableName = @tableName";
                parameters.Add("tableName", tableName);
            }

            if (!string.IsNullOrEmpty(schemaName))
            {
                query += " AND SchemaName = @schemaName";
                parameters.Add("schemaName", schemaName);
            }

            query += " ORDER BY TotalSpaceMB DESC";

            var result = await Factory.Query.ExecuteAsync(query, parameters);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An error occurred while fetching table info", Details = ex.Message });
        }
    }

    [HttpDelete("Excess/{maxRecords}")]
    public async Task<IActionResult> DeleteExcessRecords(int maxRecords, [FromQuery] string tableName = "Events", [FromQuery] string schemaName = "Log")
    {
        try
        {
            if (maxRecords <= 0)
                return BadRequest("maxRecords must be greater than 0");

            var result = await Factory.Query.ExecuteAsync(
                "EXEC dbo.DeleteExcessRecords @TableName = @tableName, @SchemaName = @schemaName, @MaxRecords = @maxRecords",
                new { tableName = $"{schemaName}.{tableName}", schemaName, maxRecords }
            );

            return Ok(new
            {
                Message = $"Excess records deleted successfully",
                MaxRecordsAllowed = maxRecords,
                Table = $"{schemaName}.{tableName}",
                Result = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An error occurred while deleting excess records", Details = ex.Message });
        }
    }

    [HttpGet("Statistics")]
    public async Task<IActionResult> GetDatabaseStatistics()
    {
        try
        {
            Dictionary<string, string> queries = new Dictionary<string, string>
            {
                ["BasicUsage"] = "SELECT * FROM dbo.GetDatabaseTablesInfos() ORDER BY TotalSpaceMB DESC;",
                ["EventsTableInfo"] = "SELECT * FROM dbo.GetDatabaseTablesInfos() WHERE TableName = 'Events' AND SchemaName = 'Log';",
                ["LargeTables"] = "SELECT * FROM dbo.GetDatabaseTablesInfos() WHERE [RowCount] > 1000 ORDER BY [RowCount] DESC;",
                ["LogSchemaTables"] = "SELECT * FROM dbo.GetDatabaseTablesInfos() WHERE SchemaName = 'Log' ORDER BY TotalSpaceMB DESC;",
                ["EnhancedInfo"] = "SELECT * FROM dbo.GetDatabaseTablesInfosEnhanced() ORDER BY [RowCount] DESC;"
            };

            var results = new Dictionary<string, object>();

            foreach (var query in queries)
            {
                var result = await Factory.Query.ExecuteAsync(query.Value, new { });
                results.Add(query.Key, result);
            }

            return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An error occurred while fetching statistics", Details = ex.Message });
        }
    }
}
