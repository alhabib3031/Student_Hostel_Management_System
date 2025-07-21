using Humanizer.DateTimeHumanizeStrategy;
using Microsoft.EntityFrameworkCore;
using Student_Hostel_Management_System.Data;
using Student_Hostel_Management_System.Data.Entites;
using Student_Hostel_Management_System.Services.Interfaces;

namespace Student_Hostel_Management_System.Services
{
    public class AdminstrationDataService : IAdminstrationDataService
    {
        // This class is responsible for managing the administration data.

        // Read About Async Await Pattern in C#.

        // Race Comdition in C# is a situation where two or more threads access shared data and try to change it at the same time.

        // Injection of the ApplicationDbContext to interact with the database.


        private readonly ApplicationDbContext _dbContext;
        //private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public AdminstrationDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // WPF // async await pattern for asynchronous operations. 
        public async Task<Admin> Add(Admin entity)
        {
            var addedAdmin = await _dbContext.Administrations.AddAsync(entity);
            await SaveChangesAsync();
            return addedAdmin.Entity;
        }

        public async Task<Admin> DeleteById(Guid id)
        {
            var admin = await _dbContext.Administrations.FirstOrDefaultAsync(ad => ad.Id == id);
            if (admin == null)
            {
                throw new ArgumentException($"No administration found with id {id}");
            }
            _dbContext.Administrations.Remove(admin);
            await SaveChangesAsync();
            return admin;
        }

        public Task<List<Admin>> GetAll()
        {
            return _dbContext.Administrations.Include(ad => ad.Students).ToListAsync();
        }

        public async Task<Admin?> GetById(Guid id)
        {
            var admin = await _dbContext.Administrations.Include(ad => ad.Students).FirstOrDefaultAsync(ad => ad.Id == id); // i can found list student by admin id also 
            return admin;
        }

        public async Task<Admin> Update(Guid id, Admin updatedEntity)
        {
            var admin = _dbContext.Administrations.Include(ad => ad.Students).FirstOrDefault(ad => ad.Id == id);
            
            if (admin == null)
            {
                throw new ArgumentException($"No administration found with id {id}");
            }

            admin.NID = updatedEntity.NID;
            admin.Name = updatedEntity.Name;
            admin.Phone = updatedEntity.Phone;
            admin.Email = updatedEntity.Email;
            admin.Description = updatedEntity.Description;

            _dbContext.Administrations.Update(admin);
            _dbContext.Entry(admin).State = EntityState.Modified;
            await SaveChangesAsync();
            return admin;
        }

        private Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
