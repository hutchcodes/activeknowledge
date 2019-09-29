using AKS.App.Build.Api.Client;
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
        IAppState AppState { get; set; } = null!;

        public TopicView? Topic { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);
            await LoadTopic();
            StateHasChanged();
        }

        private async Task LoadTopic()
        {
            var topicViewAPI = new TopicViewApi();
            Topic = await topicViewAPI.GetTopic(ProjectId, TopicId);
        }

        protected string TocCollapsedClass
        {
            get
            {
                if (!AppState.TocIsVisible)
                {
                    return "collapse";
                }
                return "";
            }
        }

        protected string TopicContentClass
        {
            get
            {
                if (!AppState.TocIsVisible)
                {
                    return "";
                }
                return "col-lg-9";
            }
        }
    }
}
