using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TimeCard.Common;
using TimeCard.CustomAuthentication;
using TimeCard.DTO;
using TimeCard.Interface;
using KeyValuePair = TimeCard.Common.KeyValuePair;

namespace TimeCard.Controllers
{
    [Authorize]
    public class ClientController : BaseController
    {
        private readonly IClient _clientManager;

        public ClientController(IClient clientManager)
        {
            _clientManager = clientManager;
        }
        
        [HttpPost]
        [Route("add")]
        public IActionResult AddClient(ClientEntity client)
        {
           var userId = Convert.ToInt32(User.Identity.GetUserId());
           
            _clientManager.AddClient(client ,userId);
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
        public IActionResult UpdateClient(ClientUpdateEntity client)
        {

            var userId = Convert.ToInt32(User.Identity.GetUserId());
            _clientManager.UpdateClient(client, userId);
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
        [Route("getClient")]
        public IActionResult GetClientProject()
         {
            var client = _clientManager.GetClientProject();
            var result = new RequestResult<List<ClientProject>>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = client
            };
            return Ok(result);
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetClientList()
        {

            var client = _clientManager.GetClientList();
            var result = new RequestResult<List<KeyValuePair>>
            {
                messageType = "SaveSuccess",
                Success = true,
                message = "Success",
                Data = client
            };
            return Ok(result);
        }



    }
}
