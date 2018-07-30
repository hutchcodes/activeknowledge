using System;
using System.Collections.Generic;
using System.Text;
using Resurgam.AppCore.Entities;

namespace Resurgam.AppCore.Specifications
{
    public class TagFilterSpecification : BaseSpecification<Tag>
    {
        public TagFilterSpecification(int projectId, int tagId) : base(x => x.ProjectId == projectId && x.Id == tagId)
        {
        }

        public TagFilterSpecification(int projectId, string tagName) : base(x => x.ProjectId == projectId && x.Name == tagName)
        {
        }
    }
}
