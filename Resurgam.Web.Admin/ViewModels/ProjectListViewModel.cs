using Resurgam.AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Web.Admin.ViewModels
{
    public class ProjectListViewModel
    {
        public ProjectListViewModel(Project project)
        {
            ProjectId = project.Id;
            ProjectName = project.Name;
        }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
    }
}
