using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EntityFramework.BulkInsert.Extensions;
using EntityFramework.BulkInsert.Helpers;
using EntityFramework.BulkInsert.Providers;

namespace TestConsole
{
    public class MySqlProvider : ProviderBase<SqlConnection, SqlTransaction>
    {
        protected override string ConnectionString
            => Context.Database.Connection.ConnectionString;

        public override object GetSqlGeography(string wkt, int srid)
        {
            throw new NotImplementedException();
        }

        protected override SqlConnection CreateConnection()
            => new SqlConnection(ConnectionString);

        public override void Run<T>(IEnumerable<T> entities, SqlTransaction transaction, BulkInsertOptions options)
        {
            var flag = (SqlBulkCopyOptions.KeepIdentity & options.SqlBulkCopyOptions) > SqlBulkCopyOptions.Default;
            using (var mappedDataReader = new MappedDataReader<T>(entities, this))
            {
                using (var sqlBulkCopy = new SqlBulkCopy(transaction.Connection, options.SqlBulkCopyOptions, transaction))
                {
                    sqlBulkCopy.BulkCopyTimeout = options.TimeOut;
                    sqlBulkCopy.BatchSize = options.BatchSize;
                    sqlBulkCopy.DestinationTableName =
                        $"[{ mappedDataReader.SchemaName}].[{ mappedDataReader.TableName}]";
                    sqlBulkCopy.EnableStreaming = options.EnableStreaming;
                    sqlBulkCopy.NotifyAfter = options.NotifyAfter;
                    if (options.Callback != null)
                        sqlBulkCopy.SqlRowsCopied += options.Callback;
                    foreach (var col in mappedDataReader.Cols)
                    {
                        if (!col.Value.IsIdentity || flag)
                            sqlBulkCopy.ColumnMappings.Add(col.Value.ColumnName, col.Value.ColumnName);
                    }
                    sqlBulkCopy.WriteToServer(mappedDataReader);
                }
            }
        }
    }
}
