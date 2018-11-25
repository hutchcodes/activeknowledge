using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;

namespace Resurgam.Blazor.App.Interfaces
{
    public interface ITab
    {
        RenderFragment ChildContent { get; }
    }
}
