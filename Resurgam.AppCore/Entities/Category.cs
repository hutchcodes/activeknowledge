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
        public int ProjectId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }
        public int Order { get; set; }
        public int ParentCategoryId { get; set; }

        private Func<CategoryTopic, bool> GetCategoryTopicByTopicIdSpec(int topicId)
        {
            return (x => x.TopicId == topicId);
        }

        private readonly List<CategoryTopic> _topics = new List<CategoryTopic>();
        public IReadOnlyCollection<CategoryTopic> Topics => _topics.AsReadOnly();
        public void AddReferencedTopic(int topicId, int order)
        {
            if (!_topics.Any(GetCategoryTopicByTopicIdSpec(topicId)))
            {
                _topics.Add(new CategoryTopic()
                {
                    TopicId = topicId,
                    Order = order,
                });
                return;
            }
        }
        public void RemoveReferencedTopic(int topicId)
        {
            var referencedTopic = _topics.FirstOrDefault(GetCategoryTopicByTopicIdSpec(topicId));
            if (referencedTopic != null)
            {
                _topics.Remove(referencedTopic);
            }
        }

        //private readonly List<Category> _categories = new List<Category>();
        //public IReadOnlyCollection<Category> Categories => _topics.AsReadOnly();
        //public void AddReferencedCategory(int categoryId, int order)
        //{
        //    if (!_topics.Any(GetCategoryTopicByTopicIdSpec(categoryId)))
        //    {
        //        _topics.Add(new Category()
        //        {
        //            TopicId = categoryId,
        //            Order = order,
        //        });
        //        return;
        //    }
        //}
        //public void RemoveReferencedCategory(int categoryId)
        //{
        //    var referencedCategory = _categories.FirstOrDefault(GetCategoryTopicByTopicIdSpec(categoryId));
        //    if (referencedCategory != null)
        //    {
        //        _categories.Remove(referencedCategory);
        //    }
        //}
    }
}
