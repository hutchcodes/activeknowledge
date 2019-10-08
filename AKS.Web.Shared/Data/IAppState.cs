using AKS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.App.Core.Data
{
    public interface IAppState
    {
        public bool TocIsVisible { get; set; }
        public string TocCollapsedClass { get; }
        public string TocContentClass { get; }
        public Guid CustomerId { get; }

        public Guid ProjectId { get; }

        public HeaderNavView? HeaderNav { get; set; }
        public CategoryTreeView CategoryTree { get; set; } 

        public Task UpdateCustomerAndProject(Guid? customerId, Guid? projectId);

        public delegate void AppStateChangeHandler(object sender, EventArgs e);
        public event AppStateChangeHandler? OnUpdateStatus;
    }
}
