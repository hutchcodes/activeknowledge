using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AKS.AppCore.Entities;
using AKS.AppCore.Interfaces;
using AKS.AppCore.Specifications;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace AKS.Infrastructure.Services
{
    public class ProjectService : IProjectService 
    {
        private readonly ILogger<ProjectService> _logger;
        private readonly IAsyncRepository<Project> _projectRepo;
        public ProjectService(ILoggerFactory loggerFactory, IAsyncRepository<Project> projectRepo)
        {
            _logger = loggerFactory.CreateLogger<ProjectService>();
            _projectRepo = projectRepo;

            _logger.LogDebug($"New instance of {GetType().Name} was created");
        }
        public async Task<List<ProjectList>> GetProjetListForDisplayAsync(Guid customerId)
        {
            var spec = new ProjectListSpecification(customerId);
            var projects = await _projectRepo.ListAsync(spec);

            var projectsVM = new List<ProjectList>();

            projectsVM.AddRange(projects.ConvertAll(x => new ProjectList() { ProjectId = x.ProjectId, Name = x.Name }));

            return projectsVM;
        }
    }
}
