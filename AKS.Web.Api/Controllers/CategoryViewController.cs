using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AKS.App.Build.Api.Controllers
{
    [Route("api/[controller]")]
    public class CategoryViewController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryViewController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("project/{projectId:Guid}")]
        public async Task<CategoryTreeView> GetHeaderForProject(Guid projectId)
        {
            var categoryTree = await _categoryService.GetCategoryTreeAsync(projectId);
            return categoryTree;
        }
    }
}