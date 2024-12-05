using BusinessRule.Interfaces;
using Common.Enums;
using DataAccess.DTOs.User;
using DataAccess.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace NewProject.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 取得單一使用者資料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUser")]
        public async Task<ApiResponse<GetUserOutputDto>> GetUserAsync(GetUserInputDto input)
        {
            var result = await _userService.GetUserAsync(input);

            if (result == null)
            {
                Log.Error("查無資料");
                return ApiResponseFactory.CreateErrorResult<GetUserOutputDto>(ErrorCode.DATA_EMPTY, result);
            }
            else
            {
                Log.Information("查詢完成");
                return ApiResponseFactory.CreateSuccessResult<GetUserOutputDto>(result);
            }
        }

        /// <summary>
        /// 新增使用者
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertUser")]
        public async Task<ApiResponse> InsertUserAsync(InsertUserDto input)
        {
            var result = await _userService.InsertUserAsync(input);

            if (result == 0)
            {
                Log.Error("新增失敗");
                return ApiResponseFactory.CreateErrorResult(ErrorCode.ERROR_SQLSERVER_UPDATE_FAILED);
            }
            else
            {
                Log.Information("新增成功");
                return ApiResponseFactory.CreateSuccessResult();
            }
        }
    }
}
