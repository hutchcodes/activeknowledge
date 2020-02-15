using AKS.Api.Build.Client;
using AKS.App.Core.Data;
using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AKS.App.Core
{
    public partial class CategoriesEditBase : ComponentBase
    {
        [Parameter]
        public Guid ProjectId { get; set; }

        [Inject]
        public CategoryEditApi CategoryEditApi { get; set; } = null!;

        [CascadingParameter] protected IAppState AppState { get; set; } = null!;

        public CategoryTree CategoryTree { get; set; } = new CategoryTree();

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            var headerTask = AppState.UpdateCustomerAndProject(null, ProjectId);
            var categoryTask = GetCategoryTree();
            await headerTask;
            await categoryTask;

        }

        public async Task GetCategoryTree()
        {
            CategoryTree = await CategoryEditApi.GetCategoryTreeForProject(ProjectId);

            var t1 = new CategoryTree { Name = "Test 1", ProjectId = ProjectId, CategoryId = Guid.NewGuid() };
            var t2 = new CategoryTree { Name = "Test 2", ProjectId = ProjectId, CategoryId = Guid.NewGuid() };
            var t11 = new CategoryTree { Name = "Test 1.1", ProjectId = ProjectId, CategoryId = Guid.NewGuid() };
            var t12 = new CategoryTree { Name = "Test 1.2", ProjectId = ProjectId, CategoryId = Guid.NewGuid() };
            var t121 = new CategoryTree { Name = "Test 1.2.1", ProjectId = ProjectId, CategoryId = Guid.NewGuid() };

            CategoryTree.Categories.Add(t1);
            CategoryTree.Categories.Add(t2);
            t1.Categories.Add(t11);
            t1.Categories.Add(t12);
            t12.Categories.Add(t121);

            StateHasChanged();
        }

        public async Task Save()
        {
            if (CategoryTree != null)
            {
                CategoryTree = await CategoryEditApi.SaveCategoryTree(CategoryTree);
            }
        }

        public async Task Cancel()
        {
            await GetCategoryTree();
        }

        public bool CanDrop(object dropping, object dropOn)
        {
            return true;
        }

        public void AddCategory(CategoryTree? category)
        {
            if(category == null)
            {
                category = CategoryTree;
            }
            category.Categories.Add(new CategoryTree { Name = "Test", ProjectId = ProjectId, CategoryId = Guid.NewGuid() });
        }

        public void AddTopic(CategoryTree category)
        {
            Console.WriteLine("here");
            category.Topics.Add(new TopicLink() { Title = "TestTopic" });
            Console.WriteLine("There");
        }

    }
}
