using Resurgam.AppCore.Entities;
using Resurgam.AppCore.Lookups;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Resurgam.Infrastructure.Data
{
    public class ResurgamContextSeed
    {
        public static async Task SeedAsync(ResurgamContext resurgamContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                // TODO: Only run this if using a real database
                // context.Database.Migrate();

                if (!resurgamContext.Customers.Any())
                {
                    resurgamContext.Customers.AddRange(GetPreconfiguredCustomers());
                    await resurgamContext.SaveChangesAsync();
                }

                if (!resurgamContext.Projects.Any())
                {
                    resurgamContext.Projects.AddRange(GetPreconfiguredProjects());
                    await resurgamContext.SaveChangesAsync();
                }


                if (!resurgamContext.Tags.Any())
                {
                    resurgamContext.Tags.AddRange(GetPreconfiguredTags());
                    await resurgamContext.SaveChangesAsync();
                }

                if (!resurgamContext.Topics.Any())
                {
                    var topics = GetPreconfiguredTopics();
                    resurgamContext.AddRange(topics);
                    await resurgamContext.SaveChangesAsync();
                }

                if (!resurgamContext.Categories.Any())
                {
                    var cats = GetPreconfiguredCategories();
                    resurgamContext.Categories.AddRange(cats);
                    await resurgamContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<ResurgamContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(resurgamContext, loggerFactory, retryForAvailability);
                }
            }
        }

        static IEnumerable<Customer> GetPreconfiguredCustomers()
        {
            return new List<Customer>()
            {
                new Customer() { Id = 123, Name = "Customer1", LogoFileName = "MaineFlag.jpg" },
            };
        }

        static IEnumerable<Project> GetPreconfiguredProjects()
        {
            return new List<Project>()
            {
                new Project() { Id=1234, Name = "Project1", CustomerId = 123, LogoFileName = "portland.jpg" },
            };
        }

        static IEnumerable<Category> GetPreconfiguredCategories()
        {
            var cat1 = new Category() { Id = 1, Name = "Category1", ProjectId = 1234, Order = 1 };
            var cat2 = new Category() { Id = 2, Name = "Category2", ProjectId = 1234, Order = 2 };
            var cat3 = new Category() { Id = 3, Name = "Category3", ProjectId = 1234, Order = 3 };
            var cat1Sub1 = new Category() { Id = 4, Name = "Category1Sub1", ProjectId = 1234, Order = 1 };
            var cat1Sub1Sub2 = new Category() { Id = 5, Name = "Category1Sub1Sub2", ProjectId = 1234, Order = 1 };

            cat1.AddReferencedCategory(cat1Sub1, 1);
            cat1Sub1.AddReferencedCategory(cat1Sub1Sub2, 1);
            cat1.AddReferencedTopic(111, 1);
            cat1Sub1.AddReferencedTopic(222, 1);
            cat1Sub1Sub2.AddReferencedTopic(444, 1);

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
                new Tag() { Id=11, Name = "Tag1", ProjectId = 1234},
                new Tag() { Id=12, Name = "Tag2", ProjectId = 1234 },
                new Tag() { Id=13, Name = "Tag3", ProjectId = 1234 },
            };
        }

        static IEnumerable<Topic> GetPreconfiguredTopics()
        {
            var content1 = new Topic() { Id = 111, Name = "Content Topic 1", ProjectId = 1234, TopicContent = "<h2>Some Header Content</h2><p>Some body Content1</p><ul><li>A list item</li><li>Another list item</li></ul><p><img src='/api/ContentImage/{{projectId}}/111/789/puppy.jpg' /><p><img src='/api/ProjectImage/{{projectId}}/987/kitty.jpg' />", Description = "Content Topic1 Desc", DefaultCategoryId = 1, TopicTypeId = TopicTypes.ContentTopic.Id };
            var content2 = new Topic() { Id = 222, Name = "Content Topic 2", ProjectId = 1234, TopicContent = "Some Content2 <p><fragment contenteditable='false' topicId='333' style='border: blue 1px solid;padding-right: 2px;padding-left: 2px;margin: 1px;'></fragment>", Description = "Content Topic2 Desc", DefaultCategoryId = 2, TopicTypeId = TopicTypes.ContentTopic.Id };
            var fragment1 = new Topic() { Id = 333, Name = "Fragment", ProjectId = 1234, TopicContent = "This part is fragment content", Description = "Fragment Desc", DefaultCategoryId = 2, TopicTypeId = TopicTypes.FragmentTopic.Id };
            var document1 = new Topic() { Id = 444, Name = "Document Topic", ProjectId = 1234, Description = "Document Desc", DefaultCategoryId = 3, TopicTypeId = TopicTypes.DocumentTopic.Id, DocumentName = "Invoice_1003_2018-06-27.pdf" };
            var collection1 = new Topic() { Id = 555, Name = "Collection Topic", ProjectId = 1234, Description = "Collection Desc", DefaultCategoryId = 3, TopicTypeId = TopicTypes.CollectionTopic.Id };
            var content3 = new Topic() { Id = 666, Name = "Video Topic", ProjectId = 1234, TopicContent = "<div id='kaltura_player_1533934464' style='width: 896px; height: 534px;' itemprop='video' itemscope itemtype='http://schema.org/VideoObject'><span itemprop='name' content='PTRBOA002_ATE_SAbrams_Q01_Final.mp4'></span><span itemprop='description' content='null'></span><span itemprop='duration' content='39'></span><span itemprop='thumbnailUrl' content='http://cdnsecakmi.kaltura.com/p/1284141/sp/128414100/thumbnail/entry_id/1_obxueqhl/version/100012'></span><span itemprop='uploadDate' content='2018-08-10T19:45:14.000Z'></span><span itemprop='width' content='896'></span><span itemprop='height' content='534'></span></div><script src='https://cdnapisec.kaltura.com/p/1284141/sp/128414100/embedIframeJs/uiconf_id/29136211/partner_id/1284141?autoembed=true&entry_id=1_obxueqhl&playerId=kaltura_player_1533934464&cache_st=1533934464&width=896&height=534&flashvars[streamerType]=auto'></script>", Description = "Video Embed from Katura", DefaultCategoryId = 2, TopicTypeId = TopicTypes.ContentTopic.Id };

            content2.AddReferencedFragments(new ReferencedFragment() { ProjectId = content2.ProjectId, ParentTopicId = 222, ChildTopicId = 333 });

            content2.AddTag(111, "Tag1");
            content2.AddTag(112, "Tag2");

            content2.AddRelatedTopic(new RelatedTopic() { Id = 2, ProjectId = collection1.ProjectId, ChildTopicId = 111, ParentTopicId = 222 });
            content2.AddRelatedTopic(new RelatedTopic() { Id = 3, ProjectId = collection1.ProjectId, ChildTopicId = 444, ParentTopicId = 222 });

            collection1.AddTag(113, "Tag1");
            collection1.AddTag(114, "Tag2");


            var collectionElement1 = new CollectionElement { Id = 99, Name = "Element1", ProjectId = 1234, TopicId = 555 };//, Topic = collection1 };
            var collectionElement2 = new CollectionElement { Id = 98, Name = "Element2", ProjectId = 1234, TopicId = 555 };//, Topic = collection1 };
            collectionElement1.AddTopic(content1);
            collectionElement1.AddTopic(document1);
            collectionElement2.AddTopic(content2);

            collection1.AddCollectionElement(collectionElement1);
            collection1.AddCollectionElement(collectionElement2);

            collection1.AddRelatedTopic(new RelatedTopic() { Id = 1, ProjectId = collection1.ProjectId, ChildTopicId = 444, ParentTopicId = 555 });

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
