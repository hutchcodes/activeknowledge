using AKS.App.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Web.Web.Data
{
    public class AppState : IAppState
    {
        public bool TocIsVisible { get; set; } = true;
    }
}
