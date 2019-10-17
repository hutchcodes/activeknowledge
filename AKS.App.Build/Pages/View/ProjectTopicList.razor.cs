using AKS.Api.Build.Client;
using AKS.App.Core.Data;
using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AKS.App.Core
{
    public class ProjectTopicListBase : ComponentBase
    {
        [Parameter]
        public Guid ProjectId { get; set; }

        [Inject]
        public TopicViewApi TopicViewApi { get; set; } = null!;

        [CascadingParameter] protected IAppState AppState { get; set; } = null!;

        public List<TopicList> TopicList { get; set; } = new List<TopicList>();
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            var headerTask = AppState.UpdateCustomerAndProject(null, ProjectId);
            var topicTask = GetTopicList();
            await headerTask;
            await topicTask;
            //StateHasChanged();
        }
        
        public async Task GetTopicList()
        {
            TopicList = await TopicViewApi.GetTopicListByProjectId(ProjectId);
            StateHasChanged();
        }

        public static string GetTopicTypeDescription(int topicTypeId)
        {
            return topicTypeId switch
            {
                1 => "Content",
                2 => "Collection",
                _ => "unknown",
            };
        }
    }
}
