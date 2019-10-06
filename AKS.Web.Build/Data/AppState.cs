using AKS.App.Build.Api.Client;
using AKS.App.Core.Data;
using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.App.Build
{
    public class AppState : IAppState
    {
        private readonly HeaderApi _headerApiClient;

        public AppState(HeaderApi headerApiClient)
        {
            _headerApiClient = headerApiClient;
        }
        public Guid CustomerId { get; private set; }

        public Guid ProjectId { get; private set; }

        public HeaderNavView? HeaderNav { get; set; }
        public CategoryTreeView? CategoryTree { get; set; }

        public async Task UpdateCustomerAndProject(Guid? customerId, Guid? projectId)
        {
            if (projectId != null)
            {
                await LoadProjectInfo(projectId.Value);
            }
            if (projectId == null && customerId != null)
            {
                await LoadCustomerInfo(customerId.Value);
            }
            
        }
        private async Task LoadProjectInfo(Guid projectId)
        {
            if (projectId == ProjectId)
            {
                return;
            }
            ProjectId = projectId;

            var getHeaderTask = _headerApiClient.GetHeaderForProject(projectId);
            //var getCategoryTreeTask = _categoryService.GetCategoryTreeAsync(projectId);

            await Task.WhenAll(getHeaderTask);//, getCategoryTreeTask);

            HeaderNav = getHeaderTask.Result;
            CustomerId = HeaderNav.CustomerId;
            //CategoryTree = getCategoryTreeTask.Result;
            OnUpdateStatus?.Invoke(this, new EventArgs());
        }

        private async Task LoadCustomerInfo(Guid customerId)
        {
            if (customerId == CustomerId && ProjectId == Guid.Empty) 
            {
                return;
            }
            CustomerId = customerId;

            var getHeaderTask = _headerApiClient.GetHeaderForCustomer(customerId);

            HeaderNav = await getHeaderTask;
        }

        public event IAppState.AppStateChangeHandler? OnUpdateStatus;

        public bool TocIsVisible { get; set; } = true;
    }
}


