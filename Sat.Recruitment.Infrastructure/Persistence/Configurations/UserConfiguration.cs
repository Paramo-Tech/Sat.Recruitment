using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasAlternateKey(u => u.Email);
            builder.HasAlternateKey(u => u.Phone);
            builder.HasAlternateKey(u => new { u.Name, u.Address });
            builder.Property(u => u.Money).HasPrecision(18, 2);
        }
    }
}
