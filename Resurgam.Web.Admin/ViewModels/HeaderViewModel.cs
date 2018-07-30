using Resurgam.AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Web.Admin.ViewModels
{
    public class HeaderNavViewModel
    {
        public HeaderNavViewModel(Customer customer)
        {
            BuildFromCustomer(customer);
        }

        public HeaderNavViewModel(Project project)
        {
            BuildFromProject(project);
        }

        private void BuildFromCustomer(Customer customer)
        {
            CustomerId = customer.Id;
            CustomerName = customer.Name;
            CustomerLogoURL = customer.LogoFileName.ToString();
        }

        private void BuildFromProject(Project project)
        {
            BuildFromCustomer(project.Customer);
            ProjectId = project.Id;
            ProjectName = project.Name;
            ProjectLogoURL = project.LogoFileName.ToString();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLogoURL { get; set; }
        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectLogoURL { get; set; }        
    }
}
