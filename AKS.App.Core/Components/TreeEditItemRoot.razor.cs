using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AKS.App.Core.Components
{
    public interface ITreeEditItem<TItem>
    {
        public IList<TItem> Items { get; set; }
        public ITreeEditItem<TItem>? Parent { get; }
        public TItem Item { get; }
        public Guid Id { get; }
        }
    public partial class TreeEditItemRoot<TItem> : ComponentBase, ITreeEditItem<TItem> 
    {
        [Parameter] public RenderFragment<TItem>? DisplayTemplate { get; set; }
        [Parameter] public RenderFragment<TItem>? ChildTemplate { get; set; }
        [Parameter] public RenderFragment<CategoryTopicList>? TopicTemplate { get; set; }
        [Parameter] public IList<TItem> Items { get; set; } = new List<TItem>();
        [Parameter] public EventCallback<TreeEditItem<TItem>?> OnStatusUpdated { get; set; }
        [Parameter] public Func<TItem, TItem, Task>? RemoveItemAction { get; set; }

        public TreeEditOptions<TItem> Options { get; set; }
        public ITreeEditItem<TItem>? Parent { get; } //Should always be null
        public TItem Item { get; } //Should always be null
        public Guid Id { get; } = Guid.NewGuid();

        public TreeEditItem<TItem>? Payload { get; set; }
        public TreeEditItem<TItem>? CurrentNode { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Options = new TreeEditOptions<TItem>()
            {
                Root = this,
                ChildTemplate = ChildTemplate,
                DisplayTemplate = DisplayTemplate,
                TopicTemplate = TopicTemplate,
                RemoveItemAction = RemoveItemAction,
            };
        }
        public async Task UpdateJobAsync()
        {
            StateHasChanged();

            await OnStatusUpdated.InvokeAsync(Payload);
        }
    }
}
