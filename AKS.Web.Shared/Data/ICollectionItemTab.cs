using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKS.Common.Models;
using Microsoft.AspNetCore.Components;

namespace AKS.App.Core.Data
{
    public interface ICollectionItemTab
    {
        TopicView? CurrentTopic { get; }
        //RenderFragment? ChildContent { get; }
    }
}
