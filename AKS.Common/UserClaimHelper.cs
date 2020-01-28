using AKS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AKS.Common
{
    public static class UserClaimHelper
    {
        const string CUSTOMERID_CLAIM = "http://schemas.microsoft.com/identity/claims/identityprovider";
        const string USERID_CLAIM = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        const string USERNAME_CLAIM = "name";
        const string FIRSTNAME_CLAIM = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
        const string LASTNAME_CLAIM = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";

        public static string GetClaimValue(ClaimsPrincipal principal, UserClaimType userClaimType)
        {
            var claimValue = "";
            switch (userClaimType)
            {
                case UserClaimType.CustomerId:
                    var idpUri = new Uri(principal.Claims.FirstOrDefault(x => x.Type == CUSTOMERID_CLAIM).Value);
                    claimValue = idpUri.Segments[1].ToString().Substring(0, 36);
                    break;

                case UserClaimType.UserId:
                    claimValue = principal.Claims.FirstOrDefault(x => x.Type == USERID_CLAIM).Value;
                    break;
                case UserClaimType.UserName:
                    claimValue = principal.Claims.FirstOrDefault(x => x.Type == USERNAME_CLAIM).Value;
                    break;
                case UserClaimType.FirstName:
                    claimValue = principal.Claims.FirstOrDefault(x => x.Type == FIRSTNAME_CLAIM).Value;
                    break;
                case UserClaimType.LastName:
                    claimValue = principal.Claims.FirstOrDefault(x => x.Type == LASTNAME_CLAIM).Value;
                    break;
                default:
                    throw new ApplicationException($"Unhandled UserClaimType: {userClaimType.ToString()}");
            }
            return claimValue;
        }
    }
}
