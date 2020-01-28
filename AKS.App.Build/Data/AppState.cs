using AKS.Api.Build.Client;
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
        private readonly CategoryViewApi _categoryViewApi;

        public AppState(HeaderApi headerApiClient, CategoryViewApi categoryViewApi)
        {
            _headerApiClient = headerApiClient;
            _categoryViewApi = categoryViewApi;
        }
        public Guid CustomerId { get; private set; }

        public Guid ProjectId { get; private set; }

        public AKSUserOld User { get; set; } = new AKSUserOld();
        public HeaderNavView? HeaderNav { get; set; }
        public CategoryTreeView CategoryTree { get; set; } = new CategoryTreeView();

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
            var getCategoryTreeTask = _categoryViewApi.GetCategoryTreeForProject(projectId);

            HeaderNav = await getHeaderTask;
            CategoryTree = await getCategoryTreeTask ?? new CategoryTreeView(); ;
            CustomerId = HeaderNav.CustomerId;
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
            CategoryTree = new CategoryTreeView(); ;
        }

        public event IAppState.AppStateChangeHandler? OnUpdateStatus;

        private bool _tocIsVisible = false;
        public bool TocIsVisible
        {
            get => _tocIsVisible;
            set
            {
                _tocIsVisible = value;
                OnUpdateStatus?.Invoke(this, new EventArgs());
            }
        }

        public string TocCollapsedClass => TocIsVisible ? "" : "collapse";

        public string TocContentClass => TocIsVisible ? "col-lg-9" : "";
    }
}



