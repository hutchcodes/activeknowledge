using AKS.Api.Build.Client;
using AKS.App.Core.Data;
using AKS.Common.Models;
using AKS.Common.Extensions;
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

        public List<CategoryTree> CategoryTrees { get; set; } = new List<CategoryTree>();

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
            CategoryTrees = await CategoryEditApi.GetCategoryTreeForProject(ProjectId);

            var t1 = new CategoryTree { Name = "Test 1", ProjectId = ProjectId, CategoryId = Guid.NewGuid() };
            var t2 = new CategoryTree { Name = "Test 2", ProjectId = ProjectId, CategoryId = Guid.NewGuid() };
            var t11 = new CategoryTree { Name = "Test 1.1", ProjectId = ProjectId, CategoryId = Guid.NewGuid() };
            var t12 = new CategoryTree { Name = "Test 1.2", ProjectId = ProjectId, CategoryId = Guid.NewGuid() };
            var t121 = new CategoryTree { Name = "Test 1.2.1", ProjectId = ProjectId, CategoryId = Guid.NewGuid() };

            if (!CategoryTrees.Any())
            {
                CategoryTrees.Add(t1);
                CategoryTrees.Add(t2);
                t1.Categories.Add(t11);
                t1.Categories.Add(t12);
                t12.Categories.Add(t121);
            }
            StateHasChanged();
        }

        public async Task Save()
        {
            if (CategoryTrees != null)
            {
                CategoryTrees = await CategoryEditApi.SaveCategoryTree(ProjectId, CategoryTrees);
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
            var newCat = new CategoryTree { Name = "Test", ProjectId = ProjectId, CategoryId = Guid.NewGuid() };
            if (category == null)
            {
                CategoryTrees.Add(newCat);
            }
            else
            {
                category.Categories.Add(newCat);
            }
        }

        public async Task RemoveCategory(CategoryTree? parent, CategoryTree category)
        {
            Console.WriteLine($"RemoveCategory: {parent?.Name} - {category.Name}");
            await CategoryEditApi.DeleteCategory(ProjectId, category.CategoryId);
            if (parent != null)
            {
                Console.WriteLine("RemoveFromParent");
                
                parent.Categories.Remove(category);
            }
            else
            {
                Console.WriteLine("RemoveFromRoot");
                CategoryTrees.Remove(category);
            }
            StateHasChanged();
        }

        public void AddTopic(CategoryTree category)
        {
            Console.WriteLine("here");
            //category.Topics.Add(new CategoryTopicList() { Title = "TestTopic" });
            var topic = new TopicList
            {
                ProjectId = Guid.Parse("74171969-a00a-424f-8683-a9e0d0e252e8"),
                TopicId = Guid.Parse("b60faad7-7d07-4801-9b5e-e59bbeb5884f"),
                TopicType = Common.Enums.TopicType.Content,
                Title = "NewTopic"
            };

            var ctl = new CategoryTopicList(topic);

            category.Topics.Add(ctl);
            Console.WriteLine("There");
        }

        public void RemoveTopic(CategoryTree category, CategoryTopicList topic)
        {
            category.Topics.Remove(topic);
        }

    }
}
