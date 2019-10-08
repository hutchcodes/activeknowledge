using AKS.App.Build.Api.Client;
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
    public class CustomerProjectListBase : ComponentBase
    {

        [Parameter]
        public Guid CustomerId { get; set; }

        [Inject]
        public ProjectViewApi ProjectViewApi { get; set; } = null!;

        [CascadingParameter] protected IAppState AppState { get; set; } = null!;

        public List<ProjectList> ProjectList { get; set; } = new List<ProjectList>();
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            var headerTask = AppState.UpdateCustomerAndProject(CustomerId, null);
            var projectTask = GetProjectList();
            await headerTask;
            await projectTask;

        }

        public async Task GetProjectList()
        {
            ProjectList = await ProjectViewApi.GetProjectListByCustomer(CustomerId);
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
