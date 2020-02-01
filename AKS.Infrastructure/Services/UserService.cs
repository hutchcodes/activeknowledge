using AKS.Common;
using AKS.Common.Enums;
using AKS.Common.Models;
using AKS.Infrastructure.Entities;
using AKS.Infrastructure.Interfaces;
using AKS.Infrastructure.Specifications;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AKS.Infrastructure.Services
{
    public class UserService : IUserService
    {
        public const string CUSTOMERID_CLAIM = "http://schemas.microsoft.com/identity/claims/identityprovider";
        public const string USERID_CLAIM = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        public const string USERNAME_CLAIM = "name";
        public const string FIRSTNAME_CLAIM = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
        public const string LASTNAME_CLAIM = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";
        //public const string EMAIL_CLAIM = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";


        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<User> _userRepo;
        public UserService(IMapper mapper, ILoggerFactory loggerFactory, IAsyncRepository<User> userRepo)
        {
            _logger = loggerFactory.CreateLogger<UserService>();
            _mapper = mapper;
            _userRepo = userRepo;

            _logger.LogDebug($"New instance of {GetType().Name} was created");
        }
        public async Task<AKSUser> GetAuthenticatedUser(Guid userId)
        {
            var userSpec = new UserByIdSpecification(userId);
            var user = await _userRepo.GetAsync(userSpec);

            if (user == null)
            {
                return new AKSUser() { UserId = userId };
            }

            var aksUser = _mapper.Map<AKSUser>(user);

            return aksUser;
        }

        public async Task UpdateUser(User user)
        {
            var userSpec = new UserByIdSpecification(user.UserId);
            var dbUser = await _userRepo.GetAsync(userSpec);

            dbUser = _mapper.Map(user, dbUser);

            if (dbUser == null)
            {
                dbUser = _mapper.Map<User>(user);
                await _userRepo.AddAsync(dbUser);
            }
            else
            {
                dbUser = _mapper.Map(user, dbUser);
                await _userRepo.UpdateAsync(dbUser);
            }
        }

        public async Task CreateUpdateUser(ClaimsPrincipal principal)
        {
            if (Guid.TryParse(principal.Claims.FirstOrDefault(x => x.Type == USERID_CLAIM).Value, out Guid userId))
            {
                var userSpec = new UserByIdSpecification(userId);


                var dbUser = await _userRepo.GetAsync(userSpec);

                if (dbUser == null)
                {
                    dbUser = new User();
                    dbUser.CustomerId = Guid.Parse(UserClaimHelper.GetClaimValue(principal, UserClaimType.CustomerId));
                    dbUser.UserId = Guid.Parse(UserClaimHelper.GetClaimValue(principal, UserClaimType.UserId));
                    dbUser.UserName = UserClaimHelper.GetClaimValue(principal, UserClaimType.UserName);
                    dbUser.FirstName = UserClaimHelper.GetClaimValue(principal, UserClaimType.FirstName);
                    dbUser.LastName = UserClaimHelper.GetClaimValue(principal, UserClaimType.LastName);

                    await _userRepo.AddAsync(dbUser);
                }
                else
                {
                    dbUser.UserName = UserClaimHelper.GetClaimValue(principal, UserClaimType.UserName);
                    dbUser.FirstName = UserClaimHelper.GetClaimValue(principal, UserClaimType.FirstName);
                    dbUser.LastName = UserClaimHelper.GetClaimValue(principal, UserClaimType.LastName);

                    await _userRepo.UpdateAsync(dbUser);
                }
            }
        }
    }
}
