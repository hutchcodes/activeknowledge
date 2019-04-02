using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AKS.Share.Web.Pages
{
    public class PageWithHeaderModel : AKSPage
    {
        public PageWithHeaderModel(IHeaderService headerService) : base(headerService) { }
        public void OnGet()
        {

        }
    }
}