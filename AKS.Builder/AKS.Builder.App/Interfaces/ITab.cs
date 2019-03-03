using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace AKS.Builder.App.Interfaces
{
    public interface ITab
    {
        RenderFragment ChildContent { get; }
    }
}
