using AKS.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AKS.Share.Web.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        public object HeaderNav { get; set; } = new HeaderNavView();
        public void OnGet()
        {
        }
    }
}
