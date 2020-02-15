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
    public class CategoryEditController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryEditController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("project/{projectId:Guid}")]
        public async Task<CategoryTree> GetCategoryTreeForProject(Guid projectId)
        {
            var categoryTree = await _categoryService.GetCategoryTreeAsync(projectId);
            return categoryTree;
        }

        [HttpPost]
        public async Task<CategoryTree> SaveCategoryTreeForProject(CategoryTree categoryTree)
        {
            categoryTree = await _categoryService.SaveCategoryTreeAsync(categoryTree);
            return categoryTree;
        }
    }
}