using Resurgam.AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Infrastructure.ViewModels
{
    public class ProjectListViewModel
    {
        public ProjectListViewModel(Project project)
        {
            ProjectId = project.ProjectId;
            ProjectName = project.Name;
        }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
    }
}
