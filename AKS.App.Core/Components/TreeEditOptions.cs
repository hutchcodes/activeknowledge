using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AKS.App.Core.Components
{
    public class TreeEditOptions<TItem>
    {
        public TreeEditItemRoot<TItem> Root { get; set; } = null!;
        public RenderFragment<TItem>? DisplayTemplate { get; set; }
        public RenderFragment<TItem>? ChildTemplate { get; set; }
        public RenderFragment<CategoryTopicList>? TopicTemplate { get; set; }
        public Func<TItem, TItem, Task>? RemoveItemAction { get; set; }
    }
}
