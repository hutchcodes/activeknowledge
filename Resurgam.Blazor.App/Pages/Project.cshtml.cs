using Microsoft.AspNetCore.Blazor.Components;
using Resurgam.Infrastructure.Interfaces;
using Resurgam.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Blazor.App.Pages
{
    public class ProjectModel : AppStatePage
    { 
        [Inject]
        protected ITopicService _topicService { get; set; }

        protected List<TopicListViewModel> Topics { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var pageTasks = new List<Task>();

            var topicTask = _topicService.GetTopicListForProject(ProjectId);
            pageTasks.Add(topicTask);

            var baseTask = base.OnParametersSetAsync();
            pageTasks.Add(baseTask);

            await Task.WhenAll(pageTasks);

            Topics = topicTask.Result;
        }
    }
}
