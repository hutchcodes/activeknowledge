using Resurgam.Infrastructure.Interfaces;
using Resurgam.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Builder.App
{
    public class AppState 
    {
        private readonly IHeaderService _headerService;
        private readonly ICategoryService _categoryService;

        public AppState(IHeaderService headerService, ICategoryService categoryService)
        {
            _headerService = headerService;
            _categoryService = categoryService;
        }
        public Guid CustomerId { get; set; }

        public Guid ProjectId { get; private set; }

        public HeaderNavViewModel HeaderNav { get; set; }
        public CategoryTreeViewModel CategoryTree { get; set; }

        public async Task LoadProjectInfo(Guid projectId)
        {
            if (projectId == ProjectId)
            {
                return;
            }
            ProjectId = projectId;

            var getHeaderTask = _headerService.GetHeaderForProjectAsync(projectId);
            var getCategoryTreeTask = _categoryService.GetCategoryTreeAsync(projectId);

            await Task.WhenAll(getHeaderTask, getCategoryTreeTask);

            HeaderNav = getHeaderTask.Result;
            CategoryTree = getCategoryTreeTask.Result;
            OnUpdateStatus(this, new EventArgs());
        }

        public delegate void AppStateChangeHandler(object sender, EventArgs e);
        public event AppStateChangeHandler OnUpdateStatus;
    }
}
