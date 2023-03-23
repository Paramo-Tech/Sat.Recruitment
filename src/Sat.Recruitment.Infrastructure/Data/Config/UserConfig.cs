using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sat.Recruitment.Domain.Entities.UserAggregate;

namespace Sat.Recruitment.Infrastructure.Data.Config
{
	public class UserConfig : IEntityTypeConfiguration<User>
    {		
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Address).HasColumnName("address");
            builder.Property(x => x.Email).HasColumnName("email");
            builder.Property(x => x.Money).HasColumnName("money");
            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.Phone).HasColumnName("phone");
            builder.Property(x => x.UserType).HasColumnName("userType");
        }
    }
}

