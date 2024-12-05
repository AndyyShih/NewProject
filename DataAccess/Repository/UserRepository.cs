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

        /// <summary>
        /// 取得單一使用者資料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetUserOutputDto> GetUserAsync(GetUserInputDto input)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync(DatabaseSource.NewProject);

            var sql = @"Select *
                        From [User]
                        Where [Name] like '%' + @Name + '%'";

            var parameters = new DynamicParameters();
            parameters.Add("Name", input.Name);

            var result = await connection.QueryFirstOrDefaultAsync<GetUserOutputDto>(sql, parameters);

            return result;
        }

        /// <summary>
        /// 新增使用者
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> InsertUserAsync(InsertUserDto input)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync(DatabaseSource.NewProject);

            var sql = @"Insert Into [User](Name , Tel)
                        Values (@Name , @Tel)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", input.Name);
            parameters.Add("Tel", input.Tel);

            var result = await connection.ExecuteAsync(sql, parameters);

            return result;
        }
    }
}
