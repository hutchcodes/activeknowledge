using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using AKS.Builder.App.Shared;
using Resurgam.Infrastructure.Interfaces;
using Resurgam.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Builder.App.Pages
{
    public class TopicModel : AppStatePage
    {
        [Inject]
        protected ITopicService _topicService { get; set; }

        [Parameter]
        protected Guid TopicId { get; set; }

        protected TopicEditViewModel Topic { get; set; }

        protected CKEditor.Blazor.CKEditorControl TopicContentEditor { get; set; }

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
            if (TopicContentEditor != null)
            {
                Topic.TopicContent = await TopicContentEditor.GetEditorText();
            }
            await _topicService.SaveTopic(Topic);
        }

        protected async Task Cancel()
        {
            await LoadTopic();
        }

        protected void contentChangeHandler(string newContent)
        {
            Topic.TopicContent = newContent;
        }
    }
}
