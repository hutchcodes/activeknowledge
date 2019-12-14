using System;
using System.Threading.Tasks;
using AKS.Infrastructure.Entities;
using AKS.Infrastructure.Interfaces;
using AKS.Infrastructure.Specifications;
using AKS.Common.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AKS.Infrastructure.Services
{
    public class HeaderService : IHeaderService
    {
        private readonly ILogger<HeaderService> _logger;
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Customer> _customerRepo;
        private readonly IAsyncRepository<Project> _projectRepo;
        public HeaderService(IMapper mapper, ILoggerFactory loggerFactory, IAsyncRepository<Customer> customerRepo, IAsyncRepository<Project> projectRepo)
        {
            _logger = loggerFactory.CreateLogger<HeaderService>();
            _mapper = mapper;
            _customerRepo = customerRepo;
            _projectRepo = projectRepo;

            _logger.LogDebug($"New instance of {GetType().Name} was created");
        }

        public async Task<HeaderNavView> GetHeaderForProjectAsync(Guid projectId)
        {
            var spec = new ProjectHeaderSpecification(projectId);
            var project = await _projectRepo.GetAsync(spec);

            return _mapper.Map<HeaderNavView>(project);
        }

        public async Task<HeaderNavView> GetHeaderForCustomerAsync(Guid customerId)
        {
            var spec = new CustomerHeaderSpecification(customerId);
            var customer = await _customerRepo.GetAsync(spec);

            return _mapper.Map<HeaderNavView>(customer);            
        }
    }
}
