using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TimeCard.Common;
using TimeCard.CustomAuthentication;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Controllers
{
    public class AccessControlController : BaseController
    {
        private readonly IAccessControl _accessControl;
        public AccessControlController(IAccessControl accessControl)
        {
            _accessControl = accessControl;
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddAccessControl(AccessControlDto accessControl, int domainId)
        { 
            var userId = Convert.ToInt32(User.Identity.GetUserId());
           
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _accessControl.AddAccessControl(accessControl, userId, domainId)
            };
          
            return Ok(result); 
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteAccessControl(int accessControlId)
        { 
            
           
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _accessControl.DeleteAccessControl(accessControlId)
            };
          
            return Ok(result); 
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateAccessControl(UpdateAccessControlDto updateAccessControl)
        { 
            var userId = Convert.ToInt32(User.Identity.GetUserId());
           
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _accessControl.UpdateAccessControl(updateAccessControl, userId)
            };
          
            return Ok(result); 
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetAccessControl(int domainId)
        { 
            
           
            var result = new RequestResult<List<UpdateAccessControlDto>>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = _accessControl.GetAccessControl(domainId)
            };
          
            return Ok(result); 
        }
    }
}