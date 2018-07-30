using Resurgam.AppCore.Entities;
using Resurgam.AppCore.Interfaces;
using Resurgam.AppCore.Specifications;
using Resurgam.Infrastructure.Data;
using Resurgam.Web.Admin.Interfaces;
using Resurgam.Web.Admin.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Web.Admin.Services
{
    public class ProjectService : IProjectService 
    {
        private readonly ILogger<ProjectService> _logger;
        private readonly IAsyncRepository<Project> _projectRepo;
        public ProjectService(ILoggerFactory loggerFactory, IAsyncRepository<Project> projectRepo)
        {
            _logger = loggerFactory.CreateLogger<ProjectService>();
            _projectRepo = projectRepo;
        }
        public async Task<List<ProjectListViewModel>> GetProjetListForDisplayAsync(int customerId)
        {
            var spec = new ProjectListSpecification(customerId);
            var projects = await _projectRepo.ListAsync(spec);

            var projectsVM = new List<ProjectListViewModel>();

            projectsVM.AddRange(projects.ConvertAll(x => new ProjectListViewModel(x)));

            return projectsVM;
        }
    }
}
