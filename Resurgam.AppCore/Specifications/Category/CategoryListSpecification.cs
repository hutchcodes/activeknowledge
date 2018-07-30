using System;
using System.Collections.Generic;
using System.Text;
using Resurgam.AppCore.Entities;

namespace Resurgam.AppCore.Specifications
{
    public class CategoryListSpecification : BaseSpecification<Category>
    {
        public CategoryListSpecification(int projectId) : base(x => x.Id == projectId)
        {
            //this.Includes.Add(x => x.Cat);
        }
    }
}
