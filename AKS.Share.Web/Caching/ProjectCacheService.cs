using System;
using System.Threading.Tasks;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;
using AKS.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;

namespace AKS.Share.Web.Caching
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
        public async Task<HeaderNavView> GetHeaderForProjectAsync(Guid projectId)
        {
            var cacheKey = string.Format(_projectKeyTemplate, projectId);
            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(30);
                return await _projectService.GetHeaderForProjectAsync(projectId);
            });
        }

        public async Task<HeaderNavView> GetHeaderForCustomerAsync(Guid customerId)
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
