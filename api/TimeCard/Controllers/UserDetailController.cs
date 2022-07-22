using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TimeCard.Common;
using TimeCard.CustomAuthentication;
using TimeCard.DTO;
using TimeCard.Interface;


namespace TimeCard.Controllers
{
    [Authorize]
    public class UserDetailController : BaseController
    {
        private readonly IUserDetail _userDetailManager;

        public UserDetailController(IUserDetail userDetailManager)
        {
            _userDetailManager = userDetailManager;
        }

        // Add the User Information
        [HttpPost]
        [Route("add")]
        public IActionResult AddUserDetail([FromForm] UploadEntity objFile)
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var fileName = string.Empty;
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                fileName = _userDetailManager.UserProfileUpload(HttpContext.Request.Form.Files[0]);
            }

            UserDetailEntity userDetail = JsonSerializer.Deserialize<UserDetailEntity> (objFile.data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            _userDetailManager.AddUserDetail(userDetail, fileName, userId);
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = true
            };

            return Ok(result);
        }

        // Update the User Information
        [HttpPut]
        [Route("editEmployee")]
        public IActionResult UpdateUserDetail([FromForm] UploadEntity objFile)
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var fileName = string.Empty;
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                fileName = _userDetailManager.UpdateUserProfile(HttpContext.Request.Form.Files[0]);
            }
            UserDetailEntity userDetail = JsonSerializer.Deserialize<UserDetailEntity>(objFile.data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            _userDetailManager.UpdateUserDetail(userDetail,fileName, userId);
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = true
            };
            return Ok(result);
        }

        // Get the Employee List
        [HttpGet]
        [Route("get")]
        public IActionResult GetUserDetail()
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var GetUserInfo = _userDetailManager.GetUserDetail();
            var result = new RequestResult<List<EmployeeList>>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = GetUserInfo
            };
            return Ok(result);
        }

        //    [HttpPost]
        //    [Route("imageUpload")]
        //    public IActionResult UserProfileUpload(IFormFile file)
        //    {
        //        _userDetailManager.UserProfileUpload(file);
        //        var result = new RequestResult<bool>
        //        {
        //            MessageType = "SaveSuccess",
        //            Success = true,
        //            Message = "Success",
        //            Data = true
        //        };
        //        return Ok(result);
        //    }


        // Get the All information of UserDetail 

        [HttpGet]
        [Route("userDetailInfo")]
        public IActionResult GetUserDetailInfo(int pkId)
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var UserInfo = _userDetailManager.GetUserDetailInfo(pkId);
            var result = new RequestResult<UserInformationEntity>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = UserInfo
            };
            return Ok(result);
        }

        [HttpGet]
        [Route("inActive")]
        public IActionResult InActiveinfo(int pkId)
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            _userDetailManager.InActiveinfo(pkId, userId);
            var result = new RequestResult<bool>
            {
                messageType = "InActive Success",
                Success = true,
                message = "Success",
                Data = true
            };
            return Ok(result);
        }
        [HttpGet]
        [Route("active")]
        public IActionResult Activeinfo(int pkId)
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            _userDetailManager.Activeinfo(pkId, userId);
            var result = new RequestResult<bool>
            {
                messageType = "Active Success",
                Success = true,
                message = "Success",
                Data = true
            };
            return Ok(result);
        }

        [HttpGet]
        [Route("asignProject")]
        public IActionResult projectInfo()
        {

            var asignProjectInfo = _userDetailManager.projectInfo();
            var result = new RequestResult<List<ClientDetails>>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = asignProjectInfo
            };
            return Ok(result);
        }
        
        [HttpGet]
        [Route("projectListInfo")]
        public IActionResult getProjectList(int userId)
        {

            var projectListInfo = _userDetailManager.getProjectList(userId);
            var result = new RequestResult<List<ProjectListInfo>>
            {
                messageType = "Data getSucess",
                Success = true,
                message = "Success",
                Data = projectListInfo
            };
            return Ok(result);
        }

        [HttpGet]
        [Route("empInformation")]
        public IActionResult EmployeeDetails(int pkId)
        {

            var empListInfo = _userDetailManager.EmployeeDetails(pkId);
            var result = new RequestResult<EmpDetails>
            {
                messageType = "Data getSucess",
                Success = true,
                message = "Success",
                Data = empListInfo
            };
            return Ok(result);
        }

        [HttpPost]
        [Route("addProject")]
        public IActionResult AddProject(List<AddProject> addProject)
        {
            var logInUserId = Convert.ToInt32(User.Identity.GetUserId());
            _userDetailManager.AddProject(addProject, logInUserId);
            var result = new RequestResult<bool>
            {
                messageType = "Data getSucess",
                Success = true,
                message = "Success",
                Data = true
            };
            return Ok(result);
        }
    }
}
