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
        public async Task<List<CategoryTree>> GetCategoryTreeForProject(Guid projectId)
        {
            var categoryTrees = await _categoryService.GetCategoryTreeAsync(projectId);
            return categoryTrees;
        }

        [HttpPost("project/{projectId:Guid}")]
        public async Task<List<CategoryTree>> SaveCategoryTreeForProject(Guid projectId, List<CategoryTree> categoryTrees)
        {
            categoryTrees = await _categoryService.SaveCategoryTreeAsync(projectId, categoryTrees);
            return categoryTrees;
        }
    }
}