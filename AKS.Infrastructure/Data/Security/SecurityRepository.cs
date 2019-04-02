using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AKS.AppCore.Security;

namespace AKS.Infrastructure.Data.Security
{
    public class SecurityRepository : ISecurityRepository
    {
        private readonly SecurityContext _dbContext;

        public SecurityRepository(SecurityContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> GetUser(Guid userId)
        {
            var user = await _dbContext.Users.Where(x => x.Id == userId)
                .Include(x => x.Groups)
                .ThenInclude(y => y.ProjectPermissions).FirstOrDefaultAsync();

            return user;
        }

        public async Task SaveUserInfo(User user)
        {
            var dbUser = await _dbContext.Users.FindAsync(user.Id);
            if (dbUser == null)
            {
                dbUser = new User()
                {
                    Id = user.Id,
                };

                _dbContext.Users.Add(dbUser);
            }
            else
            {
                _dbContext.Users.Update(dbUser);
            }

            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.EmailAddress = user.EmailAddress;

            dbUser.Groups.Clear();

            foreach(var g in user.Groups)
            {
                var dbGroup = await _dbContext.Groups.FindAsync(g.Id);
                if (dbGroup != null)
                {
                    dbUser.Groups.Add(dbGroup);
                }
                else
                {
                    dbUser.Groups.Add(g);
                }
            }
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
