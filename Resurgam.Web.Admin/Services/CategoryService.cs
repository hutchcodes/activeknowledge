using Resurgam.AppCore.Entities;
using Resurgam.AppCore.Interfaces;
using Resurgam.AppCore.Specifications;
using Resurgam.Infrastructure.Data;
using Resurgam.Web.Admin.Interfaces;
using Resurgam.Web.Admin.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Web.Admin.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ILogger<CategoryService> _logger;
        private readonly IAsyncRepository<Category> _categoryRepo;
        public CategoryService(ILoggerFactory loggerFactory, IAsyncRepository<Category> categoryRepo)
        {
            _logger = loggerFactory.CreateLogger<CategoryService>();
            _categoryRepo = categoryRepo;
        }

        public async Task<CategoryTreeViewModel> GetCategoryTreeAsync(int projectId, int? categoryId, int? topicId)
        {
            var spec = new CategoryListSpecification(projectId);
            var category = await _categoryRepo.ListAsync(spec);

            var catTreeVM = new CategoryTreeViewModel(category);
            return catTreeVM;
        }


    }
}
