using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.JSInterop;
using Resurgam.Blazor.App.Shared;
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

            var topicTask = LoadTopic();
            pageTasks.Add(topicTask);

            var baseTask = base.OnParametersSetAsync();
            pageTasks.Add(baseTask);

            await Task.WhenAll(pageTasks);
        }
        
        private async Task LoadTopic()
        {
            Topic = await _topicService.GetTopicForEdit(ProjectId, TopicId);
        }
        
        protected async Task Save()
        {
            await _topicService.SaveTopic(Topic);
        }

        protected async Task Cancel()
        {
            await LoadTopic();
        }
    }
}
