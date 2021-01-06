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
    public class ProjectEditBase : ComponentBase
    {

        [Parameter]
        public Guid ProjectId { get; set; }

        [Inject]
        public ProjectEditApi ProjectEditApi { get; set; } = null!;

        [CascadingParameter] protected IAppState AppState { get; set; } = null!;

        public ProjectEdit Project { get; set; } = new ProjectEdit();

        public string ProjectName { get; set; } = "";
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            var headerTask = AppState.UpdateCustomerAndProject(null, ProjectId);
            var projectTask = GetProject();
            await headerTask;
            await projectTask;

        }

        public async Task GetProject()
        {
            Project = await ProjectEditApi.GetProject(ProjectId);
            ProjectName = Project?.Name ?? "";
            StateHasChanged();
        }

        public async Task Save()
        {
            if (Project != null)
            {
                Project = await ProjectEditApi.UpdateProject(Project);
            }
        }

        public async Task Cancel()
        {
            await GetProject();
        }
    }
}
