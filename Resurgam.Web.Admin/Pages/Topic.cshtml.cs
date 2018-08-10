using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Resurgam.Web.Admin.Interfaces;
using Resurgam.Web.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Resurgam.Web.Admin.Security;

namespace Resurgam.Web.Admin.Pages
{
    [Authorize]
    public class TopicModel : ResurgamProjectPage
    {
        private readonly ITopicService _topicService;
        private readonly PermissionsManager _permissionsManager;

        public TopicModel(IHeaderService projectService, ITopicService topciService, ICategoryService categoryService, PermissionsManager permissionsManager) : base(projectService, categoryService)
        {
            _topicService = topciService;
            _permissionsManager = permissionsManager;
        }
        public TopicDisplayViewModel Topic { get; set; }

        public async Task<IActionResult> OnGet(int projectId, int topicId)
        {
            if (!await _permissionsManager.CanUserViewProject(HttpContext.User, projectId))
            {
               // return new ForbidResult();
            }

            var getHeaderTask = GetHeaderNav(null, projectId);
            var getTopicTask = _topicService.GetTopicForDisplayAsync(projectId, topicId);
            var getCategoryTreeTask = GetCategoryTree(projectId);

            await getHeaderTask;
            await getTopicTask;
            await getCategoryTreeTask;

            await Task.WhenAll(getHeaderTask, getTopicTask, getCategoryTreeTask);
            Topic = getTopicTask.Result;

            return Page();
        }
    }
}