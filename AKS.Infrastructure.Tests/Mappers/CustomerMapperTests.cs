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
    public class CustomerMapperTests
    {
        Ents.Customer _entity;
        Mods.CustomerEdit _model;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var customerId = Guid.NewGuid();
            _entity = new Ents.Customer
            {
                CustomerId = customerId,
                Name = "Test Name",
                LogoFileName = "LogoFile",                
            };

            _model = new Mods.CustomerEdit
            {
                CustomerId = customerId,
                Name = "Test Name",
                LogoFileName = "LogoFile",
            };
        }

        [Test]
        public void ShouldMapCustomerHeaderNav()
        {
            var mapper = MapperConfig.GetMapperConfig().CreateMapper();
            var model = mapper.Map<Mods.HeaderNavView>(_entity);

            Assert.AreEqual(_entity.CustomerId, model.CustomerId);
            Assert.AreEqual(_entity.Name, model.CustomerName);
            Assert.AreEqual(_entity.LogoFileName, model.CustomerLogo);
        }

        [Test]
        public void ShouldMapFromCustomerEdit()
        {
            var mapper = MapperConfig.GetMapperConfig().CreateMapper();
            var entity = mapper.Map<Ents.Customer>(_model);

            Assert.AreEqual(_model.CustomerId, entity.CustomerId);
            Assert.AreEqual(_model.Name, entity.Name);
            Assert.AreEqual(_model.LogoFileName, entity.LogoFileName);
        }

        [Test]
        public void ShouldMapToCustomerEdit()
        {
            var mapper = MapperConfig.GetMapperConfig().CreateMapper();
            var model = mapper.Map<Mods.CustomerEdit>(_entity);

            Assert.AreEqual(_entity.CustomerId, model.CustomerId);
            Assert.AreEqual(_entity.Name, model.Name);
            Assert.AreEqual(_entity.LogoFileName, model.LogoFileName);
        }
    }
}
