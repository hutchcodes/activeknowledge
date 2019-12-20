using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKS.Api.Build.Client;
using AKS.Common.Models;
using Microsoft.AspNetCore.Components;

namespace AKS.App.Core.Components
{
    public class TopicSearchModel : ComponentBase
    {
        [Inject]
        public TopicViewApi TopicViewApi { get; set; } = null!;

        public List<TopicList> Topics { get; set; } = new List<TopicList>();

        [Parameter]
        public bool ShowSearch { get; set; } = false;
        [Parameter]
        public bool ShowTopicSelected { get; set; } = false;

        [Parameter]
        public Action<List<TopicList>>? AddTopicsAction { get; set; }

        [Parameter]
        public Func<List<TopicList>, Task>? DeleteTopicFunc { get; set; }

        [Parameter]
        public Guid? CustomerId { get; set; }
        [Parameter]
        public Guid? ProjectId { get; set; }
        [Parameter]
        public Guid? CategoryId { get; set; }
        [Parameter]
        public string SearchString { get; set; } = "";

        [Parameter] public bool ShowAdd { get; set; }
        [Parameter] public bool ShowDelete { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await Search();
        }

        public async Task Search()
        {
            if (ProjectId.HasValue)
            {
                if (string.IsNullOrWhiteSpace(SearchString))
                {
                    Topics = await TopicViewApi.GetTopicListByProjectId(ProjectId.Value);
                }
                else
                {
                    Topics = await TopicViewApi.SearchProjectTopics(ProjectId.Value, CategoryId, SearchString);
                }
            }
            StateHasChanged();
        }

        protected void AddTopics()
        {
            AddTopicsAction?.Invoke(Topics.Where(x => x.IsSelected).ToList());
        }

        protected async Task DeleteTopic()
        {
            if (DeleteTopicFunc != null)
            {
                await DeleteTopicFunc(Topics.Where(x => x.IsSelected).ToList());
            }
        }
    }
}