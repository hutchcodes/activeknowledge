using Resurgam.Web.Admin.Interfaces;
using Resurgam.Web.Admin.Services;
using Resurgam.Web.Admin.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Web.Admin.Caching
{
    public class HeaderCacheService : IHeaderService
    {
        private readonly IMemoryCache _cache;
        private readonly HeaderService _projectService;
        private static readonly string _projectKeyTemplate = "projectHeader-{0}";
        private static readonly string _customerKeyTemplate = "cutomerHeader-{0}";

        public HeaderCacheService(IMemoryCache cache, HeaderService projectService)
        {
            _cache = cache;
            _projectService = projectService;

        }
        public async Task<HeaderNavViewModel> GetHeaderForProjectAsync(int projectId)
        {
            var cacheKey = string.Format(_projectKeyTemplate, projectId);
            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(30);
                return await _projectService.GetHeaderForProjectAsync(projectId);
            });
        }

        public async Task<HeaderNavViewModel> GetHeaderForCustomerAsync(int customerId)
        {
            var cacheKey = string.Format(_customerKeyTemplate, customerId);
            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(30);
                return await _projectService.GetHeaderForCustomerAsync(customerId);
            });
        }
    }
}
