using System;
using AKS.Infrastructure;
using AKS.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Ents = AKS.Infrastructure.Entities;
using Mods = AKS.Common.Models;

namespace AKS.Infrastructure.Tests.Mappers
{
    [TestFixture]
    public class ProjectMapperTests
    {
        Ents.Project _entity;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var customerId = Guid.NewGuid();
            _entity = new Ents.Project {
                ProjectId = Guid.NewGuid(),
                CustomerId = customerId,
                Name = "Test Name",
                LogoFileName = "LogoFile",
                Customer = new Ents.Customer { CustomerId = customerId, Name = "Test Customer Name", LogoFileName = "Customer Logo File" }
            };
        }

        [Test]
        public void ShouldMapProjectList()
        {
            var mapper = MapperConfig.GetMapperConfig().CreateMapper();
            var model = mapper.Map<Mods.ProjectList>(_entity);

            Assert.AreEqual(_entity.ProjectId, model.ProjectId);
            Assert.AreEqual(_entity.Name, model.Name);
        }

        [Test]
        public void ShouldMapProjectHeaderNav()
        {
            var mapper = MapperConfig.GetMapperConfig().CreateMapper();
            var model = mapper.Map<Mods.HeaderNavView>(_entity);

            Assert.AreEqual(_entity.ProjectId, model.ProjectId);
            Assert.AreEqual(_entity.Name, model.ProjectName);
            Assert.AreEqual(_entity.LogoFileName, model.ProjectLogo);
            Assert.AreEqual(_entity.CustomerId, model.CustomerId);
            Assert.AreEqual(_entity.Customer.Name, model.CustomerName);
            Assert.AreEqual(_entity.Customer.LogoFileName, model.CustomerLogo);
        }
    }
}
