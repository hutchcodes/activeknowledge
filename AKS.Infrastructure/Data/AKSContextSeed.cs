using AKS.AppCore.Entities;
using AKS.AppCore.Lookups;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AKS.Infrastructure.Data
{
    public class AKSContextSeed
    {
        private static readonly Guid _customerId1 = new Guid(123, 0, 0, new byte[8]);
        private static readonly Guid _projectId1 = new Guid(1234, 0, 0, new byte[8]);
        public static async Task SeedAsync(AKSContext aksContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            var retryForAvailability = retry.Value;
            try
            {
                // TODO: Only run this if using a real database
                // context.Database.Migrate();

                if (!aksContext.Customers.Any())
                {
                    aksContext.Customers.AddRange(GetPreconfiguredCustomers());
                    await aksContext.SaveChangesAsync();
                }

                if (!aksContext.Projects.Any())
                {
                    aksContext.Projects.AddRange(GetPreconfiguredProjects());
                    await aksContext.SaveChangesAsync();
                }


                if (!aksContext.Tags.Any())
                {
                    aksContext.Tags.AddRange(GetPreconfiguredTags());
                    await aksContext.SaveChangesAsync();
                }

                if (!aksContext.Topics.Any())
                {
                    var topics = GetPreconfiguredTopics();
                    aksContext.AddRange(topics);
                    await aksContext.SaveChangesAsync();
                }

                if (!aksContext.Categories.Any())
                {
                    var cats = GetPreconfiguredCategories();
                    aksContext.Categories.AddRange(cats);
                    await aksContext.SaveChangesAsync();
                }
            }
            catch 
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    //var log = loggerFactory.CreateLogger<AKSContextSeed>();
                    //log.LogError(ex.Message);
                    await SeedAsync(aksContext, loggerFactory, retryForAvailability);
                }
            }
        }

        static IEnumerable<Customer> GetPreconfiguredCustomers()
        {
            return new List<Customer>()
            {
                new Customer() { CustomerId = _customerId1, Name = "Customer1", LogoFileName = "MaineFlag.jpg" },
            };
        }

        static IEnumerable<Project> GetPreconfiguredProjects()
        {
            return new List<Project>()
            {
                new Project() { ProjectId=_projectId1, Name = "Project1", CustomerId = _customerId1, LogoFileName = "portland.jpg" },
            };
        }

        static IEnumerable<Category> GetPreconfiguredCategories()
        {
            var cat1 = new Category() { CategoryId = new Guid(1, 0, 0, new byte[8]), Name = "Category1", ProjectId = _projectId1, Order = 1 };
            var cat2 = new Category() { CategoryId = new Guid(2, 0, 0, new byte[8]), Name = "Category2", ProjectId = _projectId1, Order = 2 };
            var cat3 = new Category() { CategoryId = new Guid(3, 0, 0, new byte[8]), Name = "Category3", ProjectId = _projectId1, Order = 3 };
            var cat1Sub1 = new Category() { CategoryId = new Guid(4, 0, 0, new byte[8]), Name = "Category1Sub1", ProjectId = _projectId1, Order = 1 };
            var cat1Sub1Sub2 = new Category() { CategoryId = new Guid(5, 0, 0, new byte[8]), Name = "Category1Sub1Sub2", ProjectId = _projectId1, Order = 1 };

            cat1.AddReferencedCategory(cat1Sub1, 1);
            cat1Sub1.AddReferencedCategory(cat1Sub1Sub2, 1);
            cat1.AddReferencedTopic(new Guid(111, 0, 0, new byte[8]), 1);
            cat1Sub1.AddReferencedTopic(new Guid(222, 0, 0, new byte[8]), 1);
            cat1Sub1Sub2.AddReferencedTopic(new Guid(444, 0, 0, new byte[8]), 1);

            return new List<Category>()
            {
                cat1,
                cat2,
                cat3,
            };
        }

        static IEnumerable<Tag> GetPreconfiguredTags()
        {
            return new List<Tag>()
            {
                new Tag() { TagId=Guid.NewGuid(), Name = "Tag1", ProjectId = _projectId1 },
                new Tag() { TagId=Guid.NewGuid(), Name = "Tag2", ProjectId = _projectId1 },
                new Tag() { TagId=Guid.NewGuid(), Name = "Tag3", ProjectId = _projectId1 },
            };
        }

        static IEnumerable<Topic> GetPreconfiguredTopics()
        {
            var content1 = new Topic() { TopicId = new Guid(111, 0, 0, new byte[8]), Title = "Content Topic 1", ProjectId = _projectId1, Content = "<h2>Some Header Content</h2><p>Some body Content1</p><ul><li>A list item</li><li>Another list item</li></ul><p><img src='/api/ContentImage/{{projectId}}/111/789/puppy.jpg' /><p><img src='/api/ProjectImage/{{projectId}}/987/kitty.jpg' />", Description = "Content Topic1 Desc", DefaultCategoryId = new Guid(1, 0, 0, new byte[8]), TopicTypeId = TopicTypes.ContentTopic.Id };
            var content2 = new Topic() { TopicId = new Guid(222, 0, 0, new byte[8]), Title = "Content Topic 2", ProjectId = _projectId1, Content = "Some Content2 <p><fragment contenteditable='false' topicId='0000014d-0000-0000-0000-000000000000' style='border: blue 1px solid;padding-right: 2px;padding-left: 2px;margin: 1px;'></fragment>", Description = "Content Topic2 Desc", DefaultCategoryId = new Guid(2, 0, 0, new byte[8]), TopicTypeId = TopicTypes.ContentTopic.Id };
            var fragment1 = new Topic() { TopicId = new Guid(333, 0, 0, new byte[8]), Title = "Fragment", ProjectId = _projectId1, Content = "This part is fragment content", Description = "Fragment Desc", DefaultCategoryId = new Guid(2, 0, 0, new byte[8]), TopicTypeId = TopicTypes.FragmentTopic.Id };
            var document1 = new Topic() { TopicId = new Guid(444, 0, 0, new byte[8]), Title = "Document Topic", ProjectId = _projectId1, Description = "Document Desc", DefaultCategoryId = new Guid(3, 0, 0, new byte[8]), TopicTypeId = TopicTypes.DocumentTopic.Id, DocumentName = "Wireframes.pptx" };
            var collection1 = new Topic() { TopicId = new Guid(555, 0, 0, new byte[8]), Title = "Collection Topic", ProjectId = _projectId1, Description = "Collection Desc", DefaultCategoryId = new Guid(3, 0, 0, new byte[8]), TopicTypeId = TopicTypes.CollectionTopic.Id };
            var content3 = new Topic() { TopicId = new Guid(666, 0, 0, new byte[8]), Title = "Video Topic", ProjectId = _projectId1, Content = "<div id='kaltura_player_1533934464' style='width: 896px; height: 534px;' itemprop='video' itemscope itemtype='http://schema.org/VideoObject'><span itemprop='name' content='PTRBOA002_ATE_SAbrams_Q01_Final.mp4'></span><span itemprop='description' content='null'></span><span itemprop='duration' content='39'></span><span itemprop='thumbnailUrl' content='http://cdnsecakmi.kaltura.com/p/1284141/sp/128414100/thumbnail/entry_id/1_obxueqhl/version/100012'></span><span itemprop='uploadDate' content='2018-08-10T19:45:14.000Z'></span><span itemprop='width' content='896'></span><span itemprop='height' content='534'></span></div><script src='https://cdnapisec.kaltura.com/p/1284141/sp/128414100/embedIframeJs/uiconf_id/29136211/partner_id/1284141?autoembed=true&entry_id=1_obxueqhl&playerId=kaltura_player_1533934464&cache_st=1533934464&width=896&height=534&flashvars[streamerType]=auto'></script>", Description = "Video Embed from Katura", DefaultCategoryId = new Guid(2, 0, 0, new byte[8]), TopicTypeId = TopicTypes.ContentTopic.Id };

            content2.ReferencedFragments.Add(new ReferencedFragment() { ProjectId = content2.ProjectId, ParentTopicId = content2.TopicId, ChildTopicId = fragment1.TopicId });

            content2.Tags.Add(new Tag { TagId = Guid.NewGuid(), Name = "Tag1" });
            content2.Tags.Add(new Tag { TagId = Guid.NewGuid(), Name = "Tag2" });

            content2.RelatedToTopics.Add(new RelatedTopic { ProjectId = _projectId1, ParentTopicId = content2.TopicId, ChildTopicId = content1.TopicId });
            content2.RelatedToTopics.Add(new RelatedTopic { ProjectId = _projectId1, ParentTopicId = content2.TopicId, ChildTopicId = document1.TopicId });

            collection1.Tags.Add(new Tag { TagId = Guid.NewGuid(), Name = "Tag1" });
            collection1.Tags.Add(new Tag { TagId = Guid.NewGuid(), Name = "Tag2" });



            var collectionElement1 = new CollectionElement { CollectionElementId = new Guid(99, 0, 0, new byte[8]), Name = "Element1", ProjectId = _projectId1, TopicId = collection1.TopicId };//, Topic = collection1 };
            var collectionElement2 = new CollectionElement { CollectionElementId = new Guid(98, 0, 0, new byte[8]), Name = "Element2", ProjectId = _projectId1, TopicId = collection1.TopicId };//, Topic = collection1 };
            collectionElement1.ElementTopics.Add(new CollectionElementTopic { ProjectId = content1.ProjectId, TopicId = content1.TopicId, Order = 1 });
            collectionElement1.ElementTopics.Add(new CollectionElementTopic { ProjectId = document1.ProjectId, TopicId = document1.TopicId, Order =2 });
            collectionElement2.ElementTopics.Add(new CollectionElementTopic { ProjectId = content2.ProjectId, TopicId = content2.TopicId, Order = 1 });

            collection1.CollectionElements.Add(collectionElement1);
            collection1.CollectionElements.Add(collectionElement2);

            collection1.RelatedToTopics.Add(new RelatedTopic { ProjectId = _projectId1, ParentTopicId = content2.TopicId, ChildTopicId = content2.TopicId });


            return new List<Topic>()
            {
                content1,
                content2,
                fragment1,
                document1,
                collection1,
                content3
            };
        }
    }
}
