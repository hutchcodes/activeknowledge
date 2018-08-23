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

namespace Resurgam.Admin.Web.Pages
{
    [Authorize]
    public class TopicEditModel : ResurgamProjectPage
    {
        private readonly ITopicService _topicService;
        private readonly PermissionsManager _permissionsManager;

        public TopicEditViewModel Topic { get; set; }

        public TopicEditModel(IHeaderService projectService, ITopicService topciService, ICategoryService categoryService, PermissionsManager permissionsManager) : base(projectService, categoryService)
        {
            _topicService = topciService;
            _permissionsManager = permissionsManager;
        }

        public async Task<IActionResult> OnGet(int projectId, int topicId)
        {
            if (!await _permissionsManager.CanUserViewProject(HttpContext.User, projectId))
            {
               // return new ForbidResult();
            }

            var getHeaderTask = GetHeaderNav(null, projectId);
            var getTopicTask = _topicService.GetTopicForEditAsync(projectId, topicId);
            var getCategoryTreeTask = GetCategoryTree(projectId);

            await getHeaderTask;
            await getTopicTask;
            await getCategoryTreeTask;

            await Task.WhenAll(getHeaderTask, getTopicTask, getCategoryTreeTask);
            Topic = getTopicTask.Result;


            return Page();
        }

        public async Task<IActionResult> OnPost(TopicEditViewModel topic)
        {
            //await _topicService.SaveTopicAsync(topic);
            return await OnGet(topic.ProjectId, topic.TopicId);
        }
    }
}