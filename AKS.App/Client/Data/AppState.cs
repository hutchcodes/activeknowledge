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

        public AKSUser User { get; set; } = new AKSUser();
        public HeaderNavView? HeaderNav { get; set; }
        public List<CategoryTree> CategoryTree { get; set; } = new List<CategoryTree>();

        public async Task UpdateCustomerAndProject(Guid? customerId, Guid? projectId, bool forceReload = false)
        {
            if (forceReload)
            {
                CustomerId = Guid.Empty;
                ProjectId = Guid.Empty;
            }
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
            CategoryTree = await getCategoryTreeTask ?? new List<CategoryTree>(); ;
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
            CategoryTree = new List<CategoryTree>();
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



