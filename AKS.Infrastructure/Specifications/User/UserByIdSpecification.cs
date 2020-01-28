using System;
using System.Collections.Generic;
using System.Text;
using AKS.Infrastructure.Entities;

namespace AKS.Infrastructure.Specifications
{
    public class UserByIdSpecification : BaseSpecification<User>
    {
        public UserByIdSpecification(Guid userId) : base(x => x.UserId == userId)
        {
            AddInclude(x => x.UserGroups);

            AddInclude($"{nameof(Group.UserGroups)}.{nameof(UserGroup.Group)}.{nameof(Group.GroupProjectPermissions)}");

        }
    }
}
