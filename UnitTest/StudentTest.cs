using Microsoft.EntityFrameworkCore;
using Student_Hostel_Management_System.Data;
using Student_Hostel_Management_System.Data.Entites;
using Student_Hostel_Management_System.Services;
using Xunit;

namespace UnitTest
{
    public class StudentTest
    {
        private Administration administration;
        private static DbContextOptions<ApplicationDbContext> GetNewContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task Add_ShouldAddNewStudent()
        {
            // Arrange
            var options = GetNewContextOptions();
            using var context = new ApplicationDbContext(options);
            var service = new StudentsDataService(context);

            var student = new Student
            {
                Id = Guid.NewGuid(),
                Name = "Test Student",
                Email = "student@test.com",
                Phone = "0987654321",
                StudentId = 123,
                Address = "Test Address",
                AdministrationId = Guid.NewGuid()
            };

            // Act
            var result = await service.Add(student);

            // Assert
            Assert.NotNull(result);
            var addedStudent = await context.Students.FindAsync(result.Id);
            Assert.NotNull(addedStudent);
            Assert.Equal(student.Name, addedStudent.Name);
            Assert.Equal(student.StudentId, addedStudent.StudentId);
            Assert.Equal(student.Email, addedStudent.Email);
        }

        [Fact]
        public async Task Update_ShouldUpdateExistingStudent()
        {
            // Arrange
            var options = GetNewContextOptions();
            using var context = new ApplicationDbContext(options);
            var service = new StudentsDataService(context);

            var studentId = Guid.NewGuid();
            var existingStudent = new Student
            {
                Id = studentId,
                Name = "Original Student",
                Email = "student@test.com",
                Phone = "0987654321",
                StudentId = 123,
                Address = "Original Address",
                AdministrationId = Guid.NewGuid()
            };

            await context.Students.AddAsync(existingStudent);
            await context.SaveChangesAsync();

            var updatedStudent = new Student
            {
                Id = studentId,
                Name = "Updated Student",
                Email = "updated@test.com",
                Phone = "1234567890",
                StudentId = 456,
                Address = "Updated Address",
                AdministrationId = Guid.NewGuid()
            };

            // Act
            var result = await service.Update(studentId, updatedStudent);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedStudent.Name, result.Name);
            Assert.Equal(updatedStudent.Email, result.Email);
            Assert.Equal(updatedStudent.StudentId, result.StudentId);
            Assert.Equal(updatedStudent.Address, result.Address);
            Assert.Equal(updatedStudent.AdministrationId, result.AdministrationId);
        }

        [Fact]
        public async Task DeleteById_ShouldRemoveStudent()
        {
            // Arrange
            var options = GetNewContextOptions();
            using var context = new ApplicationDbContext(options);
            var service = new StudentsDataService(context);

            var admin = new Administration
            {
                Id = Guid.NewGuid(),
                Name = "Test Admin",
                Email = "admin@test.com",
                Phone = "0987654321",
                NID = 123456789
            };

            var studentId = Guid.NewGuid();
            var student = new Student
            {
                Id = studentId,
                Name = "Student to Delete",
                Email = "delete@test.com",
                Phone = "0987654321",
                StudentId = 123,
                Address = "Test Address",
                AdministrationId = admin.Id
            };

            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();

            // Act
            var deletedStudent = await service.DeleteById(studentId);

            // Assert
            Assert.NotNull(deletedStudent);
            var studentInDb = await context.Students.FindAsync(studentId);
            Assert.Null(studentInDb);
        }

        [Fact]
        public async Task GetById_ShouldReturnStudent_WhenStudentExists()
        {
            // Arrange
            var options = GetNewContextOptions();
            using var context = new ApplicationDbContext(options);
            var service = new StudentsDataService(context);

            var admin = new Administration
            {
                Id = Guid.NewGuid(),
                Name = "Test Admin",
                Email = "admin@test.com",
                Phone = "0987654321",
                NID = 123456789
            };
            
            var studentId = Guid.NewGuid();
            var student = new Student
            {
                Id = studentId,
                Name = "Test Student",
                Email = "student@test.com",
                Phone = "0987654321",
                StudentId = 1234,
                Address = "Test Address",
                AdministrationId = admin.Id
            };

            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();

            // Act
            var result = await service.GetById(studentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(student.Name, result.Name);
            Assert.Equal(student.StudentId, result.StudentId);
            Assert.Equal(student.Email, result.Email);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenStudentDoesNotExist()
        {
            // Arrange
            var options = GetNewContextOptions();
            using var context = new ApplicationDbContext(options);
            var service = new StudentsDataService(context);

            // Act
            var result = await service.GetById(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllStudents()
        {
            // Arrange
            var options = GetNewContextOptions();
            using var context = new ApplicationDbContext(options);
            var service = new StudentsDataService(context);

            var student1 = new Student
            {
                Id = Guid.NewGuid(),
                Name = "Student One",
                Email = "student1@test.com",
                Phone = "0987654321",
                StudentId = 123,
                Address = "Address One",
                AdministrationId = Guid.NewGuid()
            };

            var student2 = new Student
            {
                Id = Guid.NewGuid(),
                Name = "Student Two",
                Email = "student2@test.com",
                Phone = "1234567890",
                StudentId = 456,
                Address = "Address Two",
                AdministrationId = Guid.NewGuid()
            };

            await context.Students.AddRangeAsync(student1, student2);
            await context.SaveChangesAsync();

            // Act
            var results = await service.GetAll();

            // Assert
            Assert.NotNull(results);
            Assert.Equal(2, results.Count);
            Assert.Contains(results, s => s.Name == "Student One");
            Assert.Contains(results, s => s.Name == "Student Two");
        }

        [Fact]
        public async Task DeleteById_ShouldThrowException_WhenStudentDoesNotExist()
        {
            // Arrange
            var options = GetNewContextOptions();
            using var context = new ApplicationDbContext(options);
            var service = new StudentsDataService(context);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                async () => await service.DeleteById(Guid.NewGuid())
            );
        }

        [Fact]
        public async Task Update_ShouldThrowException_WhenStudentDoesNotExist()
        {
            // Arrange
            var options = GetNewContextOptions();
            using var context = new ApplicationDbContext(options);
            var service = new StudentsDataService(context);

            var updatedStudent = new Student
            {
                Id = Guid.NewGuid(),
                Name = "Updated Student",
                Email = "updated@test.com",
                Phone = "1234567890",
                StudentId = 456,
                Address = "Updated Address",
                AdministrationId = Guid.NewGuid()
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                async () => await service.Update(Guid.NewGuid(), updatedStudent)
            );
        }
    }
}