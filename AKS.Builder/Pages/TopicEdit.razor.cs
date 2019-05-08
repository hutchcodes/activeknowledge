using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AKS.Builder.Shared;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Components;

namespace AKS.Builder.Pages
{
    public class TopicModel : AppStatePage
    {
        [Inject]
        protected ITopicService TopicService { get; set; }

        [Parameter]
        protected Guid TopicId { get; set; }

        protected Common.Models.TopicEdit Topic { get; set; }

        protected ContentTopicEditor ContentEditor { get; set; }

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
            Topic = await TopicService.GetTopicForEdit(ProjectId, TopicId);
        }

        protected async Task Save()
        {
            if (ContentEditor?.TopicContentEditor != null)
            {
                Topic.Content = await ContentEditor.TopicContentEditor.GetEditorText();
            }
            await TopicService.SaveTopic(Topic);
        }

        protected async Task Cancel()
        {
            await LoadTopic();
        }

        protected void ContentChangeHandler(string newContent)
        {
            Topic.Content = newContent;
        }
    }
}
