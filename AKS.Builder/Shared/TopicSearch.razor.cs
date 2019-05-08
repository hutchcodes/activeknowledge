using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Components;

namespace AKS.Builder.Shared
{
    public class TopicSearchModel : ComponentBase
    {
        [Inject]
        protected ITopicService TopicService { get; set; }

        protected List<TopicList> Topics { get; set; }

        [Parameter]
        protected bool ShowSearch { get; set; } = false;
        [Parameter]
        protected bool ShowTopicSelected { get; set; } = false;

        [Parameter]
        protected Action<List<TopicList>> DoSomethingAction { get; set; }

        [Parameter]
        protected Func<List<TopicList>, Task> DeleteTopicFunc { get; set; }

        [Parameter]
        protected Guid? CustomerId { get; set; }
        [Parameter]
        protected Guid? ProjectId { get; set; }
        [Parameter]
        protected Guid? CategoryId { get; set; }
        [Parameter]
        protected string SearchString { get; set; }

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
                    Topics = await TopicService.GetTopicListForProject(ProjectId.Value);
                }
                else
                {
                    Topics = await TopicService.SearchTopics(ProjectId.Value, CategoryId, SearchString);
                }
            }
            StateHasChanged();
        }

        protected void DoSomething()
        {
            DoSomethingAction(Topics.Where(x => x.IsSelected).ToList());
        }

        protected async Task DeleteTopic()
        {
            await DeleteTopicFunc(Topics.Where(x => x.IsSelected).ToList());
        }
    }
}