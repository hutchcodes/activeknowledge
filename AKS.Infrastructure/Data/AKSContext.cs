using System;
using AKS.AppCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AKS.Infrastructure.Data
{
    public partial class AKSContext : DbContext
    {
        public AKSContext()
        {
        }

        public AKSContext(DbContextOptions<AKSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CategoryTopic> CategoryTopics { get; set; } = null!;
        public virtual DbSet<CollectionElement> CollectionElements { get; set; } = null!;
        public virtual DbSet<CollectionElementTopic> CollectionElementTopics { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<RelatedTopic> RelatedTopics { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<Topic> Topics { get; set; } = null!;
        public virtual DbSet<TopicFragment> TopicFragments { get; set; } = null!;
        public virtual DbSet<TopicTag> TopicTags { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.ProjectId });

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ParentCategory)
                    .WithMany(p => p!.Categories)
                    .HasForeignKey(d => new { d.ParentCategoryId, d.ProjectId })
                    .HasConstraintName("FK_Category_Category");
            });

            modelBuilder.Entity<CategoryTopic>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.CategoryId, e.TopicId });

                entity.HasIndex(e => e.CategoryId);

                entity.HasIndex(e => e.TopicId);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p!.CategoryTopics)
                    .HasForeignKey(d => new { d.CategoryId, d.ProjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryTopic_Category");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p!.CategoryTopics)
                    .HasForeignKey(d => new { d.TopicId, d.ProjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryTopic_Topic");
            });

            modelBuilder.Entity<CollectionElement>(entity =>
            {
                entity.HasKey(e => new { e.CollectionElementId, e.ProjectId });

                entity.HasIndex(e => e.TopicId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p!.CollectionElements)
                    .HasForeignKey(d => new { d.TopicId, d.ProjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CollectionElement_Topic");
            });

            modelBuilder.Entity<CollectionElementTopic>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.CollectionElementId, e.TopicId });

                entity.HasIndex(e => e.CollectionElementId);

                entity.HasIndex(e => e.TopicId);

                entity.HasOne(d => d.CollectionElement)
                    .WithMany(p => p!.CollectionElementTopics)
                    .HasForeignKey(d => new { d.CollectionElementId, d.ProjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CollectionElementTopic_CollectionElement");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p!.CollectionElementTopics)
                    .HasForeignKey(d => new { d.TopicId, d.ProjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CollectionElementTopic_Topic");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasIndex(e => e.CustomerId);

                entity.Property(e => e.ProjectId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p!.Projects)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_Customer");
            });

            modelBuilder.Entity<RelatedTopic>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.ParentTopicId, e.ChildTopicId });

                entity.HasIndex(e => e.ChildTopicId);

                entity.HasIndex(e => e.ParentTopicId);

                entity.HasOne(d => d.ParentTopic)
                    .WithMany(p => p!.RelatedToTopics)
                    .HasForeignKey(d => new { d.ChildTopicId, d.ProjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RelatedTopic_Topic");

                entity.HasOne(d => d.ChildTopic)
                    .WithMany(p => p!.RelatedFromTopics)
                    .HasForeignKey(d => new { d.ParentTopicId, d.ProjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RelatedTopic_ParentTopic");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => new { e.TagId, e.ProjectId });

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.HasKey(e => new { e.TopicId, e.ProjectId });

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TopicFragment>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.ParentTopicId, e.ChildTopicId });

                entity.HasIndex(e => e.ChildTopicId);

                entity.HasIndex(e => e.ParentTopicId);

                entity.HasOne(d => d.ChildTopic)
                    .WithMany(p => p!.TopicFragmentsParents)
                    .HasForeignKey(d => new { d.ChildTopicId, d.ProjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fragment_Topic");

                entity.HasOne(d => d.ParentTopic)
                    .WithMany(p => p!.TopicFragmentChildren)
                    .HasForeignKey(d => new { d.ParentTopicId, d.ProjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Topic_Fragment");
            });

            modelBuilder.Entity<TopicTag>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.TopicId, e.TagId });

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p!.TopicTags)
                    .HasForeignKey(d => new { d.TagId, d.ProjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TopicTag_Tag");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p!.TopicTags)
                    .HasForeignKey(d => new { d.TopicId, d.ProjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TopicTag_Topic");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
