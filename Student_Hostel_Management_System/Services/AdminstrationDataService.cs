using Humanizer.DateTimeHumanizeStrategy;
using Microsoft.EntityFrameworkCore;
using Student_Hostel_Management_System.Data;
using Student_Hostel_Management_System.Data.Entites;
using Student_Hostel_Management_System.Services.Interfaces;

namespace Student_Hostel_Management_System.Services
{
    public class AdminstrationDataService : IAdminstrationDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminstrationDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Admin> Add(Admin entity)
        {
            var addedAdmin = await _dbContext.Administrations.AddAsync(entity);
            await SaveChangesAsync();
            return addedAdmin.Entity;
        }

        // حذف منطقي - لا يحذف من قاعدة البيانات فعلياً
        public async Task<Admin> DeleteById(Guid id)
        {
            var admin = await _dbContext.Administrations
                .Include(ad => ad.Students.Where(s => !s.IsDeleted))
                .FirstOrDefaultAsync(ad => ad.Id == id && !ad.IsDeleted);
                
            if (admin == null)
            {
                throw new ArgumentException($"No administration found with id {id}");
            }

            // تطبيق الحذف المنطقي على المدير
            admin.IsDeleted = true;
            admin.DeletedAt = DateTime.Now;

            // تطبيق الحذف المنطقي على جميع الطلاب المرتبطين بهذا المدير
            foreach (var student in admin.Students)
            {
                student.IsDeleted = true;
                student.DeletedAt = DateTime.Now;
            }

            _dbContext.Administrations.Update(admin);
            await SaveChangesAsync();
            return admin;
        }

        // إضافة دالة للحصول على عدد الطلاب المرتبطين بالمدير
        public async Task<int> GetStudentsCountByAdminId(Guid adminId)
        {
            return await _dbContext.Students
                .CountAsync(s => s.AdminId == adminId && !s.IsDeleted);
        }

        // تعديل GetAll لإرجاع المدراء غير المحذوفين فقط
        public Task<List<Admin>> GetAll()
        {
            return _dbContext.Administrations
                .Include(ad => ad.Students.Where(s => !s.IsDeleted))
                .Where(ad => !ad.IsDeleted)
                .ToListAsync();
        }

        public async Task<Admin?> GetById(Guid id)
        {
            var admin = await _dbContext.Administrations
                .Include(ad => ad.Students.Where(s => !s.IsDeleted))
                .FirstOrDefaultAsync(ad => ad.Id == id && !ad.IsDeleted);
            return admin;
        }

        public async Task<Admin> Update(Guid id, Admin updatedEntity)
        {
            var admin = _dbContext.Administrations
                .Include(ad => ad.Students.Where(s => !s.IsDeleted))
                .FirstOrDefault(ad => ad.Id == id && !ad.IsDeleted);
            
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