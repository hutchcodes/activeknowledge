using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Resurgam.Web.Admin.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Resurgam.Web.Admin.Pages
{
    public class PageWithHeaderModel : ResurgamPage
    {
        public PageWithHeaderModel(IHeaderService headerService) : base(headerService) { }
        public void OnGet()
        {

        }
    }
}