using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AKS.Builder.Components.Shared;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Components;

namespace AKS.Builder.Pages
{
    public class ProjectModel : AppStatePage
    {
        [Inject]
        protected ITopicService TopicService { get; set; }

        protected bool IsCreatingTopic { get; set; }

        protected TopicEdit NewTopic { get; set; }

        protected TopicSearch TopicSearch { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var pageTasks = new List<Task>();

            var baseTask = base.OnParametersSetAsync();
            pageTasks.Add(baseTask);

            await Task.WhenAll(pageTasks);

        }

        protected void CreateTopic(int topicTypeId)
        {
            NewTopic = new TopicEdit
            {
                ProjectId = ProjectId,
                TopicId = Guid.NewGuid(),
                TopicTypeId = topicTypeId,
            };
            IsCreatingTopic = true;
        }

        protected void CloseModal()
        {
            NewTopic = null;
            IsCreatingTopic = false;
        }

        protected async Task SaveNewTopic()
        {
            if (string.IsNullOrWhiteSpace(NewTopic.Title))
            {
                return;
            }

            try
            {
                await TopicService.SaveTopic(NewTopic);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            IsCreatingTopic = false;

            //var newTopicUrl = $"edit/topic/{ProjectId}/{NewTopic.TopicId}";
            //Microsoft.AspNetCore.Blazor.Browser.Services.BrowserUriHelper.Instance.NavigateTo(newTopicUrl);
            NewTopic = null;

            await TopicSearch.Search();
        }

        protected async Task DeleteTopics(List<TopicList> topics)
        {
            //var tasks = new List<Task>();
            foreach (var topic in topics)
            {
                await TopicService.DeleteTopic(topic.ProjectId, topic.TopicId);
            }

            //return Task.WhenAll(tasks);
        }
    }
}
