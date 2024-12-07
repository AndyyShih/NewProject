using DataAccess.DTOs.User;

namespace DataAccess.IRepository
{
    public interface IUserRepository
    {
        /// <summary>
        /// 取得使用者資料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<GetUserOutputDto>> GetUserAsync(GetUserInputDto input);
        /// <summary>
        /// 新增使用者
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> InsertUserAsync(InsertUserDto input);
        /// <summary>
        /// 修改使用者資料
        /// </summary>
        /// <returns></returns>
        Task<int> UpdateUserAsync(UpdateUserDto input);
        /// <summary>
        /// 刪除使用者資料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> DeleteUserAsync(DeleteUserDto input);
    }
}
