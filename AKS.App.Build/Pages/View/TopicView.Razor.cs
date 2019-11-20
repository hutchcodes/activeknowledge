using AKS.Api.Build.Client;
using AKS.App.Core.Data;
using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.App.Core
{
    public class TopicViewBase : ComponentBase
    {
        [Parameter]
        public Guid ProjectId { get; set; }
        [Parameter]
        public Guid TopicId { get; set; }       

        [Inject]
        TopicViewApi TopicViewApi { get; set; } = null!;

        [Inject]
        TopicEditApi TopicEditApi { get; set; } = null!;

        [Inject]
        NavigationManager NavMan { get; set; } = null!;

        [CascadingParameter] protected IAppState AppState { get; set; } = null!;

        public TopicView? Topic { get; set; }


        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);
            var headerTask = AppState.UpdateCustomerAndProject(null, ProjectId);
            var topicTask = LoadTopic();
            await headerTask;
            await topicTask;
            StateHasChanged();
        }

        private async Task LoadTopic()
        {
            Topic = await TopicViewApi.GetTopic(ProjectId, TopicId);
        }

        public void EditTopic()
        {
            NavMan.NavigateTo($"/topic/{ProjectId}/{TopicId}/edit");
        }
    }
}
