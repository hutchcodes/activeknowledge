using AKS.Api.Build.Client;
using AKS.App.Core.Data;
using AKS.Common.Enums;
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

        [Inject]
        public NavigationManager NavMan { get; set; } = null!;

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

        public void NewTopic()
        {
            NavMan.NavigateTo($"/topic/{ProjectId}/new");
        }

        public void EditTopic(Guid topicId)
        {
            NavMan.NavigateTo($"/topic/{ProjectId}/{topicId}/edit");
        }

        public static string GetTopicTypeDescription(TopicType topicType)
        {
            return topicType switch
            {
                TopicType.Content => "Content",
                TopicType.Collection => "Collection",
                TopicType.Document => "Document",
                TopicType.Fragment => "Fragment",
                _ => "unknown",
            };
        }
    }
}
