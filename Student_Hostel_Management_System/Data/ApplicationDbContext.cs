using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Student_Hostel_Management_System.Data.Entites;

namespace Student_Hostel_Management_System.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Administration> Administrations { get; set; } = default!;
    public DbSet<Student> Students { get; set; } = default!;
}
