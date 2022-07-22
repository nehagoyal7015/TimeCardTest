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
using TimeCard.Manager;

namespace TimeCard.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectUserController : BaseController
    {
        private readonly IProjectUser _projectUserManager;
        public ProjectUserController(IProjectUser projectUserManager)
        {
            _projectUserManager = projectUserManager;
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetUserProjects()
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var result = new RequestResult<List<Clients>>
            {
                messageType = "SaveSucess",
                Success = true,
                message = "Success",
                Data = _projectUserManager.GetUserProjects(userId)
            };
            return Ok(result);            
        }

        [HttpGet]
        [Route("getRecent")]
        public IActionResult GetRecentProjects()
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var result = new RequestResult<List<GetRecentProj>>
            {
                messageType = "SaveSucess",
                Success = true,
                message = "Success",
                Data = _projectUserManager.GetRecentProjects(userId)
            };
            return Ok(result);            
        }
    }
}
