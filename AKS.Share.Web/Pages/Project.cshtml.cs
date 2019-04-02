using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;

namespace AKS.Share.Web.Pages
{
    public class ProjectModel : AKSProjectPage
    {
        private readonly ITopicService _topicService;
        public ProjectModel(IHeaderService headerService, ITopicService topicService, ICategoryService categoryService) : base(headerService, categoryService)
        {
            _topicService = topicService;
        }

        public List<TopicList> Topics { get; set; }

        public ITopicService TopicService => _topicService;

        public async Task OnGet(Guid projectId)
        {
            var pageTasks = new List<Task>
            {
                GetHeaderNav(null, projectId)
            };

            var topicTask = _topicService.GetTopicListForProject(projectId);
            pageTasks.Add(topicTask);

            await Task.WhenAll(pageTasks);

            Topics = topicTask.Result;
        }       
    }
}