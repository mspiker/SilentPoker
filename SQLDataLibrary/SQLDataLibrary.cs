using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace DataLibrary
{
    public class SQLDataLibrary : IDataLibrary
    {
        public async Task<List<T>> GetRecords<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);
                return data.ToList();
            }
        }
        public async Task<T?> GetRecord<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
                return data;
            }
        }
        public async Task<int> Execute<T>(string sql, T parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                int x = await connection.ExecuteAsync(sql, parameters);
                return x;
            }
        }
    }
}
