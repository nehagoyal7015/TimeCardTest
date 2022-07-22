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

namespace TimeCard.Controllers
{
    [Authorize]
    public class TimeSheetController : BaseController
    {
        private readonly ITimeSheet _timeSheetManager;

        public TimeSheetController(ITimeSheet timeSheetManager)
        {
            _timeSheetManager = timeSheetManager;
        }


        [HttpPost]
        [Route("add")]
        public IActionResult AddTimeSheet(AddTimeSheetEntity timeSheet)
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _timeSheetManager.AddTimeSheet(timeSheet,userId)
            };
          
            return Ok(result); 
        }
        

        [HttpGet]
        [Route("get")]
        public IActionResult GetTimeSheet(string date)
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var result = new RequestResult<List<TimeSheetAccess>>
            {
                messageType = "SaveSucess",
                Success = true,
                message = "Success",
                Data = _timeSheetManager.GetTimeSheet(date,userId)
            };
            return Ok(result);            
        }

        [HttpGet]
        [Route("getworkinghr")]
        public IActionResult GetWorkHr(string date)
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var result = new RequestResult<List<float>>
            {
                messageType = "SaveSucess",
                Success = true,
                message = "Success",
                Data = _timeSheetManager.GetWorkHr(date,userId)
            };
            return Ok(result);            
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteTimeSheet(int timeSheetId)
        {
            
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _timeSheetManager.DeleteTimeSheet(timeSheetId)
            };
          
            return Ok(result); 
        }

        [HttpGet]
        [Route("getMonthHr")]
        public IActionResult GetMonthHr()
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var result = new RequestResult<WeekData>
            {
                messageType = "SaveSucess",
                Success = true,
                message = "Success",
                Data = _timeSheetManager.GetMonthHr(userId)
            };
            return Ok(result);            
        }

        [HttpGet]
        [Route("getWeekHr")]
        public IActionResult GetWeekHr()
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var result = new RequestResult<WeekData>
            {
                messageType = "SaveSucess",
                Success = true,
                message = "Success",
                Data = _timeSheetManager.GetWeekHr(userId)
            };
            return Ok(result);            
        }

        [HttpGet]
        [Route("getReports")]
        public IActionResult GetTimeSheetReport()
        {
            var  getTimeSheetInfo= _timeSheetManager.GetTimeSheetReport();
            var result = new RequestResult<List<TimeSheetReport>>
            {
                messageType = "SaveSucess",
                Success = true,
                message = "Success",
                Data = getTimeSheetInfo
            };
            return Ok(result);            
        }

        [HttpPost]
        [Route("searchReports")]
        public IActionResult SearchReports(SearchDto data)
        {
            var  getTimeSheetInfo= _timeSheetManager.SearchReports(data);
            var result = new RequestResult<List<TimeSheetReport>>
            {
                messageType = "SaveSucess",
                Success = true,
                message = "Success",
                Data = getTimeSheetInfo
            };
            return Ok(result);            
        }

        [HttpGet]
        [Route("getclientinfo")]
        public IActionResult clientProjectDetail(int clientid,int userId)
        {
            var clientinfo = _timeSheetManager.clientProjectDetail(clientid,userId);
            var result = new RequestResult<List<ClientProjectInfo>>
            {
                messageType = "get Success",
                Success = true,
                message = "Success",
                Data = clientinfo
            };
            return Ok(result);
        }

        [HttpPost]
        [Route("emPtask")]
        public IActionResult EmployeeProjectInfo(SearchSelectedProject info)
        {
            var clientInfo = _timeSheetManager.EmployeeProjectInfo(info.ProjectId, info.UserId);
            var result = new RequestResult<List<EmployeeProjectDetails>>
            {
                messageType = "get Success",
                Success = true,
                message = "Success",
                Data = clientInfo
            };
            return Ok(result);
        }
    }
}
