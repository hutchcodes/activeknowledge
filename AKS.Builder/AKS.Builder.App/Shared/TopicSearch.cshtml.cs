using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Resurgam.Infrastructure.Interfaces;
using Resurgam.Infrastructure.ViewModels;

namespace AKS.Builder.App.Shared
{
    public class TopicSearchModel : ComponentBase
    {
        [Inject]
        protected ITopicService _topicService { get; set; }

        protected List<TopicListViewModel> Topics { get; set; }

        [Parameter]
        protected bool ShowSearch { get; set; } = false;
        [Parameter]
        protected bool ShowTopicSelected { get; set; } = false;

        [Parameter]
        protected Action<List<TopicListViewModel>> DoSomethingAction { get; set; }

        [Parameter]
        protected Func<List<TopicListViewModel>, Task> DeleteTopicFunc { get; set; }

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
                    Topics = await _topicService.GetTopicListForProject(ProjectId.Value);
                }
                else
                {
                    Topics = await _topicService.SearchTopics(ProjectId.Value, CategoryId, SearchString);
                }
            }
            this.StateHasChanged();
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