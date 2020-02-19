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
        [CascadingParameter(Name = "TreeEditOptions")] public TreeEditOptions<TItem> Options { get; set; } = null!;
        [CascadingParameter(Name = "Parent")] public ITreeEditItem<TItem> Parent { get; set; } = null!;

        public Guid Id { get; } = Guid.NewGuid();
        [Parameter] public TItem Item { get; set; }
        [Parameter] public IList<TItem> Items { get; set; } = new List<TItem>();
        [Parameter] public IList<CategoryTopicList> Topics { get; set; } = new List<CategoryTopicList>();
        [Parameter] public Func<object?, object?, bool> CanDrop { get; set; } = new Func<object?, object?, bool>((x, y) => true);


        bool ShowInsertBelow = false;
        bool ShowInsertAbove = false;
        bool isExpanded = true;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Console.WriteLine(Options.DisplayTemplate);
        }

        private void ToggleExpandCollapse()
        {
            isExpanded = !isExpanded;
        }

        private async Task RemoveItem(TItem item)
        {
            if (Options.RemoveItemAction != null)
            {
                await Options.RemoveItemAction.Invoke(Parent.Item, item);
            }
        }

        private void HandleDragStart(TreeEditItem<TItem> selectedItem)
        {
            Options.Root.Payload = selectedItem;
        }

        private void HandleDragEnter()
        {
            Options.Root.CurrentNode = this;
            if (this == Options.Root.Payload || Options.Root.Payload == null) return;

            if (CanDrop(Options.Root.Payload.Item, Item))
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
                if (parent == Options.Root?.Payload)
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

            if (Options.Root?.Payload == null || this == Options.Root.Payload) return;
            if (IsLoopInChain()) return;

            if (CanDrop(Options.Root.Payload.Item, Item))
            {
                Options.Root.Payload.Parent?.Items.Remove(Options.Root.Payload.Item);
                Options.Root.Payload.Parent = this;
                Items.Add(Options.Root.Payload.Item);
            }
            await Options.Root.UpdateJobAsync();

        }

        #region Drag Before
        private void HandleDragEnterBefore()
        {
            Options.Root.CurrentNode = this;
            if (this == Options.Root.Payload || Options.Root.Payload == null) return;

            if (CanDrop(Options.Root.Payload.Item, Item))
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

            if (Options.Root?.Payload == null || this == Options.Root.Payload) return;
            if (IsLoopInChain()) return;

            if (CanDrop(Options.Root.Payload.Item, Item))
            {
                Options.Root.Payload.Parent?.Items.Remove(Options.Root.Payload.Item);
                Options.Root.Payload.Parent = this;
                var index = Parent.Items.IndexOf(Item);
                Parent.Items.Insert(index, Options.Root.Payload.Item);
            }
            await Options.Root.UpdateJobAsync();

        }
        #endregion
    }
}
