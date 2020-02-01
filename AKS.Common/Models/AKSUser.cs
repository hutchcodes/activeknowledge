using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using AKS.Common.Enums;

namespace AKS.Common.Models
{
    public class AKSUser
    {
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
        public string Email { get; set; } = "";

        public List<CustomerPermission> CustomerPermissions { get; set; } = new List<CustomerPermission>();
        public List<ProjectPermission> ProjectPermissions { get; set; } = new List<ProjectPermission>();

        private void BuildFromUser(ClaimsPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                Console.WriteLine("isAuthenticated");
                Console.WriteLine(UserClaimHelper.GetClaimValue(user, UserClaimType.UserId));
                UserId = Guid.Parse(UserClaimHelper.GetClaimValue(user, UserClaimType.UserId));
                UserName = UserClaimHelper.GetClaimValue(user, UserClaimType.UserName);
                FirstName = UserClaimHelper.GetClaimValue(user, UserClaimType.FirstName);
                LastName = UserClaimHelper.GetClaimValue(user, UserClaimType.LastName);
                CustomerId = Guid.Parse(UserClaimHelper.GetClaimValue(user, UserClaimType.CustomerId));
            }
        }
    }
}
