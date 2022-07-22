using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.Controllers;

namespace TimeCard.Controllers
{
    [Authorize]
    public class DomainSettingController : BaseController
    {
    }
}
