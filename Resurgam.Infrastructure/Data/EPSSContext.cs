using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Resurgam.AppCore.Entities;
using Resurgam.AppCore.Entities.Interfaces;
using System;

namespace Resurgam.Infrastructure.Data
{
    public class ResurgamContext : DbContext
    {
        private static readonly LoggerFactory _myConsoleLoggerFactory =
            new LoggerFactory(new[] {
                        new ConsoleLoggerProvider((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information, true)
            });
        public ResurgamContext(DbContextOptions<ResurgamContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_myConsoleLoggerFactory);
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging(true);
#endif
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>(ConfigureCategory);
            builder.Entity<CategoryTopic>(ConfigureCategoryTopic);
            builder.Entity<Customer>(ConfigureCustomer);
            builder.Entity<Project>(ConfigureProject);
            builder.Entity<Tag>(ConfigureTag);
            builder.Entity<CollectionElement>(ConfigureCollectionElement);
            builder.Entity<Topic>(ConfigureTopic);
            builder.Entity<RelatedTopic>(ConfigureRelatedTopic);
            builder.Entity<ReferencedFragment>(ConfigureReferencedfragment);

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<CollectionElement> CollectionElements { get; set; }

        private void ConfigureCategory(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.Property(x => x.Id)
                .ForSqlServerUseSequenceHiLo("category_hilo");

            IMutableNavigation navigation;

            navigation = builder.Metadata.FindNavigation(nameof(Category.Topics));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            navigation = builder.Metadata.FindNavigation(nameof(Category.Categories));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigureCategoryTopic(EntityTypeBuilder<CategoryTopic> builder)
        {
            builder.ToTable("CategoryTopic");
            builder.HasKey(x => new { x.ProjectId, x.ParentCategoryId, x.TopicId });

            IMutableNavigation navigation;

            navigation = builder.Metadata.FindNavigation(nameof(CategoryTopic.Topic));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
        private void ConfigureCustomer(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.Property(x => x.Id)
                .ForSqlServerUseSequenceHiLo("customer_hilo")
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
        private void ConfigureProject(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Project");

            builder.Property(x => x.Id)
                .ForSqlServerUseSequenceHiLo("project_hilo")
                .IsRequired();

            builder.HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

        }
        private void ConfigureTag(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tag");

            builder.Property(x => x.Id)
                .ForSqlServerUseSequenceHiLo("tag_hilo")
                .IsRequired();

            builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(50);
        }

        private void ConfigureCollectionElement(EntityTypeBuilder<CollectionElement> builder)
        {
            builder.ToTable("CollectionElement");

            builder.Property(x => x.Id)
                .ForSqlServerUseSequenceHiLo("collectionElement_hilo")
                .IsRequired();

            builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(50);

            builder.HasMany(x => x.ElementTopics);

            builder.HasMany(x => x.ElementTopics)
                .WithOne();

        }
        private void ConfigureTopic(EntityTypeBuilder<Topic> builder)
        {
            builder.ToTable("Topic");

            //builder.HasKey(x => new { x.Id, x.ProjectId });

            builder.Property(x => x.Id)
                .ForSqlServerUseSequenceHiLo("topic_hilo")
                .IsRequired();

            builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(50);

            builder.Property(x => x.Description)
               .HasMaxLength(200);

            builder.HasMany(x => x.RelatedTopics)
                .WithOne(x => x.ParentTopic);

            builder.HasMany(x => x.ReferencedFragments)
                .WithOne(x => x.ParentTopic);

            builder.HasMany(x => x.CollectionElements)
                .WithOne(x => x.Topic);

            IMutableNavigation navigation;
            navigation = builder.Metadata.FindNavigation(nameof(Topic.Tags));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            navigation = builder.Metadata.FindNavigation(nameof(Topic.RelatedTopics));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            navigation = builder.Metadata.FindNavigation(nameof(Topic.CollectionElements));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            navigation = builder.Metadata.FindNavigation(nameof(Topic.ReferencedFragments));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigureRelatedTopic(EntityTypeBuilder<RelatedTopic> builder)
        {
        }

        private void ConfigureReferencedfragment(EntityTypeBuilder<ReferencedFragment> builder)
        {
        }
    }
}
