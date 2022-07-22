using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.Common;
using TimeCard.CustomAuthentication;
using TimeCard.DTO;
using TimeCard.Interface;
using TimeCard.Models;

namespace TimeCard.Controllers
{
    [Authorize]
    public class UserAccountController : BaseController
    {
        private readonly IUserAccount _userAccountManager;


        public UserAccountController(IUserAccount userAccountManager)
        {
            _userAccountManager = userAccountManager;
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddUserAccount(UserAccountEntity useraccountEntity)
        {
            _userAccountManager.AddUserAccount(useraccountEntity);
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = true
            };

            return Ok(result);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateUserAccount(UserAccountEntity useraccountEntity)
        {
            _userAccountManager.UpdateUserAccount(useraccountEntity);
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = true
            };
            return Ok(result);
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetUserAccount(int userId)
        {

            var GetUserInfo = _userAccountManager.GetUserAccount(userId);
            var result = new RequestResult<List<UserAccountEntity>>
            {
                messageType = "SaveSucess",
                Success = true,
                message = "Success",
                Data = GetUserInfo
            };
            return Ok(result);
        }  
        
        [HttpGet]
        [Route("getCurrentUser")]
        public IActionResult currentUser(int userId)
        {

            var Info1 = _userAccountManager.currentUser(userId);
            var result = new RequestResult<GetUserName>
            {
                messageType = "SaveSucess",
                Success = true,
                message = "Success",
                Data = Info1
            };
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteUserAccount(int UserId)
        {
            _userAccountManager.DeleteUserAccount(UserId);
            var result = new RequestResult<bool>
            {
                messageType = "DeleteSucess",
                Success = true,
                message = "Success",
                Data = true
            };
            return Ok();
        }

        [HttpGet]
        [Route("profileInfo")]
        public IActionResult GetUserDetailInfo(int userId)
        {

            var UserInfo = _userAccountManager.ProfileInformation(userId);
            var result = new RequestResult<ProfileEntity>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = UserInfo
            };
            return Ok(result);
        }

        [HttpPut]
        [Route("updateProfile")]
        public IActionResult UpdateProfieInfo(ProfileEntity userInfo)
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            _userAccountManager.UpdateProfieInfo(userInfo,userId);
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = true
            };
            return Ok(result);
        }
    }
}
