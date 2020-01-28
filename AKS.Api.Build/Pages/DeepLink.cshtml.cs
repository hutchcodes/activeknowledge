using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AKS.Api.Build.Pages
{
    public class DeepLinkModel : PageModel
    {
        private readonly ILogger<DeepLinkModel> _logger;

        public DeepLinkModel(ILogger<DeepLinkModel> logger)
        {
            _logger = logger;
        }
    }
}
