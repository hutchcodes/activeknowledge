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
        readonly static MapperConfigurationExpression _cfg = new MapperConfigurationExpression();

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
            //ConfigTopicEntity();

            Mapper.Initialize(_cfg);
            Mapper.AssertConfigurationIsValid();

        }

        #region ModelMapperConfig

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
                .ForMember(mod => mod.IsSelected, y => y.Ignore())
            ;
            _cfg.CreateMap<Ents.Topic, Mods.TopicView>()
                .BeforeMap((ent, mod) => TopicCleaner.CleanTopicContent(ent))
                .ForMember(mod => mod.CollectionElements, y => y.Ignore())
                .ForMember(mod => mod.RelatedTopics, y => y.MapFrom(ent => ent.RelatedToTopics.Select(x => x.ChildTopic)))
            ;
            _cfg.CreateMap<Ents.Topic, Mods.TopicEdit>()
                .ForMember(mod => mod.RelatedTopics, y => y.MapFrom(ent => ent.RelatedToTopics.Select(x => x.ChildTopic)))
                .ForMember(mod => mod.CollectionElements, y => y.Ignore())
                .ReverseMap()
            ;

            _cfg.CreateMap<Ents.CollectionElement, Mods.CollectionElement>()
                .ReverseMap()
            ;
            _cfg.CreateMap<Ents.CollectionElementTopic, Mods.CollectionElementTopic>()
                .ReverseMap()
                .ForMember(mod=> mod.Topic, y=> y.Ignore());
        }

        private static void ConfigTagModel()
        {
            _cfg.CreateMap<Ents.Tag, Mods.Tag>()
                .ReverseMap();
        }

        private static void ConfigCategoryTreeModel()
        {
            _cfg.CreateMap<Ents.Category, Mods.CategoryTreeView>()
                .ForMember(mod => mod.Categories, x => x.MapFrom(ent => ent.Categories))
                .ForMember(mod => mod.Topics, x => x.MapFrom(ent => ent.Topics.Select(y => y.Topic)));
        }
        #endregion

        #region EntitiyMapperConfig

        private static void ConfigTopicEntity()
        {
            _cfg.CreateMap<Mods.TopicEdit, Ents.Topic>()
                .ForMember(ent => ent.RelatedToTopics, y => y.Ignore())
                .ForMember(ent => ent.CollectionElements, y => y.Ignore())
                .ForMember(ent => ent.DefaultCategoryId, y => y.Ignore())
                .ForMember(ent => ent.FileResourceId, y => y.Ignore())
                .ForMember(ent => ent.ReferencedFragments, y => y.Ignore())
                .ForMember(ent => ent.FragmentReferencedBy, y => y.Ignore())
                .ForMember(ent => ent.ImageResourceId, y => y.Ignore())
                .ForMember(ent => ent.RelatedFromTopics, y => y.Ignore())
                .ConstructUsing((mod, ent) => new Ents.Topic())
            ;

            _cfg.CreateMap<Mods.CollectionElement, Ents.CollectionElement>()
                .ForMember(ent => ent.Topic, y => y.Ignore())
            //.ForMember(ent => ent.ElementTopics, y=> y.Ignore())
            ;

            _cfg.CreateMap<Mods.TopicView, Ents.Topic>()
                .ForMember(ent => ent.ProjectId, y => y.MapFrom(mod => mod.ProjectId))
                .ForMember(ent => ent.TopicId, y => y.MapFrom(mod => mod.TopicId))
                .ForAllOtherMembers(x => x.Ignore());
        }

        #endregion
    }
}

