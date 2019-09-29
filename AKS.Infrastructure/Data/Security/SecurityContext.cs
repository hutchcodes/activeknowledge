using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using AKS.AppCore.Security;
using System;

namespace AKS.Infrastructure.Data.Security
{
    public class SecurityContext : DbContext
    {
        private static readonly LoggerFactory _myConsoleLoggerFactory;//=
            //new LoggerFactory(new[] {
            //            new ConsoleLoggerProvider( new Microsoft.Extensions.Options.OptionsMonitor<ConsoleLoggerOptions>( new ConsoleLoggerOptions()) // { a (category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information, true)
            //});
        public SecurityContext(DbContextOptions<SecurityContext> options) : base(options)
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
            builder.Entity<ProjectGroupPermissions>(ConfigureProjectGroupPermissions);
            builder.Entity<Group>(ConfigureGroup);
            builder.Entity<User>(ConfigureUser);
        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<ProjectGroupPermissions> ProjectGroupPermissions { get; set; } = null!;

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(x => x.FirstName)
                .HasMaxLength(50);

            builder.HasMany(x=> x.Groups);
        }

        private void ConfigureGroup(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups");

            builder.Property(x => x.Name)
                .HasMaxLength(50);

            builder.HasMany(x => x.Users);
            builder.HasMany(x => x.ProjectPermissions);
        }
        private void ConfigureProjectGroupPermissions(EntityTypeBuilder<ProjectGroupPermissions> builder)
        {
            builder.ToTable("ProjectGroupPermissions");
            builder.HasKey(x => new { x.ProjectId, x.GroupId });
        }
    }
}
