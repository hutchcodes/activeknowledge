using AKS.AppCore.Entities;
using AKS.AppCore.Interfaces;
using AKS.AppCore.Specifications;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;
using AKS.Infrastructure.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Infrastructure.Services
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

            _logger.LogDebug($"New instance of {GetType().Name} was created");
        }

        public async Task<HeaderNavView> GetHeaderForProjectAsync(Guid projectId)
        {
            var spec = new ProjectHeaderSpecification(projectId);
            var project = await _projectRepo.GetAsync(spec);

            var projectVM = new HeaderNavView()
            {
                ProjectId = project.ProjectId,
                ProjectName = project.Name,
                ProjectLogoURL = project.LogoFileName,
                CustomerId = project.Customer.CustomerId,
                CustomerName = project.Customer.Name,
                CustomerLogoURL = project.Customer.LogoFileName
            };

            return projectVM;
        }

        public async Task<HeaderNavView> GetHeaderForCustomerAsync(Guid customerId)
        {
            var spec = new CustomerHeaderSpecification(customerId);
            var customer = await _customerRepo.GetAsync(spec);

            var headerNav = new HeaderNavView()
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.Name,
                CustomerLogoURL = customer.LogoFileName
            };   
            
            return headerNav;
        }
    }
}
