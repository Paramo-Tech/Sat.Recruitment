
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserE>
    {
        public void Configure(EntityTypeBuilder<UserE> builder)
        {
            builder.
                HasIndex(b => b.Id);
            builder
                .Property(b => b.Id)
                .IsRequired();
        }
    }
}
