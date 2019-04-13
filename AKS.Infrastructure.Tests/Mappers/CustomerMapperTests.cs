using System;
using AKS.Infrastructure;
using AKS.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Ents = AKS.AppCore.Entities;
using Mods = AKS.Common.Models;


namespace AKS.Infrastructure.Tests.Mappers
{
    [TestFixture]
    public class CustomerMapperTests
    {
        Ents.Customer _entity;
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
        }

        [Test]
        public void ShouldMapCustomerHeaderNav()
        {
            var model = Mapper.Map<Mods.HeaderNavView>(_entity);

            Assert.AreEqual(_entity.CustomerId, model.CustomerId);
            Assert.AreEqual(_entity.Name, model.CustomerName);
            Assert.AreEqual(_entity.LogoFileName, model.CustomerLogo);
        }
    }
}
