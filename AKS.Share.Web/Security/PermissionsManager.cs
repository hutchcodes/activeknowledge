using AKS.AppCore.Security;
using AKS.Infrastructure.Data.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AKS.Share.Web.Security
{
    public class PermissionsManager
    {
        const string ObjectIdentifier = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        const string GivenName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
        const string Surname = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";
        const string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

        const string Groups = "groups";

        private readonly ISecurityRepository _securityRepo;
        public PermissionsManager(ISecurityRepository securityRepo)
        {
            _securityRepo = securityRepo;
        }

        public async Task<List<ProjectGroupPermissions>> GetPermissionsForUser(Guid userId)
        {
            var user = await _securityRepo.GetUser(userId);

            if(user == null)
            {
                return new List<ProjectGroupPermissions>();
            }

            return user.Groups.SelectMany(x => x.ProjectPermissions).ToList();
        }

        internal async Task<bool> CanUserViewProject(ClaimsPrincipal claimsPrincipal, Guid projectId)
        {
            var userId = Guid.Parse(claimsPrincipal.Claims.First(x => x.Type.Equals(ObjectIdentifier)).Value);

            var userPermissions = await GetPermissionsForUser(userId);

            return userPermissions.Any(x => x.ProjectId == projectId && x.CanView);
        }

        public async Task UpdateUser(ClaimsPrincipal principal)
        {
            var user = new User()
            {
                Id = Guid.Parse(principal.Claims.FirstOrDefault(x => x.Type == ObjectIdentifier).Value),
                FirstName = principal.Claims.FirstOrDefault(x => x.Type == GivenName)?.Value,
                LastName = principal.Claims.FirstOrDefault(x => x.Type == Surname)?.Value,
                EmailAddress = principal.Claims.FirstOrDefault(x => x.Type == Email)?.Value,
            };

            foreach(var claim in principal.Claims.Where(x=> x.Type == Groups))
            {
                user.Groups.Add(new Group() { Id = Guid.Parse(claim.Value) });
            }

            await _securityRepo.SaveUserInfo(user);
        }
    }
}
