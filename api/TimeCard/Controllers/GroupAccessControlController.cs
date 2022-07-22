using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TimeCard.Common;
using TimeCard.CustomAuthentication;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Controllers
{

    public class GroupAccessControlController : BaseController
    {
        private readonly IGroupAccessControl _groupAccessControl;
        public GroupAccessControlController(IGroupAccessControl groupAccessControl)
        {
            _groupAccessControl = groupAccessControl;
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddGroupAccessControl(List<GroupAccessControlDto> groupAccessControl, int groupId)
        { 
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            
           
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _groupAccessControl.AddGroupAccessControl(groupAccessControl, groupId, userId)
            };
          
            return Ok(result); 
        }

        [HttpGet]
        [Route("getAssigned")]
        public IActionResult GetAssignedAccessControl(int groupId)
        { 
            
           
            var result = new RequestResult<List<UpdateGroupAccessControlDto>> 
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _groupAccessControl.GetAssignedAccessControl(groupId)
            };
          
            return Ok(result); 
        }

        [HttpGet]
        [Route("getAvialable")]
        public IActionResult GetAvialableAccessControl(int groupId)
        { 
            
           
            var result = new RequestResult<List<UpdateAccessControlDto>> 
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _groupAccessControl.GetAvialableAccessControl(groupId)
            };
          
            return Ok(result); 
        }

        
    }
}