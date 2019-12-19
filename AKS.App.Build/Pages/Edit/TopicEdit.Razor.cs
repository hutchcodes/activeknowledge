using AKS.Api.Build.Client;
using AKS.App.Core.Data;
using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
        
        [Inject]
        NavigationManager NavMan { get; set; } = null!;

        [Inject]
        protected IJSRuntime JsRuntime { get; set; } = null;

        [CascadingParameter] protected IAppState AppState { get; set; } = null!;

        public TopicEdit Topic { get; set; } = new TopicEdit();

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);
            var headerTask = AppState.UpdateCustomerAndProject(null, ProjectId);
            
            var lastSegment = new Uri(NavMan.Uri).Segments[^1].ToLower();
            if (lastSegment == "new")
            {
                NewTopic();
            }
            else
            {
                var topicTask = LoadTopic();
                await topicTask;
            }
            
            await headerTask;
  
            StateHasChanged();
        }

        private void NewTopic()
        {
            Topic = new TopicEdit
            {
                ProjectId = this.ProjectId,
                TopicId = Guid.NewGuid(),
                TopicType = Common.Enums.TopicType.Content,
                TopicStatus = Common.Enums.TopicStatus.New
            };
            TopicId = Topic.TopicId;
        }
        private async Task LoadTopic()
        {
            Topic = await TopicEditApi.GetTopic(ProjectId, TopicId);
        }

        public async Task Save()
        {
            if (Topic.TopicStatus == Common.Enums.TopicStatus.New)
            {
                Topic = await TopicEditApi.UpdateTopic(Topic);
                NavMan.NavigateTo($"/topic/{Topic.ProjectId}/{Topic.TopicId}/edit");
            }
            else
            {
                Topic = await TopicEditApi.UpdateTopic(Topic);
            }
                
        }

        public async Task Cancel()
        {
            if (Topic.TopicStatus == Common.Enums.TopicStatus.New)
            {
                NewTopic();
            }
            else
            {
                await LoadTopic();
            }
        }

        public async Task DeleteTopic()
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm",  "Are you sure?");
            if (confirmed)
            {
                await TopicEditApi.DeleteTopic(ProjectId, TopicId);
                NavMan.NavigateTo($"/project/{ProjectId}");
            }
        }
    }
}
