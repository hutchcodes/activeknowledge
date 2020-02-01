using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AKS.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AKS.Api.Build.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("GetUser")]
        public AKSUser GetUser()
        {
            return new AKSUser(User);            
        }

        [AllowAnonymous]
        [HttpGet("GetCurrentUser")]
        public AKSUser GetCurrentUser()
        {
            return new AKSUser(User);
        }
    }
}