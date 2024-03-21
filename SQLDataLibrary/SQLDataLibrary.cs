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
        /// <summary>
        /// Get a list of records from the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public async Task<List<T>> GetRecords<T, U>(string sql, U parameters, string connectionString)
        {
            using IDbConnection connection = new SqlConnection(connectionString);
            var data = await connection.QueryAsync<T>(sql, parameters);
            return data.ToList();
        }
        /// <summary>
        /// Get a single record from the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public async Task<T?> GetRecord<T, U>(string sql, U parameters, string connectionString)
        {
            using IDbConnection connection = new SqlConnection(connectionString);
            var data = await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
            return data;
        }
        /// <summary>
        /// Execute a command against the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public async Task<int> Execute<T>(string sql, T parameters, string connectionString)
        {
            using IDbConnection connection = new SqlConnection(connectionString);
            int x = await connection.ExecuteAsync(sql, parameters);
            return x;
        }
    }
}
