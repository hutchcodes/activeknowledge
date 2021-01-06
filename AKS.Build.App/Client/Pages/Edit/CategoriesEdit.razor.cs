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
using AKS.App.Core.Components;

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
        public TopicSearchModal TopicSearch { get; set; } = null!;

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);
            Console.WriteLine("Set Params");
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

                await SaveCategory(null, t1);
                await SaveCategory(t1, t11);
                await SaveCategory(t12, t121);
                Console.WriteLine("Added new categories");
            }
            StateHasChanged();
        }

        public async Task Save()
        {
            if (CategoryTrees != null)
            {
                CategoryTrees = await CategoryEditApi.SaveCategoryTree(ProjectId, CategoryTrees);
                Console.WriteLine("Save whole thing");
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

        public async Task AddCategory(CategoryTree? category)
        {
            var newCat = new CategoryTree { Name = "Test", ProjectId = ProjectId, CategoryId = Guid.NewGuid(), ParentCategoryId = category?.CategoryId };

            if (category == null)
            {
                CategoryTrees.Add(newCat);
            }
            else
            {
                category.Categories.Add(newCat);
            }
            await SaveCategory(category, newCat);
        }

        public async Task SaveCategory( CategoryTree category)
        {
            var catsToSave = new List<CategoryTree>();
            catsToSave.Add(category);

            await CategoryEditApi.SaveCategoryTree(ProjectId, catsToSave);
        }
        public async Task SaveCategory(CategoryTree? parentCategory, CategoryTree category)
        {
            Console.WriteLine($"save {category.Name}");
            category.ParentCategoryId = parentCategory?.CategoryId;

            var catsToSave = new List<CategoryTree>();
            catsToSave.Add(category);

            var reorderCategories = parentCategory?.Categories ?? CategoryTrees;

            foreach (var cat in reorderCategories)
            {
                if (cat.Order != reorderCategories.IndexOf(cat))
                {
                    cat.Order = reorderCategories.IndexOf(cat);
                    catsToSave.Add(cat);
                }
            }

            await CategoryEditApi.SaveCategoryTree(ProjectId, catsToSave);
        }

        public async Task RemoveCategory(CategoryTree? parent, CategoryTree category)
        {
            await CategoryEditApi.DeleteCategory(category.ProjectId, category.CategoryId);
            if (parent != null)
            {
                parent.Categories.Remove(category);
            }
            else
            {
                CategoryTrees.Remove(category);
            }
            StateHasChanged();
        }

        private CategoryTree? _currentCategory;
        public void AddTopic(CategoryTree category)
        {
            _currentCategory = category;
            TopicSearch.ShowModal();
        }

        public async Task AddTopicsAsync(List<TopicList> topics)
        {
            if (_currentCategory == null) return;
            foreach (var t in topics)
            {
                Console.WriteLine(t.Title);
                var ctl = new CategoryTopicList(t);
                _currentCategory.Topics.Add(ctl);
                await SaveTopic(_currentCategory, ctl);
            }
            _currentCategory = null;
            StateHasChanged();
        }


        public async Task SaveTopic(CategoryTree category, CategoryTopicList topic)
        {
            topic.CategoryId = category.CategoryId;

            var topicsToSave = new List<CategoryTopicList>();
            topicsToSave.Add(topic);

            foreach (var top in category.Topics.Where(x => x != topic))
            {
                if (top.Order != category.Topics.IndexOf(top))
                {
                    top.Order = category.Topics.IndexOf(top);
                    topicsToSave.Add(top);
                }
            }

            await CategoryEditApi.SaveCategoryTopics(topicsToSave);
        }

        public async Task RemoveTopic(CategoryTree category, CategoryTopicList topic)
        {
            Console.WriteLine("Cat " +category.Name);
            Console.WriteLine("Topic " + topic.Topic?.Title);
            await CategoryEditApi.DeleteCategoryTopic(category.ProjectId, category.CategoryId, topic.TopicId);
            category.Topics.Remove(topic);
        }

    }
}
