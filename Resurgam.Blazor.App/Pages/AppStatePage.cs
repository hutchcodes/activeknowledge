using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Blazor.App.Pages
{
    public class AppStatePage : BlazorComponent
    {
        [Inject]
        protected AppState AppState { get; set; }

        [Parameter]
        protected Guid ProjectId { get; set; }

        protected override Task OnParametersSetAsync()
        {
            return AppState.LoadProjectInfo(ProjectId);            
        }
    }
}
