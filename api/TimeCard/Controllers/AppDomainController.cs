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
    public class AppDomainController : BaseController
    {
        private readonly IAppDomain _appDomainManager;

        public AppDomainController(IAppDomain appDomainManager)
        {
            _appDomainManager = appDomainManager;
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddAppDomain(AppDomainEntity AppDomainEntity)
        {
            _appDomainManager.AddAppDomain(AppDomainEntity);
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
        public IActionResult UpdateDomain(AppDomainEntity domain)
        {
            _appDomainManager.UpdateDomain(domain);
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
