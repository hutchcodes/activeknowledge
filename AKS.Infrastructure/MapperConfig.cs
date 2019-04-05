using System;
using System.Collections.Generic;
using System.Text;
using Mods = AKS.Common.Models;
using Ents = AKS.AppCore.Entities;
using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.EquivalencyExpression;
using AKS.Infrastructure.Data;
using AutoMapper.EntityFrameworkCore;

namespace AKS.Infrastructure
{
    public static class MapperConfig
    {
        public static void ConfigMappers()
        {
            var cfg = new MapperConfigurationExpression();

            //Configure to handle EF Core Collections based on the EF Core Primary Key
            cfg.AddCollectionMappers();
            cfg.UseEntityFrameworkCoreModel<AKSContext>();

            cfg.CreateMap<Ents.Project, Mods.ProjectList>();
            cfg.CreateMap<Ents.Project, Mods.HeaderNavView>()
                .ForMember(mod => mod.ProjectName, x => x.MapFrom(ent => ent.Name))
                .ForMember(mod => mod.ProjectLogo, x => x.MapFrom(ent => ent.LogoFileName))
                .ForMember(mod => mod.CustomerName, x => x.MapFrom(ent => ent.Customer.Name))
                .ForMember(mod => mod.CustomerLogo, x => x.MapFrom(ent => ent.Customer.LogoFileName))
            ;

            cfg.CreateMap<Ents.Topic, Mods.TopicLink>();
            cfg.CreateMap<Ents.Topic, Mods.TopicList>()
                .ForMember(mod => mod.IsSelected, y => y.Ignore());
            cfg.CreateMap<Ents.Topic, Mods.TopicView>()
                .ForMember(mod => mod.RelatedTopics, y => y.MapFrom(ent => ent.RelatedToTopics));

            cfg.CreateMap<Ents.CollectionElement, Mods.CollectionElement>();

            cfg.CreateMap<Ents.Tag, Mods.Tag>()
                .EqualityComparison((ent, mod) => ent.TagId == mod.TagId); 

            //Validate that the configuation is valid
            //var config = new MapperConfiguration(cfg);
            //config.AssertConfigurationIsValid();

            Mapper.Initialize(cfg);

        }
    }
}

