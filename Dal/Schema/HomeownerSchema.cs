using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities.Homeowners;
using Models.Entities.Users;

namespace Dal.Schema
{
    public class HomeownerSchema : IEntityTypeConfiguration<Homeowner>
    {
        public void Configure(EntityTypeBuilder<Homeowner> builder)
        {
            builder
                .HasOne(x => x.UserRef)
                .WithOne(x => x.HomeownerRef)
                .HasForeignKey<User>(x => x.HomeownerKey)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}