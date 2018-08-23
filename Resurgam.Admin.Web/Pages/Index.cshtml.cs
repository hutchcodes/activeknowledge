using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Resurgam.AppCore.Entities;
using Resurgam.AppCore.Interfaces;
using Resurgam.Infrastructure.Interfaces;
using Resurgam.Infrastructure.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Resurgam.Admin.Web.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        public object HeaderNav { get; set; } = new HeaderNavViewModel();
        public void OnGet()
        {
        }
    }
}
