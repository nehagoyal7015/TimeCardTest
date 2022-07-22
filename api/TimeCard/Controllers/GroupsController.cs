using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeCard.Common;
using TimeCard.CustomAuthentication;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Controllers
{
    [Authorize]
    public class GroupsController : BaseController
    {
        private readonly IGroup _groupsManager;
        public GroupsController(IGroup groupsManager)
        {
            _groupsManager = groupsManager;
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddGroup(AddGroupDto group, int domainId)
        { 
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _groupsManager.AddGroup(group, userId, domainId)
            };
          
            return Ok(result); 
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetGroup(int domainId)
        { 
            var userId = Convert.ToInt32(User.Identity.GetUserId());
           
            var result = new RequestResult<List<UpdateGroupDto>>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _groupsManager.GetGroup(domainId)
            };
          
            return Ok(result); 
        }

        [HttpPut]
        [Route("edit")]
        public IActionResult EditGroup(UpdateGroupDto group)
        { 
            var userId = Convert.ToInt32(User.Identity.GetUserId());
           
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _groupsManager.EditGroup(group, userId)
            };
          
            return Ok(result); 
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteGroup(int groupId)
        {
            
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _groupsManager.DeleteGroup(groupId)
            };
          
            return Ok(result);
        }


        [HttpGet]
        [Route("getAvailable")]
        public IActionResult GetAvailableGroup(int userId, int domainId)
        {            
            var result = new RequestResult<List<UpdateGroupDto>>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _groupsManager.GetAvailableGroup(userId,domainId)
            };
          
            return Ok(result); 
        }

        [HttpGet]
        [Route("getAssigned")]
        public IActionResult GetAssignedGroup(int userId)
        { 
            var result = new RequestResult<List<UpdateGroupDto>>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _groupsManager.GetAssignedGroup(userId)
            };
          
            return Ok(result); 
        }
    }
}