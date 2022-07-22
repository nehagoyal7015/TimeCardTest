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
    public class UserLeaveController : BaseController
    {
        private readonly IUserLeave _userLeaveManager;

        public UserLeaveController(IUserLeave userLeaveManager)
        {
            _userLeaveManager = userLeaveManager;
        }


        [HttpPost]
        [Route("add")]
        public IActionResult AddUserLeave(UserLeaveEntity userLeave)
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            _userLeaveManager.AddUserLeave(userLeave, userId);
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = true
            };
            return Ok(result);
        }
        
        [HttpPost]
        [Route("addNewUser")]
        public IActionResult AddNewUserLeave(UserLeaveEntity userLeave,int userId)
        {
            _userLeaveManager.AddNewUserLeave(userLeave, userId);
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
        public IActionResult GetUserLeave(int userId, int year)
        {
      
            var userleave = _userLeaveManager.GetUserLeave(userId,year);
            var result = new RequestResult<List<UserLeaveEntity>>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = userleave
            };
            return Ok(result);
        }



        [HttpGet]
        [Route("cancel")]
        public IActionResult CancelLeave(int LeaveId)
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            _userLeaveManager.CancelLeave(LeaveId, userId);
            var result = new RequestResult<bool>
            {
                messageType = "DeleteSucess",
                Success = true,
                message = "Success",
                Data = true
            };
            return Ok(result);
        }

        [HttpGet]
        [Route("getUser")]
        public IActionResult GetUserName()
        {

            var userAccount = _userLeaveManager.GetUserName();
            var result = new RequestResult<List<UserAcountDtos>>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = userAccount
            };
            return Ok(result);
        }

        [HttpGet]
        [Route("countLeaves")]
        public IActionResult CountLeaves(int userId)
        {

            var userAccount = _userLeaveManager.CountLeaves(userId);
            var result = new RequestResult<UserLeaveEntity>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = userAccount
            };
            return Ok(result);
        }

        [HttpGet]
        [Route("getAllUpcomingLeave")]
        public IActionResult GetAllUpcomingLeave()
        {

            var  aa = _userLeaveManager.GetAllUpcomingLeave();
            var result = new RequestResult<List<UserLeaveDto>>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = aa
            };
            return Ok(result);
        }

        [HttpGet]
        [Route("getLeaveDetails")]
        public IActionResult GetleavesDetails()
        {

            var leaveDetailInfo = _userLeaveManager.GetleavesDetails();
            var result = new RequestResult<List<leavesDetails>>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = leaveDetailInfo

            };
            return Ok(result);
        }

         [HttpGet]
        [Route("search")]
        public IActionResult SearchLeaveDetails(string userName, int Year, string date)
        {

            var searchInfo = _userLeaveManager.SearchLeaveDetails(userName,Year,date);
            var result = new RequestResult<List<leavesDetails>>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = searchInfo
            };
            return Ok(result);
        }

    }
}
