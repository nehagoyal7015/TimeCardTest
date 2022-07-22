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
    public class HolidayController : BaseController
    {
        private readonly IHoliday _holidayManager;
        public HolidayController(IHoliday holidayManager)
        {
            _holidayManager = holidayManager;
        }
        
        [HttpPost]
        [Route("add")]
        public IActionResult AddHoliday(AddHolidayEntity holiday)
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _holidayManager.AddHoliday(holiday, userId)
            };
          
            return Ok(result); 
        }
        
        [HttpGet]
        [Route("get")]
        public IActionResult GetHoliday(int year)
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            // throw new Exception("jadu ");
            var result = new RequestResult<List<HolidayEntity>>
            {
                messageType = "SaveSucess",
                Success = true,
                message = "Success",
                Data = _holidayManager.GetHoliday(year,userId)
            };
            return Ok(result);            
        }

        
        [HttpPut]
        [Route("edit")]
        public IActionResult UpdateHoliday(AddHolidayEntity update)
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _holidayManager.UpdateHoliday(update,userId)
            };
          
            return Ok(result); 
        }

        
        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteHoliday(int id)
        {
            
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _holidayManager.DeleteHoliday(id)
            };
          
            return Ok(result); 
        }

        

        
        [HttpPost]
        [Route("opt")]
        public IActionResult OptHoliday(HolidayEntity holiday)
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            _holidayManager.OptHoliday(holiday, userId);
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
        [Route("editOptHoliday")]
        public IActionResult EditOptHoliday(HolidayEntity holiday)
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            _holidayManager.EditOptHoliday(holiday, userId);
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
        [Route("getUpcoming")]
        public IActionResult GetUpcomingHoliday()
        {
            // throw new Exception("jadu ");
            var result = new RequestResult<List<HolidayEntity>>
            {
                messageType = "SaveSucess",
                Success = true,
                message = "Success",
                Data = _holidayManager.GetUpcomingHoliday()
            };
            return Ok(result);            
        }

        [HttpGet]
        [Route("getOptEmp")]
        public IActionResult GetEmployeesOpt(int floatingHolidayId)
        {
            var result = new RequestResult<List<ListEmpOpt>>
            {
                messageType = "SaveSucess",
                Success = true,
                message = "Success",
                Data = _holidayManager.GetEmployeesOpt(floatingHolidayId)
            };
            return Ok(result);            
        }

    }
}