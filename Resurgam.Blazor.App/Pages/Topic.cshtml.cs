using Microsoft.AspNetCore.Blazor.Components;
using Resurgam.Infrastructure.Interfaces;
using Resurgam.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Blazor.App.Pages
{
    public class TopicModel : BlazorComponent
    {
        [Inject]
        protected ITopicService _topicService { get; set; }

        [Parameter]
        protected Guid ProjectId { get; set; }

        [Parameter]
        protected Guid TopicId { get; set; }

        protected TopicDisplayViewModel Topic { get; set; }

        protected override async Task OnInitAsync()
        {
            var pageTasks = new List<Task>();
            var topicTask = _topicService.GetTopicForDisplayAsync(ProjectId, TopicId);
            pageTasks.Add(topicTask);

            await Task.WhenAll(pageTasks);

            Topic = topicTask.Result;
        }

        protected Task Foo() => Task.CompletedTask;
    }
}
