using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities.Contractors;
using Models.Entities.Homeowners;
using Models.Entities.Internals;
using Models.Entities.Users;

namespace Dal.Utilities
{
    public sealed class EntityDbContext: IdentityDbContext<User, UserRole, Guid>
    {
        public DbSet<Contractor> Contractors { get; set; }

        public DbSet<Homeowner> Homeowners { get; set; }
        
        public DbSet<InternalUser> InternalUsers { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Constructor that will be called by startup.cs
        /// </summary>
        /// <param name="optionsBuilderOptions"></param>
        // ReSharper disable once SuggestBaseTypeForParameter
        public EntityDbContext(DbContextOptions<EntityDbContext> optionsBuilderOptions) : base(optionsBuilderOptions)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Contractor>()
                .HasOne(x => x.UserRef)
                .WithOne(x => x.ContractorRef)
                .HasForeignKey<User>(x => x.ContractorKey)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
            
            builder.Entity<Homeowner>()
                .HasOne(x => x.UserRef)
                .WithOne(x => x.HomeownerRef)
                .HasForeignKey<User>(x => x.HomeownerKey)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
            
            builder.Entity<InternalUser>()
                .HasOne(x => x.UserRef)
                .WithOne(x => x.InternalUserRef)
                .HasForeignKey<User>(x => x.InternalUserKey)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}