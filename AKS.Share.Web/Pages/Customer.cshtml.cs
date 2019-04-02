using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;

namespace Resurgam.Admin.Web.Pages
{
    public class CustomerModel : ResurgamPage
    {
        private readonly IProjectService _projectService;
        public CustomerModel(IHeaderService headerService, IProjectService projectService) : base(headerService)
        {
            _projectService = projectService;
        }

        public List<ProjectList> Projects { get; set; }

        public async Task OnGet(Guid customerId)
        {
            var pageTasks = new List<Task>
            {
                GetHeaderNav(customerId, null)
            };

            var projectTask = _projectService.GetProjetListForDisplayAsync(customerId);
            pageTasks.Add(projectTask);

            await Task.WhenAll(pageTasks);

            Projects = projectTask.Result;
        }
    }
}