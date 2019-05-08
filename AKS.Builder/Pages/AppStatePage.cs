using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Builder.Pages
{
    public class AppStatePage : ComponentBase
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
