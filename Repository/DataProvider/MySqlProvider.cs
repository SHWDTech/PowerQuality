using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using EntityFramework.BulkInsert.Extensions;
using EntityFramework.BulkInsert.Helpers;
using EntityFramework.BulkInsert.Providers;
using MySql.Data.MySqlClient;

namespace Repository.DataProvider
{
    public class MySqlProvider : ProviderBase<MySqlConnection, MySqlTransaction>
    {
        protected override string ConnectionString
            => Context.Database.Connection.ConnectionString;

        public override object GetSqlGeography(string wkt, int srid)
        {
            throw new NotImplementedException();
        }

        protected override MySqlConnection CreateConnection()
            => new MySqlConnection(ConnectionString);

        public override void Run<T>(IEnumerable<T> entities, MySqlTransaction transaction, BulkInsertOptions options)
        {
            var flag = (SqlBulkCopyOptions.KeepIdentity & options.SqlBulkCopyOptions) > SqlBulkCopyOptions.Default;
            using (var mappedDataReader = new MappedDataReader<T>(entities, this))
            {
                using (transaction)
                {
                    var mySqlCommand = new StringBuilder($"INSERT INTO {mappedDataReader.TableName} (");
                    for (var i = 0; i < mappedDataReader.Cols.Count; i++)
                    {
                        mySqlCommand.Append($"{mappedDataReader.Cols[i]}");
                        if (i < mappedDataReader.Cols.Count)
                        {
                            mySqlCommand.Append(",");
                        }
                    }
                    mySqlCommand.Append(") VALUES");

                    do
                    {
                        //mappedDataReader.
                    } while (mappedDataReader.NextResult());
                }
            }
        }
    }
}
