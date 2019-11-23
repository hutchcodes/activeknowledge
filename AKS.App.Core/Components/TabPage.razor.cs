using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKS.App.Core.Components
{
    public class TabPageBase : ComponentBase
    {
        [CascadingParameter]
        private TabSet? Parent { get; set; }

        [Parameter] public RenderFragment ChildContent { get; set; } = null!;

        [Parameter] public string Text { get; set; } = "";
        [Parameter] public bool ShowDelete { get; set; }
        [Parameter] public Action? Delete { get; set; }

        protected bool IsActive { get { return Parent?.ActivePage == this; } }

        protected override void OnInitialized()
        {
            if (Parent == null)
            {
                throw new ArgumentNullException(nameof(Parent), "TabPage must exist within a TabControl");
            }
            base.OnInitialized();
            Parent.AddPage(this);
        }
    }
}
