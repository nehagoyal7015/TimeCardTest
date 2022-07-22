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
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectInvoiceController : BaseController
    {
        private readonly IProjectInvoice _projectInvoiceManager;
        public ProjectInvoiceController(IProjectInvoice projectInvoiceManager)
        {
            _projectInvoiceManager = projectInvoiceManager;
        }
        [HttpPost]
        [Route("add")]
        public IActionResult AddProjectInvoice(ProjectInvoiceEntity projectInvoice)
        {
            _projectInvoiceManager.AddProjectInvoice(projectInvoice);
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
        public IActionResult GetProjectInvoice(int invoiceId)
        {
            var InvoiceInfo = _projectInvoiceManager.GetProjectInvoice(invoiceId);
            var result = new RequestResult<List<ProjectInvoiceEntity>>
            {
                messageType = "update Success",
                Success = true,
                message = "Success",
                Data = InvoiceInfo
            };

            return Ok(result);
        }
    }
}
