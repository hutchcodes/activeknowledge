using AKS.Infrastructure.Entities;
using AKS.Infrastructure.Interfaces;
using AKS.Infrastructure.Specifications;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AKS.Infrastructure.Services
{
    public class GroupService : IGroupService
    {
        private readonly IMapper _mapper;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IAsyncRepository<Group> _groupRepo;
  

        public GroupService(IMapper mapper, ILoggerFactory loggerFactory, IAsyncRepository<Group> groupRepo)
        {
            _mapper = mapper;
            _loggerFactory = loggerFactory;
            _groupRepo = groupRepo;

        }

        public async Task AddCustomerAdminWithUser(Guid customerId, Guid userId)
        {
            var groupId = Guid.NewGuid();
            var customerAdminGroup = new Group
            {
                GroupId = groupId,
                CustomerId = customerId,
                GroupName = "Customer Admin Group",
                CanEditCustomer = true,
                CanManageAccess = true,
                CanCreateProject = true,
                IsActive = true
            };

            customerAdminGroup.UserGroups.Add(new UserGroup() { GroupId = groupId, UserId = userId });

            await _groupRepo.AddAsync(customerAdminGroup);
        }
    }
}
