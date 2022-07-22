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
using KeyValuePair = TimeCard.Common.KeyValuePair;

namespace TimeCard.Controllers
{
    [Authorize]
    public class ProjectController : BaseController
    {
        private readonly IProject _projectManager;


        public ProjectController(IProject projectManager)
        {
            _projectManager = projectManager;
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddProject(ProjectEntity project)
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            _projectManager.AddProject(project, userId);
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
        [Route("update")]
        public IActionResult UpdateProject(ProjectEntity project)
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            _projectManager.UpdateProject(project, userId);
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
        public IActionResult GetProject()
        {
            var projectInfo = _projectManager.GetProject();
            var result = new RequestResult<List<ProjectList>>
            {
                messageType = "Get Success",
                Success = true,
                message = "Success",
                Data = projectInfo
            };  

            return Ok(result);
        }
        [HttpGet]
        [Route("getTaskList")]
        public IActionResult EmployeeTaskList(int TaskId, int userId)
        {
            var projectInfo = _projectManager.EmployeeTaskList(TaskId,userId);
            var result = new RequestResult<List<EmployeeTaskList>>
            {
                messageType = "Get Success",
                Success = true,
                message = "Success",
                Data = projectInfo
            };  

            return Ok(result);
        }
        [HttpGet]
        [Route("getTask")]
        public IActionResult GetProjectTask(int ProjectId)
        {
            var TaskInfo = _projectManager.GetProjectTask(ProjectId);
            var result = new RequestResult<List<projectTaskList>>
            {
                messageType = "Get Success",
                Success = true,
                message = "Success",
                Data = TaskInfo
            };

            return Ok(result);
        }

        [HttpGet]
        [Route("projectList")]
        public IActionResult ProjectListInfo(int domainId)
        {
            var projectListInfo = _projectManager.ProjectListInfo(domainId);
            var result = new RequestResult<List<ProjectListInformation>>
            {
                messageType = "Get Success",
                Success = true,
                message = "Success",
                Data = projectListInfo
            };

            return Ok(result);
        }

        [HttpGet]
        [Route("getEmployee")]
        public IActionResult GetEmployee()
        {
            var empInfo = _projectManager.GetEmployee();
            var result = new RequestResult<List<KeyValuePair>>
            {
                messageType = "Get Success",
                Success = true,
                message = "Success",
                Data = empInfo
            };

            return Ok(result);
        }

        [HttpGet]
        [Route("projectDetails")]
        public IActionResult GetProjedctDetail(int ProjectId)
        {
            var projectDetailInfo = _projectManager.GetProjedctDetail(ProjectId);
            var result = new RequestResult<List<TaskList>>
            {
                messageType = "Get Success",
                Success = true,
                message = "Success",
                Data = projectDetailInfo
            };

            return Ok(result);
        }
        [HttpGet]
        [Route("getProjectList")]
        public IActionResult GetProjectList(int projectId)
        {
            var projectDetailInfo1 = _projectManager.GetProjectList(projectId);
            var result = new RequestResult<ProjectInformation>
            {
                messageType = "Get Success",
                Success = true,
                message = "Success",
                Data = projectDetailInfo1
            };

            return Ok(result);
        }
        //[HttpGet]
        //[Route("EmpHourList")]
        //public IActionResult GetEmpSubmitHour(int ProjectId)
        //{
        //    var GetSubmitInfo = _projectManager.GetEmpSubmitHour(ProjectId);
        //    var result = new RequestResult<List<GetEmpSubmittedHour>>
        //    {
        //        messageType = "Get Success",
        //        Success = true,
        //        message = "Success",
        //        Data = GetSubmitInfo
        //    };

        //    return Ok(result);
        //}

        [HttpGet]
        [Route("getAllEmployee")]
        public IActionResult EmployeeInformation()
        {
            var GetEmpInfo = _projectManager.EmployeeInformation();
            var result = new RequestResult<List<KeyValuePair>>
            {
                messageType = "Get Success",
                Success = true,
                message = "Success",
                Data = GetEmpInfo
            };
            return Ok(result);
        }

        [HttpGet]
        [Route("archieve")]
        public IActionResult Archeive(int projectId, bool isArcheive)
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            _projectManager.Archeive(projectId, userId,isArcheive);
            var result = new RequestResult<bool>
            {
                messageType = "Archeive Success",
                Success = true,
                message = "Success",
                Data = true
            };
            return Ok(result);
        }

        [HttpGet]
        [Route("getAllClient")]
        public IActionResult GetUserProjects()
        {
            var clientInfo = _projectManager.GetUserProjects();
            var result = new RequestResult<List<ClientInfo>>
            {
                messageType = "Get Success",
                Success = true,
                message = "Success",
                Data = clientInfo
            };
            return Ok(result);
        }
    }

}
