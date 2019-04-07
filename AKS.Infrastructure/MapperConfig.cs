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
using System.Linq;

namespace AKS.Infrastructure
{
    public static class MapperConfig
    {
        static MapperConfigurationExpression _cfg = new MapperConfigurationExpression();

        public static void ConfigMappers()
        {
            //Configure to handle EF Core Collections based on the EF Core Primary Key
            _cfg.AddCollectionMappers();
            _cfg.UseEntityFrameworkCoreModel<AKSContext>();

            ConfigHeaderModel();
            ConfigProjectModel();
            ConfigTopicModel();
            ConfigTagModel();
            ConfigCategoryTreeModel();


            //Validate that the configuation is valid
            //var config = new MapperConfiguration(cfg);
            //config.AssertConfigurationIsValid();

            Mapper.Initialize(_cfg);

        }

        private static void ConfigHeaderModel()
        {
            _cfg.CreateMap<Ents.Customer, Mods.HeaderNavView>()
                .ForMember(mod => mod.CustomerName, x => x.MapFrom(ent => ent.Name))
                .ForMember(mod => mod.CustomerLogo, x => x.MapFrom(ent => ent.LogoFileName))
                .ForMember(mod => mod.ProjectId, x => x.Ignore())
                .ForMember(mod => mod.ProjectName, x => x.Ignore())
                .ForMember(mod => mod.ProjectLogo, x => x.Ignore())
            ;
        }

        private static void ConfigProjectModel()
        {
            _cfg.CreateMap<Ents.Project, Mods.ProjectList>();
            _cfg.CreateMap<Ents.Project, Mods.HeaderNavView>()
                .ForMember(mod => mod.ProjectName, x => x.MapFrom(ent => ent.Name))
                .ForMember(mod => mod.ProjectLogo, x => x.MapFrom(ent => ent.LogoFileName))
                .ForMember(mod => mod.CustomerName, x => x.MapFrom(ent => ent.Customer.Name))
                .ForMember(mod => mod.CustomerLogo, x => x.MapFrom(ent => ent.Customer.LogoFileName));
        }

        private static void ConfigTopicModel()
        {
            _cfg.CreateMap<Ents.Topic, Mods.TopicLink>();
            _cfg.CreateMap<Ents.Topic, Mods.TopicList>()
                .ForMember(mod => mod.IsSelected, y => y.Ignore());
            _cfg.CreateMap<Ents.Topic, Mods.TopicView>()
                .BeforeMap((ent, mod) => TopicCleaner.CleanTopicContent(ent))
                .ForMember(mod => mod.RelatedTopics, y => y.MapFrom(ent => ent.RelatedToTopics.Select(x => x.ChildTopic)));

            _cfg.CreateMap<Ents.CollectionElement, Mods.CollectionElement>();
        }

        private static void ConfigTagModel()
        {
            _cfg.CreateMap<Ents.Tag, Mods.Tag>()
                .EqualityComparison((ent, mod) => ent.TagId == mod.TagId);
        }

        private static void ConfigCategoryTreeModel()
        {
            _cfg.CreateMap<Ents.Category, Mods.CategoryTreeView>()
                .ForMember(mod => mod.Categories, x => x.MapFrom(ent => ent.Categories))
                .ForMember(mod => mod.Topics, x => x.MapFrom(ent => ent.Topics.Select(y => y.Topic)));
        }
    }
}

