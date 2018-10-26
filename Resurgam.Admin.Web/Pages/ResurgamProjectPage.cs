using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Resurgam.Infrastructure.Interfaces;
using Resurgam.Infrastructure.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Resurgam.Admin.Web.Pages
{
    public abstract class ResurgamProjectPage : ResurgamPage 
    {
        protected readonly ICategoryService _categoryService;

        public ResurgamProjectPage(IHeaderService headerService, ICategoryService categoryService) : base (headerService)
        {
            _categoryService = categoryService;
        }
        public CategoryTreeViewModel CategoryTree { get; set; }

        public async Task GetCategoryTree(Guid projectId)
        {
            CategoryTree = await _categoryService.GetCategoryTreeAsync(projectId);
        }
    }
}