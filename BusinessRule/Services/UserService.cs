using BusinessRule.Interfaces;
using DataAccess.DTOs.User;
using DataAccess.IRepository;

namespace BusinessRule.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        /// <summary>
        /// 取得使用者資料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetUserOutputDto>> GetUserAsync(GetUserInputDto input)
        {
            var result = await _userRepository.GetUserAsync(input);

            return result;
        }

        /// <summary>
        /// 新增使用者
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> InsertUserAsync(InsertUserDto input)
        {
            var result = await _userRepository.InsertUserAsync(input);

            return result;
        }

        /// <summary>
        /// 修改使用者資料
        /// </summary>
        /// <returns></returns>
        public async Task<int> UpdateUserAsync(UpdateUserDto input)
        {
            var result = await _userRepository.UpdateUserAsync(input);

            return result;
        }

        /// <summary>
        /// 刪除使用者資料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserAsync(DeleteUserDto input)
        {
            var result = await _userRepository.DeleteUserAsync(input);

            return result;
        }
    }
}
