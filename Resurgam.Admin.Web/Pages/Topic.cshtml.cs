using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Resurgam.Infrastructure.Interfaces;
using Resurgam.Infrastructure.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Resurgam.Admin.Web.Security;
using Microsoft.Extensions.Configuration;

namespace Resurgam.Admin.Web.Pages
{
    [Authorize]
    public class TopicModel : ResurgamProjectPage
    {
        private readonly ITopicService _topicService;
        private readonly PermissionsManager _permissionsManager;
        private readonly IConfiguration _config;

        public TopicModel(IHeaderService projectService, ITopicService topciService, ICategoryService categoryService, PermissionsManager permissionsManager, IConfiguration config) : base(projectService, categoryService)
        {
            _config = config;
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