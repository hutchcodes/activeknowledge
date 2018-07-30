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
    public class ProjectModel : ResurgamPage
    {
        private readonly ITopicService _topicService;
        public ProjectModel(IHeaderService headerService, ITopicService topicService) : base(headerService)
        {
            _topicService = topicService;
        }

        public List<TopicListViewModel> Topics { get; set; }

        public ITopicService TopicService => _topicService;

        public async Task OnGet(int projectId)
        {
            var pageTasks = new List<Task>();
            pageTasks.Add(GetHeaderNav(null, projectId));

            var topicTask = _topicService.GetTopicListForProject(projectId);
            pageTasks.Add(topicTask);

            await Task.WhenAll(pageTasks);

            Topics = topicTask.Result;
        }       
    }
}