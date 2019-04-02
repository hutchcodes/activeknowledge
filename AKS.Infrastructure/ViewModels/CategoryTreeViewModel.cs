using AKS.AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Infrastructure.ViewModels
{
    [Obsolete]
    public class CategoryTreeViewModel
    {
        public CategoryTreeViewModel(IEnumerable<Category> categories)
        {
            foreach (var cat in categories)
            {
                Categories.Add(new CategoryTreeViewModel(cat));
            }
        }
        public CategoryTreeViewModel(Category category)
        {
            Name = category.Name;
            foreach(var topic in category.Topics)
            {
                Topics.Add(new TopicLinkViewModel(topic.Topic));
            }

            foreach (var cat in category.Categories)
            {
                Categories.Add(new CategoryTreeViewModel(cat));
            }
        }
        public string Name { get; set; }
        public List<CategoryTreeViewModel> Categories { get;} = new List<CategoryTreeViewModel>();
        public List<TopicLinkViewModel> Topics { get; } = new List<TopicLinkViewModel>();
    }
}
