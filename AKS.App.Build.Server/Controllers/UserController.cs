using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AKS.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AKS.App.Build.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("GetUser")]
        public AKSUser GetUser()
        {
            return new AKSUser(User);            
        }
    }
}