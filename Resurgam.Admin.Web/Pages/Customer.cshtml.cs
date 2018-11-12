using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Resurgam.AppCore.Entities;
using Resurgam.Infrastructure.Interfaces;
using Resurgam.Infrastructure.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Resurgam.Admin.Web.Pages
{
    public class CustomerModel : ResurgamPage
    {
        private readonly IProjectService _projectService;
        public CustomerModel(IHeaderService headerService, IProjectService projectService) : base(headerService)
        {
            _projectService = projectService;
        }

        public List<ProjectListViewModel> Projects { get; set; }

        public async Task OnGet(Guid customerId)
        {
            var pageTasks = new List<Task>();
            pageTasks.Add(GetHeaderNav(customerId, null));

            var projectTask = _projectService.GetProjetListForDisplayAsync(customerId);
            pageTasks.Add(projectTask);

            await Task.WhenAll(pageTasks);

            Projects = projectTask.Result;
        }
    }
}