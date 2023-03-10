using Microsoft.EntityFrameworkCore;

namespace Sat.Recruitment.Persistence.Models;

public partial class SatRecruitmentDbContext : DbContext
{
    public SatRecruitmentDbContext()
    {
    }

    public SatRecruitmentDbContext(DbContextOptions<SatRecruitmentDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=SatRecruitmentDb;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Id");

            entity.ToTable("User");

            entity.HasIndex(e => e.Address, "CK_Address_Unique").IsUnique();

            entity.HasIndex(e => e.Email, "CK_Email_Unique").IsUnique();

            entity.HasIndex(e => e.Name, "CK_Name_Unique").IsUnique();

            entity.HasIndex(e => e.Phone, "CK_Phone_Unique").IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Money).HasColumnType("money");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.UserType)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
