using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace AKS.Common.Models
{
   public class AKSUser
    {
        const string COMPANYID_CLAIM = "http://schemas.microsoft.com/identity/claims/identityprovider";
        const string USERID_CLAIM = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        const string USERNAME_CLAIM = "name";
        const string FIRSTNAME_CLAIM = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
        const string LASTNAME_CLAIM = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";

        public AKSUser() { }
        public AKSUser(ClaimsPrincipal user)
        {
            BuildFromUser(user);
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public Guid CustomerId { get; set; }

        private void BuildFromUser(ClaimsPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                foreach (var c in user.Claims)
                {
                    switch (c.Type)
                    {
                        case USERID_CLAIM:
                            Guid.TryParse(c.Value, out Guid userId);
                            UserId = userId;
                            break;
                        case USERNAME_CLAIM:
                            UserName = c.Value;
                            break;
                        case FIRSTNAME_CLAIM:
                            FirstName = c.Value;
                            break;
                        case LASTNAME_CLAIM:
                            LastName = c.Value;
                            break;
                        case COMPANYID_CLAIM:
                            Guid.TryParse(c.Value, out Guid customerId);
                            CustomerId = customerId;
                            break;
                    }
                }
            }
        }
    }
}
