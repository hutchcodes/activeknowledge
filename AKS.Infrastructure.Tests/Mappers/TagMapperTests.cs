using System;
using Mods = AKS.Common.Models;
using AKS.Infrastructure;
using AKS.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using Ents = AKS.AppCore.Entities;
using System.Linq;

namespace AKS.Infrastructure.Tests.Mappers
{
    [TestFixture]
    public class TagMapperTests
    {
        readonly Ents.TopicTag _entity;
        readonly Mods.Tag _model;

        public TagMapperTests()
        //[OneTimeSetUp]
        //public void OneTimeSetup()
        {
            var projectId = Guid.NewGuid();
            var topicId = Guid.NewGuid();
            var tagId = Guid.NewGuid();

            _entity = new Ents.TopicTag
            {
                ProjectId = projectId,
                TagId = topicId,
                TopicId = tagId,
                Tag = new Ents.Tag
                {
                    ProjectId = projectId,
                    TagId = tagId,
                    Name = "Test Name",
                }
            };

            _model = new Mods.Tag
            {
                ProjectId = Guid.NewGuid(),
                TagId = Guid.NewGuid(),
                Name = "Test Name"
            };
        }

        [Test]
        public void ShouldMapEntityTagToModel()
        {
            var model = Mapper.Map<Mods.Tag>(_entity);

            Assert.AreEqual(_entity.ProjectId, model.ProjectId);
            Assert.AreEqual(_entity.TagId, model.TagId);
            Assert.AreEqual(_entity.Tag?.Name, model.Name);
        }

        [Test]
        public void ShouldMapModelTagToEntity()
        {
            var entity = Mapper.Map<Ents.TopicTag>(_model);

            Assert.AreEqual(_model.ProjectId, entity.ProjectId);
            Assert.AreEqual(_model.TopicId, entity.TopicId);
            Assert.AreEqual(_model.TagId, entity.TagId);
            Assert.AreEqual(_model.Name, entity.Tag?.Name);
        }
    }
}
