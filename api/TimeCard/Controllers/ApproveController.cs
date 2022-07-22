using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.Interface;
using TimeCard.Manager;
using TimeCard.Common;
using TimeCard.CustomAuthentication;
using TimeCard.DTO;

namespace TimeCard.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApproveController : ControllerBase
    {
        private readonly IApprove  _approveManager;

        public ApproveController(IApprove approveManager)
        {
            _approveManager = approveManager;
        }

        

        [HttpPost]
        [Route("searchreport")]
        public IActionResult SearchReport(AppSearchDto data)
        {
            var getapprovalInfo = _approveManager.SearchReport(data);
            var result = new RequestResult<List<AppReport>>
            {
                messageType = "SaveSucess",
                Success = true,
                message = "Success",
                Data = getapprovalInfo
            };
            return Ok(result);
        }

        [HttpGet]
        [Route("getclientinfo")]
        public IActionResult AppClientProjectDetail(int clientid, int userId)
        {
            var appclientinfo = _approveManager.AppClientProjectDetail(clientid, userId);
            var result = new RequestResult<List<ApprovalClientProjectInfo>>
            {
                messageType = "get Success",
                Success = true,
                message = "Success",
                Data = appclientinfo
            };
            return Ok(result);
        }
        [HttpGet]
        [Route("empReport")]
        public IActionResult AppEmployeeEnteredTaskList(int taskId, int userId)
        {
            var empReport = _approveManager.AppEmployeeEnteredTaskList(taskId, userId);
            var result = new RequestResult<List<EmpDetailApp>>
            {
                messageType = "get Success",
                Success = true,
                message = "Success",
                Data = empReport
            };
            return Ok(result);
        }

        [HttpPost]
        [Route("getProjectTask")]
        public IActionResult TaskListProject(taskData taskinfo)
        {
            var taskinfoList = _approveManager.TaskListProject(taskinfo.ProjectId);
            var result = new RequestResult<List<taskProject>>
            {
                messageType = "SaveSucess",
                Success = true,
                message = "Success",
                Data = taskinfoList
            };
            return Ok(result);
        }


        [HttpPost]
        [Route("search")]
        public IActionResult EmployeeDetail(ApproveSearchDto data, int TaskId)
        {
            var getappTimeSheetInfo = _approveManager.EmployeeDetail(data, TaskId);
            var result = new RequestResult<List<ApproveReport>>
            {
                messageType = "SaveSucess",
                Success = true,
                message = "Success",
                Data = getappTimeSheetInfo
            };
            return Ok(result);
        }

        [HttpGet]
        [Route("getrecent")]
        public IActionResult GetRecentApprovals()
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var result = new RequestResult<List<Clients1>>
            {
                messageType = "SaveSucess",
                Success = true,
                message = "Success",
                Data = _approveManager.GetRecentApprovals(userId)
            };
            return Ok(result);
        }

    }
}

