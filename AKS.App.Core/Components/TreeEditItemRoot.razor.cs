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
        public Guid Id { get; }
        }
    partial class TreeEditItemRoot<TItem> : ITreeEditItem<TItem> 
    {
        [Parameter] public RenderFragment<TItem>? DisplayTemplate { get; set; }
        [Parameter] public RenderFragment<TItem>? ChildTemplate { get; set; }
        [Parameter] public RenderFragment<TopicLink>? TopicTemplate { get; set; }
        [Parameter] public IList<TItem> Items { get; set; } = new List<TItem>();
        public ITreeEditItem<TItem>? Parent { get; } //Should always be null
        public Guid Id { get; } = Guid.NewGuid();
        [Parameter] public EventCallback<TreeEditItem<TItem>?> OnStatusUpdated { get; set; }

        public TreeEditItem<TItem>? Payload { get; set; }
        public TreeEditItem<TItem>? CurrentNode { get; set; }

        public async Task UpdateJobAsync()
        {
            StateHasChanged();

            await OnStatusUpdated.InvokeAsync(Payload);
        }
    }
}
