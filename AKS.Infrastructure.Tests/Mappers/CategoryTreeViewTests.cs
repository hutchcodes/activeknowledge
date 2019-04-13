using System;
using System.Linq;
using Ents = AKS.AppCore.Entities;
using AKS.Infrastructure;
using AKS.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Mods = AKS.Common.Models;

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
            _entity.AddReferencedCategory(new Ents.Category
            {
                ProjectId = projectId,
                CategoryId = Guid.NewGuid(),
                Order = 1,
                Name = "Test Name2"
            }, 1);
            _entity.Topics.Add(new Ents.CategoryTopic
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
            var model = Mapper.Map<Mods.CategoryTreeView>(_entity);

            Assert.AreEqual(_entity.CategoryId, model.CategoryId);
            Assert.AreEqual(_entity.Name, model.Name);

            Assert.AreEqual(_entity.Categories.First().CategoryId, model.Categories.First().CategoryId);
            Assert.AreEqual(_entity.Categories.First().Name, model.Categories.First().Name);

            Assert.AreEqual(_entity.Topics.First().Topic.TopicId, model.Topics.First().TopicId);
            Assert.AreEqual(_entity.Topics.First().Topic.Title, model.Topics.First().Title);

        }
    }
}
