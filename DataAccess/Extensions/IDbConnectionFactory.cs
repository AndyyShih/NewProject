using Common.Enums;
using System.Data;

namespace DataAccess.Extensions
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync(DatabaseSource source);
    }
}
