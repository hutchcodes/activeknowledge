using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKS.Infrastructure.Entities;
using AKS.Infrastructure.Interfaces;
using AKS.Infrastructure.Specifications;
using AKS.Common.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AKS.Infrastructure.Services
{
    public class ProjectService : IProjectService 
    {
        private readonly ILogger<ProjectService> _logger;
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Project> _projectRepo;
        public ProjectService(IMapper mapper, ILoggerFactory loggerFactory, IAsyncRepository<Project> projectRepo)
        {
            _logger = loggerFactory.CreateLogger<ProjectService>();
            _mapper = mapper;
            _projectRepo = projectRepo;

            _logger.LogDebug($"New instance of {GetType().Name} was created");
        }

        public async Task<List<ProjectList>> GetProjetListForDisplayAsync(Guid customerId)
        {
            var spec = new ProjectListSpecification(customerId);
            var projects = await _projectRepo.ListAsync(spec);

            return _mapper.Map<List<ProjectList>>(projects);
        }

        public async Task<ProjectEdit> GetProjectForEdit(Guid projectId)
        {
            var spec = new ProjectSpecification(projectId);
            var project = await _projectRepo.GetAsync(spec);

            return _mapper.Map<ProjectEdit>(project);
        }

        public async Task<ProjectEdit> UpdateProject(ProjectEdit projectEdit)
        {
            var spec = new ProjectSpecification(projectEdit.ProjectId);
            var project = await _projectRepo.GetAsync(spec);

            project = _mapper.Map(projectEdit, project);
            await _projectRepo.UpdateAsync(project);

            project = await _projectRepo.GetAsync(spec);
            return _mapper.Map<ProjectEdit>(project);
        }
    }
}
