using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AKS.App.Build.Server.Pages
{
    public class DeepLinkModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public DeepLinkModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
        }

        public async Task foo()
        {
          
        }
    }
}
