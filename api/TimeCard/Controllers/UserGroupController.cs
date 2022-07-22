using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TimeCard.Common;
using TimeCard.CustomAuthentication;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Controllers
{

    public class UserGroupController : BaseController
    {
        private readonly IUserGroup _userGroup;
        public UserGroupController(IUserGroup userGroup)
        {
            _userGroup = userGroup;
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddUserGroup(List<UserGroupDto> userGroup)
        { 
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _userGroup.AddUserGroup(userGroup, userId)
            };
          
            return Ok(result); 
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetUserGroup(int userId)
        { 
            
           
            var result = new RequestResult<UserGroupDto>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _userGroup.GetUserGroup(userId)
            };
          
            return Ok(result); 
        }

        [HttpPut]
        [Route("edit")]
        public IActionResult EditUserGroup(UserGroupDto userGroup)
        { 
            var userId = Convert.ToInt32(User.Identity.GetUserId());
           
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _userGroup.EditUserGroup(userGroup, userId)
            };
          
            return Ok(result); 
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteUserGroup(int userId)
        {
            
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _userGroup.DeleteUserGroup(userId)
            };
          
            return Ok(result);
        }
    }
}