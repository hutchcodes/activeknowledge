using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AKS.App.Core.Components
{
    public partial class TreeEditItem<TItem> : ITreeEditItem<TItem>
    {
        [CascadingParameter(Name = "Root")] public TreeEditItemRoot<TItem> Root { get; set; } = null!;
        [CascadingParameter(Name = "DisplayTemplate")] public RenderFragment<TItem>? DisplayTemplate { get; set; }
        [CascadingParameter(Name = "ChildTemplate")] public RenderFragment<TItem>? ChildTemplate { get; set; }
        [CascadingParameter(Name = "TopicTemplate")] public RenderFragment<TopicLink>? TopicTemplate { get; set; }
        [CascadingParameter(Name = "Parent")] public ITreeEditItem<TItem> Parent { get; set; } = null!;
        public Guid Id { get; } = Guid.NewGuid();
        [Parameter] public TItem Item { get; set; }
        [Parameter] public IList<TItem> Items { get; set; } = new List<TItem>();
        [Parameter] public IList<TopicLink> Topics { get; set; } = new List<TopicLink>();
        [Parameter] public Func<object?, object?, bool> CanDrop { get; set; } = new Func<object?, object?, bool>((x, y) => true);


        bool ShowInsertBelow = false;
        bool ShowInsertAbove = false;
        bool isExpanded = true;

        private void ToggleExpandCollapse()
        {
            isExpanded = !isExpanded;
        }

        private void HandleDragStart(TreeEditItem<TItem> selectedItem)
        {
            Root.Payload = selectedItem;
        }

        private void HandleDragEnter()
        {
            Root.CurrentNode = this;
            if (this == Root.Payload || Root.Payload == null) return;

            if (CanDrop(Root.Payload.Item, Item))
            {
                ShowInsertBelow = true;
            }
            else
            {
                ShowInsertBelow = false;
            }
        }

        private void HandleDragLeave()
        {
            ShowInsertBelow = false;
        }

        private bool IsLoopInChain()
        {
            ITreeEditItem<TItem>? parent = Parent;
            while (parent != null)
            {
                if (parent == Root?.Payload)
                {
                    return true;
                }
                parent = parent?.Parent;
            }
            return false;
        }

        private async Task HandleDrop()
        {
            ShowInsertBelow = false;

            if (Root?.Payload == null || this == Root.Payload) return;
            if (IsLoopInChain()) return;

            if (CanDrop(Root.Payload.Item, Item))
            {
                Root.Payload.Parent?.Items.Remove(Root.Payload.Item);
                Root.Payload.Parent = this;
                Items.Add(Root.Payload.Item);
            }
            await Root.UpdateJobAsync();

        }

        #region Drag Before
        private void HandleDragEnterBefore()
        {
            Root.CurrentNode = this;
            if (this == Root.Payload || Root.Payload == null) return;

            if (CanDrop(Root.Payload.Item, Item))
            {
                ShowInsertAbove = true;
            }
            else
            {
                ShowInsertAbove = false;
            }
        }

        private void HandleDragLeaveBefore()
        {
            ShowInsertAbove = false;
        }

        private async Task HandleDropBefore()
        {
            Console.WriteLine("Drop Before");
            ShowInsertAbove = false;

            if (Root?.Payload == null || this == Root.Payload) return;
            if (IsLoopInChain()) return;

            if (CanDrop(Root.Payload.Item, Item))
            {
                Root.Payload.Parent?.Items.Remove(Root.Payload.Item);
                Root.Payload.Parent = this;
                var index = Parent.Items.IndexOf(Item);
                Parent.Items.Insert(index, Root.Payload.Item);
            }
            await Root.UpdateJobAsync();

        }
        #endregion
    }
}
