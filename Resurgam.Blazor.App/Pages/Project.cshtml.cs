using Microsoft.AspNetCore.Blazor.Components;
using Resurgam.Infrastructure.Interfaces;
using Resurgam.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Blazor.App.Pages
{
    public class ProjectModel :BlazorComponent
    {
        [Inject]
        protected ITopicService _topicService { get; set; }

        [Parameter]
        protected Guid ProjectId { get; set; }

        protected List<TopicListViewModel> Topics { get; set; }

        protected override async Task OnInitAsync()
        {
            var pageTasks = new List<Task>();
            var topicTask = _topicService.GetTopicListForProject(ProjectId);
            pageTasks.Add(topicTask);

            await Task.WhenAll(pageTasks);

            Topics = topicTask.Result;

            //await base.OnInitAsync();
        }
    }
}
