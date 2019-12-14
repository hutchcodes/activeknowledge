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
    public class CategoryService : ICategoryService
    {
        private readonly ILogger<CategoryService> _logger;
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Category> _categoryRepo;
        public CategoryService(IMapper mapper, ILoggerFactory loggerFactory, IAsyncRepository<Category> categoryRepo)
        {
            _logger = loggerFactory.CreateLogger<CategoryService>();
            _mapper = mapper;
            _categoryRepo = categoryRepo;

            _logger.LogDebug($"New instance of {GetType().Name} was created");
        }

        public async Task<CategoryTreeView> GetCategoryTreeAsync(Guid projectId, Guid? categoryId, Guid? topicId)
        {
            var spec = new CategoryListSpecification(projectId);
            var categories = await _categoryRepo.ListAsync(spec);

            var catTree = new CategoryTreeView
            {
                Categories = _mapper.Map<List<CategoryTreeView>>(categories)
            };

            return catTree;
        }
    }
}
