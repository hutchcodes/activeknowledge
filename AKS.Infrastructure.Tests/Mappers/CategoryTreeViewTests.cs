using System;
using System.Linq;
using Ents = AKS.Infrastructure.Entities;
using AKS.Infrastructure;
using AKS.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Mods = AKS.Common.Models;
using AutoMapper;

namespace AKS.Infrastructure.Tests.Mappers
{
    [TestFixture]
    public class CategoryTreeViewTests
    {
        Ents.Category _entity;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var projectId = Guid.NewGuid();
            _entity = new Ents.Category
            {
                ProjectId = projectId,
                CategoryId = Guid.NewGuid(),
                Order = 1,
                Name = "Test Name1"
            };
            _entity.Categories.Add(new Ents.Category
            {
                ProjectId = projectId,
                CategoryId = Guid.NewGuid(),
                Order = 1,
                Name = "Test Name2"
            });
            _entity.CategoryTopics.Add(new Ents.CategoryTopic
            {
                ProjectId = projectId,
                Topic = new Ents.Topic
                {
                    ProjectId = projectId,
                    Title = "TopicTitle",
                    Description = "TopicDesc",
                }
            });
        }

        [Test]
        public void ShouldMapCategories()
        {
            var mapper = MapperConfig.GetMapperConfig().CreateMapper();
            var model = mapper.Map<Mods.CategoryTree>(_entity);

            Assert.AreEqual(_entity.CategoryId, model.CategoryId);
            Assert.AreEqual(_entity.Name, model.Name);

            Assert.AreEqual(_entity.Categories.First().CategoryId, model.Categories.First().CategoryId);
            Assert.AreEqual(_entity.Categories.First().Name, model.Categories.First().Name);

            Assert.AreEqual(_entity.CategoryTopics.First().Topic.TopicId, model.Topics.First().TopicId);
            Assert.AreEqual(_entity.CategoryTopics.First().Topic.Title, model.Topics.First().Topic.Title);

        }
    }
}
