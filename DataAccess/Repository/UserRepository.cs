using Common.Enums;
using Dapper;
using DataAccess.DTOs.User;
using DataAccess.Extensions;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<GetUserOutputDto> GetUserAsync(GetUserInputDto input)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync(DatabaseSource.Spirit_Life);

            var sql = @"Select *
                        From [User]
                        Where [Name] like '%' + @Name + '%'";

            var parameters = new DynamicParameters();
            parameters.Add("Name", input.Name);

            var result = await connection.QueryFirstOrDefaultAsync<GetUserOutputDto>(sql, parameters);

            return result;
        }
    }
}
