using AKS.AppCore.Entities;
using AKS.AppCore.Lookups;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AKS.AppCore.Security;

namespace AKS.Infrastructure.Data.Security
{
    public class SecurityContextSeed
    {
        public static async Task SeedAsync(SecurityContext securityContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                // TODO: Only run this if using a real database
                // context.Database.Migrate();

                if (!securityContext.Users.Any())
                {
                    securityContext.Users.AddRange(GetPreconfiguredUsers());
                    await securityContext.SaveChangesAsync();
                }

                if (!securityContext.Groups.Any())
                {
                    securityContext.Groups.AddRange(GetPreconfiguredGroups());
                    await securityContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<SecurityContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(securityContext, loggerFactory, retryForAvailability);
                }
            }
        }

        static IEnumerable<User> GetPreconfiguredUsers()
        {
            return new List<User>();
            //{
            //    new User() { Id = Guid.Parse("ce4c5ef2-a3f7-4d14-a6fa-e0f328e6362e"), FirstName = "Jeremy", LastName = "Hutchinson", EmailAddress = "jrhutch@live.com" },
            //};
        }

        static IEnumerable<Group> GetPreconfiguredGroups()
        {
            return new List<Group>()
            {
                new Group()
                {
                    Id = Guid.Parse("ce4c5ef2-a3f7-4d14-a6fa-e0f328e6362e"),
                    Name = "Project1 Viewer",
                    ProjectPermissions = new List<ProjectGroupPermissions> { new ProjectGroupPermissions { ProjectId = new Guid(1234, 0, 0, new byte[8]), CanView = true}}
                }
            };
        }
    }
}
