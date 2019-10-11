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
        private readonly IProjectService _projectService;
        private readonly IHeaderService _headerService;

        public ProjectController(IProjectService projectService, IHeaderService headerService)
        {
            this._projectService = projectService;
            this._headerService = headerService;
        }

        // GET: api/Project/Header/1234
        [HttpGet("header/{projectId:Guid}", Name = "GetProjectHeader")]
        public async Task<HeaderNavView> GetHeader(Guid projectId)
        {
            return await _headerService.GetHeaderForProjectAsync(projectId);
        }

        [HttpGet("list/{customerId:Guid}", Name = "GetProjectsForCustomer")]
        public async Task<List<ProjectList>> GetProjectsForCustomer(Guid customerId)
        {
            return await _projectService.GetProjetListForDisplayAsync(customerId);
        }

        [HttpGet("{projectId:Guid}", Name = "GetProjectEdit")]
        public async Task<ProjectEdit> GetProjectEdit(Guid projectId)
        {
            return await _projectService.GetProjectForEdit(projectId);
        }

        // GET: api/Customer/Header/1234
        [HttpPost(Name = "UpdateProjectEdit")]
        public async Task<ProjectEdit> UpdateProjectEdit(ProjectEdit project)
        {
            return await _projectService.UpdateProject(project);
        }
    }
}
