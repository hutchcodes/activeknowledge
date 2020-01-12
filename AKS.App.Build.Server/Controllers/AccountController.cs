using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AKS.App.Build.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /*
         *  Called when requesting to sign up or sign in
         */
        public void SignUpSignIn(string redirectUrl)
        {
            redirectUrl = redirectUrl ?? "/";

            var host = HttpContext.Request.Host;
            

            // Use the default policy to process the sign up / sign in flow
            //HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = redirectUrl });
            return;
        }
    }
}