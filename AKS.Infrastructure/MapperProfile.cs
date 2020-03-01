using System;
using System.Collections.Generic;
using System.Text;
using Mods = AKS.Common.Models;
using Ents = AKS.Infrastructure.Entities;
using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.EquivalencyExpression;
using AKS.Infrastructure.Data;
using AutoMapper.EntityFrameworkCore;
using System.Linq;

namespace AKS.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            ConfigHeaderModel();
            ConfigCustomerModel();
            ConfigProjectModel();
            ConfigTopicModel();
            ConfigTagModel();
            ConfigCategoryTreeModel();
            CollectionElementConfig();
            ConfigTopicEntity();
            ConfigFragments();

            ConfigPermissions();
        }

        #region ModelMapperConfig

        private void ConfigHeaderModel()
        {
            CreateMap<Ents.Customer, Mods.HeaderNavView>()
                .ForMember(mod => mod.CustomerName, x => x.MapFrom(ent => ent.Name))
                .ForMember(mod => mod.CustomerLogo, x => x.MapFrom(ent => ent.LogoFileName))
                .ForMember(mod => mod.ProjectId, x => x.Ignore())
                .ForMember(mod => mod.ProjectName, x => x.Ignore())
                .ForMember(mod => mod.ProjectLogo, x => x.Ignore())
            ;
        }

        private void ConfigCustomerModel()
        {
            CreateMap<Ents.Customer, Mods.CustomerEdit>();
            CreateMap<Mods.CustomerEdit, Ents.Customer>()
                .ForMember(ent => ent.Projects, x => x.Ignore())
                .ForMember(ent => ent.CustomCssId, x => x.Ignore())
                .ForMember(ent => ent.Groups, x => x.Ignore())
                .ForMember(ent => ent.Users, x => x.Ignore())
            ;
        }

        private void ConfigProjectModel()
        {
            CreateMap<Ents.Project, Mods.ProjectList>()
                .ForMember(mod => mod.TopicCount, x => x.Ignore())
                ;
            CreateMap<Ents.Project, Mods.HeaderNavView>()
                .ForMember(mod => mod.ProjectName, x => x.MapFrom(ent => ent.Name))
                .ForMember(mod => mod.ProjectLogo, x => x.MapFrom(ent => ent.LogoFileName))
                .ForMember(mod => mod.CustomerName, x => x.MapFrom(ent => ent.Customer!.Name))
                .ForMember(mod => mod.CustomerLogo, x => x.MapFrom(ent => ent.Customer!.LogoFileName));

            CreateMap<Ents.Project, Mods.ProjectEdit>();
            CreateMap<Mods.ProjectEdit, Ents.Project>()
                .ForMember(ent => ent.Customer, x => x.Ignore())
                .ForMember(ent => ent.CustomerId, x => x.Ignore())
                ;
        }

        private void ConfigTopicModel()
        {
            CreateMap<Ents.Topic, Mods.TopicLink>();
            CreateMap<Ents.Topic, Mods.TopicList>()
                .ForMember(mod => mod.IsSelected, y => y.Ignore())
            ;
            CreateMap<Ents.Topic, Mods.TopicView>()
                .BeforeMap((ent, mod) => TopicCleaner.CompileTopicForView(ent))
                .ForMember(mod => mod.CollectionElements, y => y.MapFrom(ent => ent.CollectionElements))
                .ForMember(mod => mod.RelatedTopics, y => y.MapFrom(ent => ent.RelatedToTopics.Select(x => x.ChildTopic)))
                .ForMember(mod => mod.Tags, y => y.MapFrom(ent => ent.TopicTags));
            ;
            CreateMap<Ents.Topic, Mods.TopicEdit>()
                .BeforeMap((ent, mod) => TopicCleaner.DetokenizeTopicContent(ent))
                .ForMember(mod => mod.RelatedTopics, y => y.MapFrom(ent => ent.RelatedToTopics.Select(x => x.ChildTopic)))
                .ForMember(mod => mod.CollectionElements, y => y.MapFrom(ent => ent.CollectionElements))
                .ForMember(mod => mod.Tags, y => y.MapFrom(ent => ent.TopicTags))
                .ForMember(mod => mod.FragmentsUsed, y => y.MapFrom(ent => ent.TopicFragmentChildren))
            ;

            //_CreateMap<Mods.TopicEdit, Ents.Topic>()
            //    //.ForMember(ent => ent.RelatedToTopics.First()., y => y.MapFrom(mod => mod.RelatedTopics))
            //    .ForMember(ent => ent.CollectionElements, y => y.MapFrom(mod => mod.CollectionElements))
            //    .ForMember(ent => ent.TopicTags, y => y.MapFrom(mod => mod.Tags))
            //    .AfterMap((mod, ent) => TopicCleaner.TokenizeTopicContent(ent));
            //;
        }

        private void CollectionElementConfig()
        {
            CreateMap<Ents.CollectionElement, Mods.CollectionElementView>()
                .ForMember(mod => mod.ElementTopics, y => y.MapFrom(ent => ent.CollectionElementTopics))
            ;
            CreateMap<Ents.CollectionElementTopic, Mods.CollectionElementTopicView>()
                .ForMember(mod => mod.Topic, y => y.MapFrom(ent => ent.Topic))
            ;

            CreateMap<Ents.CollectionElement, Mods.CollectionElementEdit>()
                .ForMember(mod => mod.ElementTopics, y => y.MapFrom(ent => ent.CollectionElementTopics))
                .ReverseMap()
            ;
            CreateMap<Ents.CollectionElementTopic, Mods.CollectionElementTopicList>()
                .ForMember(mod => mod.Topic, y => y.MapFrom(ent => ent.Topic))
            ;

            CreateMap<Mods.CollectionElementTopicList, Ents.CollectionElementTopic>()
                .ForMember(ent => ent.ProjectId, y => y.MapFrom(mod => mod.ProjectId))
                .ForMember(ent => ent.TopicId, y => y.MapFrom(mod => mod.TopicId))
                .ForMember(ent => ent.Order, y => y.MapFrom(mod => mod.Order))
                .ForMember(ent => ent.Topic, y => y.Ignore())
                .ForMember(ent => ent.CollectionElement, y => y.Ignore())
            ;
        }

        private void ConfigTagModel()
        {
            CreateMap<Ents.TopicTag, Mods.Tag>()
                .ForMember(mod => mod.Name, y => y.MapFrom(ent => ent.Tag!.Name))
                ;
            CreateMap<Mods.Tag, Ents.TopicTag>()
                .ForMember(ent => ent.Tag, y => y.MapFrom(mod => mod))
                .ForMember(ent => ent.Topic, y => y.Ignore())
            ;
            CreateMap<Mods.Tag, Ents.Tag>()
                .ForMember(ent => ent.TopicTags, y => y.Ignore())
            ;
        }

        private void ConfigCategoryTreeModel()
        {
            CreateMap<Ents.Category, Mods.CategoryTree>()
                .ForMember(dest => dest.Topics, x=> x.MapFrom(src => src.CategoryTopics))
                ;

            CreateMap<Mods.CategoryTree, Ents.Category>()
               .ForMember(dest => dest.ParentCategory, x => x.Ignore())
               .ForMember(dest => dest.CategoryTopics, x => x.Ignore())
               .ForMember(dest => dest.Categories, x=> x.Ignore())
            ;

            CreateMap<Mods.CategoryTopicList, Ents.CategoryTopic>()
                .ForMember(ent => ent.Category, x => x.Ignore())
                .ForMember(ent => ent.Topic, x => x.Ignore())
                ;

            CreateMap<Ents.CategoryTopic, Mods.CategoryTopicList>()
               .ForMember(dest => dest.Topic, x => x.MapFrom(src => src.Topic))
            ;
        }
        #endregion

        #region EntitiyMapperConfig

        private void ConfigTopicEntity()
        {
            CreateMap<Mods.TopicEdit, Ents.Topic>()
                .BeforeMap((mod, ent) => TopicCleaner.TokenizeTopicContent(mod))
                .ForMember(ent => ent.RelatedToTopics, y => y.MapFrom(mod => mod.RelatedTopics))
                .ForMember(ent => ent.CollectionElements, y => y.MapFrom(mod => mod.CollectionElements))
                .ForMember(ent => ent.DefaultCategoryId, y => y.Ignore())
                .ForMember(ent => ent.FileResourceId, y => y.Ignore())
                .ForMember(ent => ent.TopicFragmentChildren, y => y.MapFrom(mod => mod.FragmentsUsed))
                .ForMember(ent => ent.TopicFragmentsParents, y => y.Ignore())
                .ForMember(ent => ent.ImageResourceId, y => y.Ignore())
                .ForMember(ent => ent.RelatedFromTopics, y => y.Ignore())
                .ForMember(ent => ent.TopicTags, y => y.MapFrom(mod => mod.Tags))
                .ForMember(ent => ent.CategoryTopics, y => y.Ignore())
                .ForMember(ent => ent.CollectionElementTopics, y => y.Ignore())
                .ConstructUsing((mod, ent) => new Ents.Topic())
            ;

            CreateMap<Mods.TopicLink, Ents.RelatedTopic>()
                .ForMember(ent => ent.ProjectId, y => y.MapFrom(mod => mod.ProjectId))
                .ForMember(ent => ent.ChildTopicId, y => y.MapFrom(mod => mod.TopicId))
                .ForMember(ent => ent.ParentTopicId, y => y.Ignore())
                .ForMember(ent => ent.ChildTopic, y => y.Ignore())
                .ForMember(ent => ent.ParentTopic, y => y.Ignore())
            ;

            CreateMap<Mods.TopicView, Ents.Topic>()
                .ForMember(ent => ent.ProjectId, y => y.MapFrom(mod => mod.ProjectId))
                .ForMember(ent => ent.TopicId, y => y.MapFrom(mod => mod.TopicId))
                .ForAllOtherMembers(x => x.Ignore());
        }

        #endregion

        private void ConfigFragments()
        {
            CreateMap<Mods.TopicFragmentLink, Ents.TopicFragment>()
                .ForMember(ent => ent.ProjectId, y => y.MapFrom(mod => mod.ProjectId))
                .ForMember(ent => ent.ChildTopicId, y => y.MapFrom(mod => mod.TopicId))
                .ForMember(ent => ent.ParentTopicId, y => y.MapFrom(mod => mod.ParentTopicId))
                .ForMember(ent => ent.ChildTopic, y => y.Ignore())
                .ForMember(ent => ent.ParentTopic, y => y.Ignore())
                ;
            CreateMap<Ents.TopicFragment, Mods.TopicFragmentLink>()
                .ForMember(mod => mod.ProjectId, y => y.MapFrom(ent => ent.ProjectId))
                .ForMember(mod => mod.ParentTopicId, y => y.MapFrom(ent => ent.ParentTopicId))
                .ForMember(mod => mod.TopicId, y => y.MapFrom(ent => ent.ChildTopicId))
                .ForMember(mod => mod.Title, y => y.MapFrom(ent => ent.ChildTopic!.Title))
                .ForMember(mod => mod.Description, y => y.MapFrom(ent => ent.ChildTopic!.Description))
            ;
        }

        private void ConfigPermissions()
        {
            CreateMap<Ents.User, Mods.AKSUser>()
                .ForMember(mod => mod.UserId, y => y.MapFrom(ent => ent.UserId))
                .ForMember(mod => mod.CustomerId, y => y.MapFrom(ent => ent.CustomerId))
                .ForMember(mod => mod.UserId, y => y.MapFrom(ent => ent.UserId))
                .ForMember(mod => mod.FirstName, y => y.MapFrom(ent => ent.FirstName))
                .ForMember(mod => mod.LastName, y => y.MapFrom(ent => ent.LastName))
                .ForMember(mod => mod.UserName, y => y.MapFrom(ent => ent.UserName))
                .ForMember(mod => mod.Email, y => y.MapFrom(ent => ent.Email))
                .ForMember(mod => mod.CustomerPermissions, y => y.MapFrom(ent => GetCustomerPermissions(ent.UserGroups)))
                .ForMember(mod => mod.ProjectPermissions, y => y.MapFrom(ent => GetProjectPermissions(ent.UserGroups)))
            ;
        }

        private List<Mods.CustomerPermission> GetCustomerPermissions(ICollection<Ents.UserGroup> userGroups)
        {
            var customerPermissions = new List<Mods.CustomerPermission>();
            foreach (var ug in userGroups)
            {
                if (ug.Group != null)
                {

                    var customerPermission = customerPermissions.FirstOrDefault(x => x.CustomerId == ug.Group.CustomerId);
                    if (customerPermission == null)
                    {
                        customerPermission = new Mods.CustomerPermission();
                        customerPermissions.Add(customerPermission);
                    }
                    customerPermission.CanManageAccess = customerPermission.CanManageAccess || ug.Group.CanManageAccess;
                    customerPermission.CanEditCustomer = customerPermission.CanEditCustomer || ug.Group.CanEditCustomer;
                    customerPermission.CanCreateProject = customerPermission.CanCreateProject || ug.Group.CanCreateProject;
                }

            }
            return customerPermissions;
        }

        private List<Mods.ProjectPermission> GetProjectPermissions(ICollection<Ents.UserGroup> userGroups)
        {
            var projectPermissions = new List<Mods.ProjectPermission>();
            foreach (var ug in userGroups)
            {
                if (ug.Group != null)
                {
                    foreach (var pp in ug.Group.GroupProjectPermissions)
                    {
                        var projectPermission = projectPermissions.FirstOrDefault(x => x.ProjectId == pp.ProjectId);
                        if (projectPermission == null)
                        {
                            projectPermission = new Mods.ProjectPermission();
                            projectPermissions.Add(projectPermission);
                        }
                        projectPermission.CanRead = projectPermission.CanRead || pp.CanRead;
                        projectPermission.CanEdit = projectPermission.CanEdit || pp.CanEdit;
                    }
                }
            }
            return projectPermissions;
        }
    }
}

