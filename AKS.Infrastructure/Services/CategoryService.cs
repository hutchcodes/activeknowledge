using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AKS.AppCore.Entities;
using AKS.AppCore.Interfaces;
using AKS.AppCore.Specifications;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace AKS.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ILogger<CategoryService> _logger;
        private readonly IAsyncRepository<Category> _categoryRepo;
        public CategoryService(ILoggerFactory loggerFactory, IAsyncRepository<Category> categoryRepo)
        {
            _logger = loggerFactory.CreateLogger<CategoryService>();
            _categoryRepo = categoryRepo;

            _logger.LogDebug($"New instance of {GetType().Name} was created");
        }

        public async Task<CategoryTreeView> GetCategoryTreeAsync(Guid projectId, Guid? categoryId, Guid? topicId)
        {
            var spec = new CategoryListSpecification(projectId);
            var categories = await _categoryRepo.ListAsync(spec);

            var catTree = new CategoryTreeView();

            catTree = BuildTree(catTree, categories);

            return catTree;
        }

        private CategoryTreeView BuildTree(CategoryTreeView catTree, IEnumerable<Category> categories)
        {
            foreach (var cat in categories)
            {
                var subCatTree = new CategoryTreeView
                {
                    CategoryId = cat.CategoryId,
                    CategoryName = cat.Name
                };

                foreach (var top in cat.Topics)
                {
                    catTree.Topics.Add(
                        new TopicLink
                        {
                            ProjectId = top.ProjectId,
                            TopicId = top.TopicId,
                            TopicName = top.Topic.Name,
                            TopicDescription = top.Topic.Description
                        }
                    );
                }

                subCatTree = BuildTree(subCatTree, cat.Categories);

                catTree.Categories.Add(subCatTree);
            }            

            return catTree;
        }


    }
}
