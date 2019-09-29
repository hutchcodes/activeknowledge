using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.App.Core.Data
{
    public interface IAppState
    {
        bool TocIsVisible { get; set; }
    }
}
