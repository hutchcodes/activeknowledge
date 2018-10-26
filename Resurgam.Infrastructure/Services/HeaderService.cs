using Resurgam.AppCore.Entities;
using Resurgam.AppCore.Interfaces;
using Resurgam.AppCore.Specifications;
using Resurgam.Infrastructure.Interfaces;
using Resurgam.Infrastructure.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Infrastructure.Services
{
    public class HeaderService : IHeaderService
    {
        private readonly ILogger<HeaderService> _logger;
        private readonly IAsyncRepository<Customer> _customerRepo;
        private readonly IAsyncRepository<Project> _projectRepo;
        public HeaderService(ILoggerFactory loggerFactory, IAsyncRepository<Customer> customerRepo, IAsyncRepository<Project> projectRepo)
        {
            _logger = loggerFactory.CreateLogger<HeaderService>();
            _customerRepo = customerRepo;
            _projectRepo = projectRepo;
        }

        public async Task<HeaderNavViewModel> GetHeaderForProjectAsync(Guid projectId)
        {
            var spec = new ProjectHeaderSpecification(projectId);
            var project = await _projectRepo.GetAsync(spec);

            var projectVM = new HeaderNavViewModel(project);

            return projectVM;
        }

        public async Task<HeaderNavViewModel> GetHeaderForCustomerAsync(Guid customerId)
        {
            var spec = new CustomerHeaderSpecification(customerId);
            var customer = await _customerRepo.GetAsync(spec);

            var projectVM = new HeaderNavViewModel(customer);
            
            return projectVM;
        }
    }
}
