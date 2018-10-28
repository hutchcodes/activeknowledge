using Microsoft.AspNetCore.Blazor.Components;
using Resurgam.Infrastructure.Interfaces;
using Resurgam.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Blazor.App.Pages
{
    public class TopicModel : AppStatePage
    {
        [Inject]
        protected ITopicService _topicService { get; set; }

        [Parameter]
        protected Guid TopicId { get; set; }

        protected TopicEditViewModel Topic { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var pageTasks = new List<Task>();

            var topicTask = _topicService.GetTopicForEditAsync(ProjectId, TopicId);
            pageTasks.Add(topicTask);

            var baseTask = base.OnParametersSetAsync();
            pageTasks.Add(baseTask);

            await Task.WhenAll(pageTasks);

            Topic = topicTask.Result;
        }

        protected Task Foo() => Task.CompletedTask;
    }
}
