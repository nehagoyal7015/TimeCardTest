using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    public class AuthenticateController : BaseController
    {
        private readonly IUserAccount userAccount;

        public AuthenticateController(IUserAccount _userAccount)
        {
            userAccount = _userAccount;
        }

        /// <summary>
        /// User authentication service
        /// </summary>
        /// <param name="userCredential">Domain, username, password are required</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserCrediential userCredential)
        {

            //var isValid = userAccount.ValidateUser(userCredential);
            //var result = new RequestResult<string>();
            //if (isValid)
            //{
            //    result.MessageType = "Successfull";
            //    result.Success = true;
            //    result.Message = "Success";
            //    result.Data = jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
            //}
            //else
            //{
            //    result.MessageType = "Failed";
            //    result.Success = false;
            //    result.Message = "Error";
            //    result.Data = null;
            //    throw new Exception(" UserName and Password is Incorrect");
            //}

            var result = new RequestResult<UserLoginDetail>();
            result.messageType = "Successfull";
            result.Success = true;
            result.message = "Success";
            result.Data = userAccount.UserAuthentication(userCredential);
            return Ok(result);

        }

        [AllowAnonymous]
        [HttpPost("gAuth")]
        public IActionResult gAuth([FromBody] GoogleCrediential googleCrediential )
        {
            var result = new RequestResult<UserLoginDetail>();
            result.messageType = "Successfull";
            result.Success = true;
            result.message = "Success";
            result.Data = userAccount.gAuth(googleCrediential);
            return Ok(result);

        }


    }
}
