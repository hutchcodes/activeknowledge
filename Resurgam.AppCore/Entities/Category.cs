using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Resurgam.AppCore.Entities
{
    [Table("Category")]
    public class Category : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public Guid ProjectId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }
        public int Order { get; set; }
        public Guid? ParentCategoryId { get; set; }

        private Func<CategoryTopic, bool> GetCategoryTopicByTopicIdSpec(Guid topicId)
        {
            return (x => x.TopicId == topicId);
        }

        private readonly List<CategoryTopic> _topics = new List<CategoryTopic>();
        public IReadOnlyCollection<CategoryTopic> Topics => _topics.AsReadOnly();
        public void AddReferencedTopic(Guid topicId, int order)
        {
            if (!_topics.Any(GetCategoryTopicByTopicIdSpec(topicId)))
            {
                _topics.Add(new CategoryTopic()
                {
                    ProjectId = ProjectId,
                    ParentCategoryId = CategoryId,
                    TopicId = topicId,
                    Order = order,
                });
                return;
            }
        }
        public void RemoveReferencedTopic(Guid topicId)
        {
            var referencedTopic = _topics.FirstOrDefault(GetCategoryTopicByTopicIdSpec(topicId));
            if (referencedTopic != null)
            {
                _topics.Remove(referencedTopic);
            }
        }

        private readonly List<Category> _categories = new List<Category>();
        public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();
        public void AddReferencedCategory(Category category, int order)
        {
            if (!_categories.Any(x => x.CategoryId == category.CategoryId))
            {
                category.ParentCategoryId = CategoryId;
                category.Order = order;
                _categories.Add(category);
                return;
            }
        }
        public void AddReferencedCategory(Guid categoryId, int order)
        {
            if (!_categories.Any(x => x.CategoryId == categoryId))
            {
                _categories.Add(new Category()
                {
                    ProjectId = ProjectId,
                    ParentCategoryId = CategoryId,
                    CategoryId = categoryId,
                    Order = order,                    
                });
                return;
            }
        }
        public void RemoveReferencedCategory(Guid categoryId)
        {
            var referencedCategory = _categories.FirstOrDefault(x=> x.CategoryId == categoryId);
            if (referencedCategory != null)
            {
                _categories.Remove(referencedCategory);
            }
        }
    }
}
