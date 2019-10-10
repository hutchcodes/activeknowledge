using AKS.Infrastructure.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AKS.Infrastructure.Security;

namespace AKS.Infrastructure.Data.Security
{
    public sealed class SecurityContextSeed
    {
        public static async Task SeedAsync(SecurityContext securityContext, ILoggerFactory loggerFactory, int retry = 0)
        {
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
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                if (retry < 10)
                {
                    retry++;
                    var log = loggerFactory.CreateLogger<SecurityContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(securityContext, loggerFactory, retry);
                }
            }
#pragma warning restore CA1031 // Do not catch general exception types
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
