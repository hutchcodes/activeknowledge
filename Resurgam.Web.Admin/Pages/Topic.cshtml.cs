using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Resurgam.Web.Admin.Interfaces;
using Resurgam.Web.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Resurgam.Web.Admin.Pages
{
    public class TopicModel : ResurgamProjectPage
    {
        private readonly ITopicService _topicService;
        public TopicModel(IHeaderService projectService, ITopicService topciService, ICategoryService categoryService) : base(projectService, categoryService)
        {
            _topicService = topciService;
        }
        public TopicDisplayViewModel Topic { get; set; }

        public async Task OnGet(int projectId, int topicId)
        {
            var getHeaderTask = GetHeaderNav(null, projectId);
            var getTopicTask = _topicService.GetTopicForDisplayAsync(projectId, topicId);
            var getCategoryTreeTask = GetCategoryTree(projectId);

            await Task.WhenAll(getHeaderTask, getTopicTask, getCategoryTreeTask);
            Topic = getTopicTask.Result;
        }
    }
}