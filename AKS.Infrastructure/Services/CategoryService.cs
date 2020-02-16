using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKS.Infrastructure.Entities;
using AKS.Infrastructure.Interfaces;
using AKS.Infrastructure.Specifications;
using AKS.Common.Models;
using AKS.Common.Extensions;
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

        public async Task<List<CategoryTree>> GetCategoryTreeAsync(Guid projectId, Guid? categoryId, Guid? topicId)
        {
            var spec = new CategoryListSpecification(projectId);
            var categories = await _categoryRepo.ListAsync(spec);

            var categoryTrees = _mapper.Map<List<CategoryTree>>(categories);

            return GetTreeOfCategories(categoryTrees);
        }

        public async Task<List<CategoryTree>> SaveCategoryTreeAsync(Guid projectId, List<CategoryTree> categoryTrees)
        {
            var flat = GetFlatListOfCategories(categoryTrees);
            foreach (var cat in flat)
            {
                cat.ProjectId = projectId;
                await _categoryRepo.UpdateAsync<CategoryTree>(cat);
            }

            var spec = new CategoryListSpecification(projectId);
            var categories = await _categoryRepo.ListAsync(spec);

            categoryTrees = _mapper.Map<List<CategoryTree>>(categories);

            return GetTreeOfCategories(categoryTrees);
        }

        private List<CategoryTree> GetFlatListOfCategories(List<CategoryTree> categoryTree, Guid? parentCategoryId = null)
        {
            var flat = new List<CategoryTree>();
            for (var i= 0; i < categoryTree.Count; i++)
            {
                var cat = categoryTree[i];
                cat.ParentCategoryId = parentCategoryId;
                cat.Order = i;
                flat.Add(cat);
                flat.AddRange(GetFlatListOfCategories(cat.Categories, cat.CategoryId));
                cat.Categories.Clear();
            }
            return flat;
        }

        private List<CategoryTree> GetTreeOfCategories(List<CategoryTree> flat)
        {
            var categoryTrees = new List<CategoryTree>();

            var catDic = flat.ToDictionary(c => c.CategoryId, c => c);
            foreach (var cat in flat)
            {
                if (cat.ParentCategoryId.HasValue)
                {
                    catDic[cat.ParentCategoryId.Value].Categories.AddAtPosition(cat, cat.Order);
                }
                else
                {
                    categoryTrees.AddAtPosition(cat, cat.Order);
                }
            }
            return categoryTrees;
        }
    }
}
