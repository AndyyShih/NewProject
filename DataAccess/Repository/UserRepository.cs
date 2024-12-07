using Common.Enums;
using Dapper;
using DataAccess.DTOs.User;
using DataAccess.Extensions;
using DataAccess.IRepository;
using System.Text;

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
        /// 取得使用者資料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetUserOutputDto>> GetUserAsync(GetUserInputDto input)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync(DatabaseSource.NewProject);

            var sql = new StringBuilder ("Select * From [User] Where 1=1");

            var parameters = new DynamicParameters();

            if (input.ID.HasValue)
            {
                sql.Append(" And ID = @ID ");
                parameters.Add("ID", input.ID);

            }

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                sql.Append(" And Name like '%' + @Name + '%' ");
                parameters.Add("Name", input.Name);

            }

            if (!string.IsNullOrWhiteSpace(input.Tel))
            {
                sql.Append(" And Tel = @Tel ");
                parameters.Add("Tel", input.Tel);

            }

            var result = (await connection.QueryAsync<GetUserOutputDto>(sql.ToString(), parameters)).ToList();

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

        /// <summary>
        /// 修改使用者資料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> UpdateUserAsync(UpdateUserDto input)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync(DatabaseSource.NewProject);

            var sql = @"Update [User]
                        Set Name = @Name , Tel = @Tel
                        Where ID = @ID";

            var parameters = new DynamicParameters();
            parameters.Add("ID", input.ID);
            parameters.Add("Name", input.Name);
            parameters.Add("Tel", input.Tel);

            var result = await connection.ExecuteAsync(sql, parameters);

            return result;
        }

        /// <summary>
        /// 刪除使用者資料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserAsync(DeleteUserDto input)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync(DatabaseSource.NewProject);

            var sql = @"Delete From [User]
                        Where ID = @ID";

            var parameters = new DynamicParameters();
            parameters.Add("ID", input.ID);

            var result = await connection.ExecuteAsync(sql, parameters);

            return result;
        }
    }
}
