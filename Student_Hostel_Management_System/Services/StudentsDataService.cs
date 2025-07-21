using Microsoft.EntityFrameworkCore;
using Student_Hostel_Management_System.Data;
using Student_Hostel_Management_System.Data.Entites;
using Student_Hostel_Management_System.Services.Interfaces;

namespace Student_Hostel_Management_System.Services
{
    public class StudentsDataService : IStudentsDataService
    {
        // This class is responsible for managing the student data.
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
            var addedStudent = _dbContext.Students.Add(entity);
            await SaveChangesAsync();
            return addedStudent.Entity;
        }

        public async Task<Student> DeleteById(Guid id)
        {
            var student = _dbContext.Students.FirstOrDefault(st => st.Id == id);
            if (student == null)
            {
                throw new ArgumentException($"No student found with id {id}");
            }
            _dbContext.Students.Remove(student);
            await SaveChangesAsync();
            return student;
        }

        public async Task<List<Student>> GetAll()
        {
            var students = await _dbContext.Students
                .Include(st => st.Admin)
                .ToListAsync();
            return students;
        }

        public Task<Student?> GetById(Guid id)
        {
            var student = _dbContext.Students
                .Include(st => st.Admin)
                .FirstOrDefaultAsync(st => st.Id == id);
            return student;
        }

        public Task<Student> Update(Guid id, Student updatedEntity)
        {
            var student = _dbContext.Students.FirstOrDefault(st => st.Id == id);
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
            _dbContext.Students.Update(student);
            _dbContext.Entry(student).State = EntityState.Modified;
            return Task.FromResult(student);
        }
    }
}
