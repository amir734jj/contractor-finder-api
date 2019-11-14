using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Abstracts;
using Models.Entities;
using Models.Entities.Common;
using Models.Entities.Contractors;
using Models.Entities.Homeowners;
using Models.Entities.Internals;
using Models.Entities.Projects;
using Models.Entities.Users;

namespace Dal.Utilities
{
    public sealed class EntityDbContext: DbContext
    {
        public DbSet<Contractor> Contractors { get; set; }

        public DbSet<Homeowner> Homeowners { get; set; }
        
        public DbSet<InternalUser> AdminUsers { get; set; }

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
    }
}