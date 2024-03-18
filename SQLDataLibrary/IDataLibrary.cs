
namespace DataLibrary
{
    public interface IDataLibrary
    {
        Task<int> Execute<T>(string sql, T parameters, string connectionString);
        Task<T?> GetRecord<T, U>(string sql, U parameters, string connectionString);
        Task<List<T>> GetRecords<T, U>(string sql, U parameters, string connectionString);
    }
}