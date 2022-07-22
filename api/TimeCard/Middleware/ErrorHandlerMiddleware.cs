using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using TimeCard.Common;
using TimeCard.Errors;

namespace TimeCard.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var result = new RequestResult<string>();
                result.messageType = "Error";
                result.Success = false;
                switch (error)
                {
                    case AppException :

                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        result.message = "Custom Validation";
                        result.messageType = "Info";
                        break;

                    case KeyNotFoundException e:

                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        result.message = e.ToString();
                        break;

                    case UnauthorizedAccessException e:

                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        result.message = "Access Denied";
                        break;  

                    default:

                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        result.message = "Unhandled Exception";
                        break;
                }

                result.Data = error ? .Message;

                var resResult = JsonSerializer.Serialize(result);
                await response.WriteAsync(resResult);
            }
         }
    }
}
    