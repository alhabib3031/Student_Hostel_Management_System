using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Student_Hostel_Management_System.Data.Entites;

namespace Student_Hostel_Management_System.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : IdentityDbContext(options)
{
    public DbSet<Admin> Administrations { get; set; } = default!;
    public DbSet<Student> Students { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
          base.OnModelCreating(builder);

          // ================= Admin =================
          builder.Entity<Admin>(entity =>
          {
              entity.HasKey(e => e.Id);

              entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWID()");

              entity.Property(e => e.NID)
                    .IsRequired();

              entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

              entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsRequired();

              entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsRequired();

              entity.Property(e => e.Description)
                    .HasMaxLength(500);

              // Indexes
              entity.HasIndex(e => e.NID)
                    .IsUnique()
                    .HasDatabaseName("IX_Admin_NID");

              entity.HasIndex(e => e.Email)
                    .IsUnique()
                    .HasDatabaseName("IX_Admin_Email");

              // One-to-many relationship with Students
              entity.HasMany(e => e.Students)
                    .WithOne(s => s.Admin)
                    .HasForeignKey(s => s.AdminId)
                    .OnDelete(DeleteBehavior.Cascade);
          });

          // ================= Student =================
          builder.Entity<Student>(entity =>
          {
              entity.HasKey(e => e.Id);

              entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWID()");

              entity.Property(e => e.StudentId)
                    .IsRequired();

              entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

              entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsRequired();

              entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsRequired();

              entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsRequired();

              entity.Property(e => e.AdminId)
                    .IsRequired();

              // Indexes
              entity.HasIndex(e => e.StudentId)
                    .IsUnique()
                    .HasDatabaseName("IX_Student_StudentId");

              entity.HasIndex(e => e.Email)
                    .IsUnique()
                    .HasDatabaseName("IX_Student_Email");

              entity.HasIndex(e => e.AdminId)
                    .HasDatabaseName("IX_Student_AdministrationId");
          });
    }

}