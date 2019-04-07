using System;
using System.Linq;
using AKS.Infrastructure;
using AKS.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace AKS.AppCore.Tests
{
    [TestFixture]
    public class CategoryTreeViewTests
    {
        Entities.Category _entity;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var projectId = Guid.NewGuid();
            _entity = new Entities.Category
            {
                ProjectId = projectId,
                CategoryId = Guid.NewGuid(),
                Order = 1,
                Name = "Test Name1"
            };
            _entity.AddReferencedCategory(new Entities.Category
            {
                ProjectId = projectId,
                CategoryId = Guid.NewGuid(),
                Order = 1,
                Name = "Test Name2"
            }, 1);
            _entity.Topics.Add(new Entities.CategoryTopic
            {
                ProjectId = projectId,
                Topic = new Entities.Topic
                {
                    ProjectId = projectId,
                    Title = "TopicTitle",
                    Description = "TopicDesc",
                }
            });
        }

        [Test]
        public void ShouldMapCollectionElement()
        {
            var model = Mapper.Map<Common.Models.CategoryTreeView>(_entity);

            Assert.AreEqual(_entity.CategoryId, model.CategoryId);
            Assert.AreEqual(_entity.Name, model.Name);

            Assert.AreEqual(_entity.Categories.First().CategoryId, model.Categories.First().CategoryId);
            Assert.AreEqual(_entity.Categories.First().Name, model.Categories.First().Name);

            Assert.AreEqual(_entity.Topics.First().Topic.TopicId, model.Topics.First().TopicId);
            Assert.AreEqual(_entity.Topics.First().Topic.Title, model.Topics.First().Title);

        }
    }
}
