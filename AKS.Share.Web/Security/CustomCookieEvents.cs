using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Share.Web.Security
{
    public class CustomCookieEvents : CookieAuthenticationEvents
    {
        public override async Task SignedIn(CookieSignedInContext context)
        {
            var permissionsManager = context.HttpContext.RequestServices.GetService(typeof(PermissionsManager)) as PermissionsManager;
            await permissionsManager.UpdateUser(context.Principal);
        }        
    }
}
