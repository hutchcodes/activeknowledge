using AKS.App.Build.Api.Client;
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

        public List<TopicList> TopicList { get; set; } = new List<TopicList>();
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            await GetTopicList();
        }
        
        public async Task GetTopicList()
        {
            var topicViewAPI = new TopicViewApi();
            TopicList = await topicViewAPI.GetTopicListByProjectId(ProjectId);
            StateHasChanged();
        }

        public string GetTopicTypeDescription(int topicTypeId)
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
