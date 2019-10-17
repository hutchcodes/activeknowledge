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
    public class TopicEditBase : ComponentBase
    {
        [Parameter]
        public Guid ProjectId { get; set; }
        [Parameter]
        public Guid TopicId { get; set; }       

        [Inject]
        TopicEditApi TopicEditApi { get; set; } = null!;

        [CascadingParameter] protected IAppState AppState { get; set; } = null!;

        public TopicEdit Topic { get; set; } = new TopicEdit();


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
            Topic = await TopicEditApi.GetTopic(ProjectId, TopicId);
        }

        public async Task Save()
        {
            Topic = await TopicEditApi.UpdateTopic(Topic);
        }

        public async Task Cancel()
        {
            await LoadTopic();
        }
    }
}
