using DataAccess.DTOs.User;

namespace BusinessRule.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// 取得單一使用者資料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetUserOutputDto> GetUserAsync(GetUserInputDto input);
        /// <summary>
        /// 新增使用者
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> InsertUserAsync(InsertUserDto input);
    }
}
