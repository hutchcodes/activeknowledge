using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Resurgam.Web.Admin.Interfaces;
using Resurgam.Web.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Resurgam.Web.Admin.Pages
{
    public abstract class ResurgamPage : PageModel
    {
        protected readonly IHeaderService _headerService;

        public ResurgamPage(IHeaderService headerService)
        {
            _headerService = headerService;
        }
        public HeaderNavViewModel HeaderNav { get; set; }
        public async Task GetHeaderNav(int? customerId, int? projectId)
        {
            if (projectId.HasValue)
            {
                HeaderNav = await _headerService.GetHeaderForProjectAsync(projectId.Value);
            }
            else if (customerId.HasValue)
            {
                HeaderNav = await _headerService.GetHeaderForCustomerAsync(customerId.Value);
            }
        }
    }
}