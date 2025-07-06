using Microsoft.EntityFrameworkCore;
using Student_Hostel_Management_System.Data;
using Student_Hostel_Management_System.Data.Entites;
using Student_Hostel_Management_System.Services;
using Xunit;

namespace UnitTest
{
    public class AdministrationServiceTests
    {
        private static DbContextOptions<ApplicationDbContext> GetNewContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task Add_ShouldAddNewAdmin()
        {
            // Arrange
            var options = GetNewContextOptions();
            using var context = new ApplicationDbContext(options);
            var service = new AdminstrationDataService(context);

            var admin = new Administration
            {
                Id = Guid.NewGuid(),
                Name = "Test Admin",
                Email = "admin@test.com",
                Phone = "0987654321",
                NID = 123456789,
                Discription = "Test Description"
            };

            // Act
            var result = await service.Add(admin);

            // Assert
            Assert.NotNull(result);
            var addedAdmin = await context.Administrations.FindAsync(result.Id);
            Assert.NotNull(addedAdmin);
            Assert.Equal(admin.Name, addedAdmin.Name);
            Assert.Equal(admin.Email, addedAdmin.Email);
        }

        [Fact]
        public async Task Update_ShouldUpdateExistingAdmin()
        {
            // Arrange
            var options = GetNewContextOptions();
            using var context = new ApplicationDbContext(options);
            var service = new AdminstrationDataService(context);

            var adminId = Guid.NewGuid();
            var existingAdmin = new Administration
            {
                Id = adminId,
                Name = "Original Admin",
                Email = "admin@test.com",
                Phone = "0987654321",
                NID = 123456789,
                Discription = "Test Description"
            };

            await context.Administrations.AddAsync(existingAdmin);
            await context.SaveChangesAsync();

            var updatedAdmin = new Administration
            {
                Id = adminId,
                Name = "Updated Admin",
                Email = "updated@test.com",
                Phone = "1234567890",
                NID = 987654321,
                Discription = "Test Description"
            };

            // Act
            var result = await service.Update(adminId, updatedAdmin);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedAdmin.Name, result.Name);
            Assert.Equal(updatedAdmin.Email, result.Email);
            Assert.Equal(updatedAdmin.Phone, result.Phone);
            Assert.Equal(updatedAdmin.NID, result.NID);
        }

        [Fact]
        public async Task DeleteById_ShouldRemoveAdmin()
        {
            // Arrange
            var options = GetNewContextOptions();
            using var context = new ApplicationDbContext(options);
            var service = new AdminstrationDataService(context);
             
            var adminId = Guid.NewGuid();
            var admin = new Administration
            {
                Id = adminId,
                Name = "Admin to Delete",
                Email = "delete@test.com",
                Phone = "0987654321",
                NID = 123456789,
                Discription = "Test Description"
            };

            await context.Administrations.AddAsync(admin);
            await context.SaveChangesAsync();

            // Act
            var deletedAdmin = await service.DeleteById(adminId);

            // Assert
            Assert.NotNull(deletedAdmin);
            var adminInDb = await context.Administrations.FindAsync(adminId);
            Assert.Null(adminInDb);
        }

        [Fact]
        public async Task GetById_ShouldReturnAdmin_WhenAdminExists()
        {
            // Arrange
            var options = GetNewContextOptions();
            await using var context = new ApplicationDbContext(options);
            var service = new AdminstrationDataService(context);

            var adminId = Guid.NewGuid();
            var admin = new Administration
            {
                Id = adminId,
                Name = "Test Admin",
                Email = "admin@test.com",
                Phone = "0987654321",
                NID = 123456789,
                Discription = "Test Description"
            };

            await context.Administrations.AddAsync(admin);
            await context.SaveChangesAsync();

            // Act
            var result = await service.GetById(adminId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(admin.Name, result.Name);
            Assert.Equal(admin.Email, result.Email);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenAdminDoesNotExist()
        {
            // Arrange
            var options = GetNewContextOptions();
            using var context = new ApplicationDbContext(options);
            var service = new AdminstrationDataService(context);

            // Act
            var result = await service.GetById(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllAdmins()
        {
            // Arrange
            var options = GetNewContextOptions();
            using var context = new ApplicationDbContext(options);
            var service = new AdminstrationDataService(context);

            var admin1 = new Administration
            {
                Id = Guid.NewGuid(),
                Name = "Admin One",
                Email = "admin1@test.com",
                Phone = "0987654321",
                NID = 123456789,
                Discription = "Test Description"
            };

            var admin2 = new Administration
            {
                Id = Guid.NewGuid(),
                Name = "Admin Two",
                Email = "admin2@test.com",
                Phone = "1234567890",
                NID = 987654321,
                Discription = "Test Description"
            };

            await context.Administrations.AddRangeAsync(admin1, admin2);
            await context.SaveChangesAsync();

            // Act
            var results = await service.GetAll();

            // Assert
            Assert.NotNull(results);
            Assert.Equal(2, results.Count);
            Assert.Contains(results, a => a.Name == "Admin One");
            Assert.Contains(results, a => a.Name == "Admin Two");
        }

        [Fact]
        public async Task DeleteById_ShouldThrowException_WhenAdminDoesNotExist()
        {
            // Arrange
            var options = GetNewContextOptions();
            using var context = new ApplicationDbContext(options);
            var service = new AdminstrationDataService(context);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                async () => await service.DeleteById(Guid.NewGuid())
            );
        }

        [Fact]
        public async Task Update_ShouldThrowException_WhenAdminDoesNotExist()
        {
            // Arrange
            var options = GetNewContextOptions();
            using var context = new ApplicationDbContext(options);
            var service = new AdminstrationDataService(context);

            var updatedAdmin = new Administration
            {
                Id = Guid.NewGuid(),
                Name = "Updated Admin",
                Email = "updated@test.com",
                Phone = "1234567890",
                NID = 987654321,
                Discription = "Test Description"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                async () => await service.Update(Guid.NewGuid(), updatedAdmin)
            );
        }
    }
}