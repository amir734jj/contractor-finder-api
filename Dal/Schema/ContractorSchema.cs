using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities.Contractors;
using Models.Entities.Users;

namespace Dal.Schema
{
    public class ContractorSchema : IEntityTypeConfiguration<Contractor>
    {
        public void Configure(EntityTypeBuilder<Contractor> builder)
        {
            builder.HasOne(x => x.UserRef)
                .WithOne(x => x.ContractorRef)
                .HasForeignKey<User>(x => x.ContractorKey)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}