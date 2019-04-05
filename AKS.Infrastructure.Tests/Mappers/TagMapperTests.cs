using System;
using AKS.Infrastructure;
using AKS.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace AKS.AppCore.Tests
{
    [TestFixture]
    public class TagMapperTests
    {
        Entities.Tag _entity;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _entity = new Entities.Tag {
                ProjectId = Guid.NewGuid(),
                TagId = Guid.NewGuid(),
                Name = "Test Name",
            };
        }

        [Test]
        public void ShouldMapTag()
        {
            var model = Mapper.Map<Common.Models.Tag>(_entity);

            Assert.AreEqual(_entity.ProjectId, model.ProjectId);
            Assert.AreEqual(_entity.TagId, model.TagId);
            Assert.AreEqual(_entity.Name, model.Name);
        }
    }
}
