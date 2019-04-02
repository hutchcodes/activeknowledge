using AKS.Builder.Components.Shared;
using AKS.Builder.Shared;
using Microsoft.AspNetCore.Components;
using AKS.Infrastructure.Interfaces;
using AKS.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Builder.Pages
{
    public class ProjectModel : AppStatePage
    {
        [Inject]
        protected ITopicService _topicService { get; set; }

        protected bool IsCreatingTopic { get; set; }

        protected TopicEditViewModel NewTopic { get; set; }

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
            NewTopic = new TopicEditViewModel
            {
                ProjectId = ProjectId,
                TopicId = Guid.NewGuid(),
                TopicTypeID = topicTypeId,
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
            if (string.IsNullOrWhiteSpace(NewTopic.TopicName))
            {
                return;
            }

            try
            {
                await _topicService.SaveTopic(NewTopic);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            IsCreatingTopic = false;

            var newTopicUrl = $"edit/topic/{ProjectId}/{NewTopic.TopicId}";
            //Microsoft.AspNetCore.Blazor.Browser.Services.BrowserUriHelper.Instance.NavigateTo(newTopicUrl);
            NewTopic = null;

            await TopicSearch.Search();
        }

        protected async Task DeleteTopics(List<TopicListViewModel> topics)
        {
            //var tasks = new List<Task>();
            foreach (var topic in topics)
            {
                await _topicService.DeleteTopic(topic.ProjectId, topic.TopicId);
            }

            //return Task.WhenAll(tasks);
        }
    }
}
