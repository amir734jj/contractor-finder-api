using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities.Users;

namespace Dal.Schema
{
    public class UserSchema : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(x => x.PhotoRef)
                .WithOne()
                .HasForeignKey<User>(x => x.PhotoKey);
        }
    }
}