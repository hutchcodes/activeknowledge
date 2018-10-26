using System;
using System.Collections.Generic;
using System.Text;
using Resurgam.AppCore.Entities;

namespace Resurgam.AppCore.Specifications
{
    public class TagFilterSpecification : BaseSpecification<Tag>
    {
        public TagFilterSpecification(Guid projectId, Guid tagId) : base(x => x.ProjectId == projectId && x.TagId == tagId)
        {
        }

        public TagFilterSpecification(Guid projectId, string tagName) : base(x => x.ProjectId == projectId && x.Name == tagName)
        {
        }
    }
}
