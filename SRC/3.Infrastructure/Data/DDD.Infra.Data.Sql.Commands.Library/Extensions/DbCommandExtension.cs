using DDD.Infra.Data.Sql.Commands.Library.Extensions;
using DDD.Utilities.Library.Extensions;
using System.Data;
using System.Data.Common;

namespace DDD.Infra.Data.Sql.Commands.Library.Extensions;

public static class DbCommandExtension
{
    public static void ApplyCorrectYeKe(this DbCommand command)
    {
        command.CommandText = command.CommandText.ApplyCorrectYeKe();

        foreach (DbParameter parameter in command.Parameters)
        {
            switch (parameter.DbType)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                case DbType.StringFixedLength:
                case DbType.Xml:
                    parameter.Value = parameter.Value is DBNull ? parameter.Value : parameter.Value.ApplyCorrectYeKe();
                    break;
            }
        }
    }
}