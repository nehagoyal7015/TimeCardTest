using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.Common;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Controllers
{
    [Authorize]
    public class ProjectDetailController : BaseController
    {
        private readonly IProjectDetail _projectDetailManager;


        public ProjectDetailController(IProjectDetail projectDetailManager)
        {
            _projectDetailManager = projectDetailManager;
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddProjectDetail(ProjectDetailEntity projectdetail)
        {

            _projectDetailManager.AddProjectDetail(projectdetail);
            var result = new RequestResult<bool>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = true
            };

            return Ok(result);
        }
    }
}
