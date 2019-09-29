using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AKS.Infrastructure.Interfaces;
using AKS.Common.Models;

namespace AKS.App.Build.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IHeaderService _headerService;
        public ProjectController(IHeaderService headerService)
        {
            _headerService = headerService;
        }
        
        // GET: api/Project/Header/1234
        [HttpGet("header/{projectId:Guid}",  Name = "GetProjectHeader")]
        public async Task<HeaderNavView> GetHeader(Guid projectId)
        {
            return await _headerService.GetHeaderForProjectAsync(projectId);
        }
    }
}
