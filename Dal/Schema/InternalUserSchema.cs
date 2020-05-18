using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities.Internals;
using Models.Entities.Users;

namespace Dal.Schema
{
    public class InternalUserSchema : IEntityTypeConfiguration<InternalUser>
    {
        public void Configure(EntityTypeBuilder<InternalUser> builder)
        {
            builder
                .HasOne(x => x.UserRef)
                .WithOne(x => x.InternalUserRef)
                .HasForeignKey<User>(x => x.InternalUserKey)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}