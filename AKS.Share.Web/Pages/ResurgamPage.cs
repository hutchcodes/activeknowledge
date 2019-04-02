using System;
using System.Threading.Tasks;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Resurgam.Admin.Web.Pages
{
    public abstract class ResurgamPage : PageModel
    {
        protected readonly IHeaderService _headerService;

        public ResurgamPage(IHeaderService headerService)
        {
            _headerService = headerService;
        }
        public HeaderNavView HeaderNav { get; set; }
        public async Task GetHeaderNav(Guid? customerId, Guid? projectId)
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