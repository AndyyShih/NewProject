using Common.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccess.Extensions
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IDbConnection> CreateConnectionAsync(DatabaseSource source)
        {
            var connectionString = GetConnectionString(source);
            var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            return connection;
        }

        private string GetConnectionString(DatabaseSource source)
        {
            var connectionString = source switch
            {
                DatabaseSource.NewProject => _configuration.GetConnectionString("NewProject_Db")
            };

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"沒有名稱為 '{source}' 的連線字串");
            }

            return connectionString;
        }
    }
}
