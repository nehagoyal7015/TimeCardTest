using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeCard.Common;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Controllers
{
    [Authorize]
    public class DocsController : BaseController
    {
        private readonly IDocs _docs;

        public DocsController(IDocs docs)
        {
            _docs = docs;
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetDocuments()
        {
            var result = new RequestResult<List<parentCatList>>
            {
                messageType = "SaveSucess",
                Success = true,
                message = "Success",
                Data = _docs.GetDocs()
            };
            return Ok(result);            
        }

        [HttpPost]
        [Route("add-doc")]
        public IActionResult AddDocument(DocsDto docInfo)
        {
            var result = new RequestResult<bool>
            {
                messageType = "SaveSucess",
                Success = true,
                message = "Success",
                Data = _docs.AddDocument(docInfo)
            };
            return Ok(result);            
        }


        [HttpGet]
        [Route("get-doc-cat")]
        public IActionResult GetParent(int domainId, string catname)
        {
            var result = new RequestResult<List<parentCat>>
            {
                messageType = "SaveSucess",
                Success = true,
                message = "Success",
                Data = _docs.GetParent(domainId,catname)
            };
            return Ok(result);            
        }
    }
}