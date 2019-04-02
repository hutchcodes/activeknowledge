using System;
using System.Threading.Tasks;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AKS.Share.Web.Security;

namespace AKS.Share.Web.Pages
{
    [Authorize]
    public class TopicModel : AKSProjectPage
    {
        private readonly ITopicService _topicService;
        private readonly PermissionsManager _permissionsManager;

        public TopicModel(IHeaderService projectService, ITopicService topciService, ICategoryService categoryService, PermissionsManager permissionsManager) : base(projectService, categoryService)
        {
            _topicService = topciService;
            _permissionsManager = permissionsManager;
        }
        public TopicView Topic { get; set; }

        public async Task<IActionResult> OnGet(Guid projectId, Guid topicId)
        {
            if (!await _permissionsManager.CanUserViewProject(HttpContext.User, projectId))
            {
               // return new ForbidResult();
            }

            var getHeaderTask = GetHeaderNav(null, projectId);
            var getTopicTask = _topicService.GetTopicForDisplay(projectId, topicId);
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