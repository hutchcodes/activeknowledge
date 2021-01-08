using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AKS.Api.Build.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HeaderController : ControllerBase
    {
        private readonly IHeaderService _headerService;
        public HeaderController(IHeaderService headerService)
        {
            _headerService = headerService;
        }

        [HttpGet("project/{projectId:Guid}")]
        public async Task<HeaderNavView> GetHeaderForProject(Guid projectId)
        {
            var headerNavView = await _headerService.GetHeaderForProjectAsync(projectId);
            return headerNavView;
        }

        [HttpGet("customer/{customerId:Guid}")]
        public async Task<HeaderNavView> GetHeaderForCustomer(Guid customerId)
        {
            var headerNavView = await _headerService.GetHeaderForCustomerAsync(customerId);
            return headerNavView;
        }

    }
}