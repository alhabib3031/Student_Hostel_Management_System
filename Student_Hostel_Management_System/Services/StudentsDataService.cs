using Microsoft.EntityFrameworkCore;
using Student_Hostel_Management_System.Data;
using Student_Hostel_Management_System.Data.Entites;
using Student_Hostel_Management_System.Services.Interfaces;

namespace Student_Hostel_Management_System.Services
{
    public class StudentsDataService : IStudentsDataService
    {
        private readonly ApplicationDbContext _dbContext;
        
        public StudentsDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task<Student> Add(Student entity)
        {
            entity.LastModified = DateTime.Now;
            var addedStudent = _dbContext.Students.Add(entity);
            await SaveChangesAsync();
            return addedStudent.Entity;
        }

        // حذف منطقي للطالب
        public async Task<Student> DeleteById(Guid id)
        {
            var student = _dbContext.Students.FirstOrDefault(st => st.Id == id && !st.IsDeleted);
            if (student == null)
            {
                throw new ArgumentException($"No student found with id {id}");
            }
            
            // تطبيق الحذف المنطقي
            student.IsDeleted = true;
            student.DeletedAt = DateTime.Now;
            
            _dbContext.Students.Update(student);
            await SaveChangesAsync();
            return student;
        }

        // تعديل GetAll لإرجاع الطلاب غير المحذوفين فقط
        public async Task<List<Student>> GetAll()
        {
            var students = await _dbContext.Students
                .Include(st => st.Admin)
                .Where(st => !st.IsDeleted && !st.Admin!.IsDeleted)
                .ToListAsync();
            return students;
        }

        public Task<Student?> GetById(Guid id)
        {
            var student = _dbContext.Students
                .Include(st => st.Admin)
                .FirstOrDefaultAsync(st => st.Id == id && !st.IsDeleted);
            return student;
        }

        public async Task<Student> Update(Guid id, Student updatedEntity)
        {
            var student = _dbContext.Students.FirstOrDefault(st => st.Id == id && !st.IsDeleted);
            if (student == null)
            {
                throw new ArgumentException($"No student found with id {id}");
            }
            
            student.StudentId = updatedEntity.StudentId;
            student.Name = updatedEntity.Name;
            student.Phone = updatedEntity.Phone;
            student.Email = updatedEntity.Email;
            student.Address = updatedEntity.Address;
            student.AdminId = updatedEntity.AdminId;
            student.LastModified = DateTime.Now;
            
            _dbContext.Students.Update(student);
            _dbContext.Entry(student).State = EntityState.Modified;
            await SaveChangesAsync();
            return student;
        }
    }
}