using Student_Hostel_Management_System.Data.Entites;

namespace Student_Hostel_Management_System.Components.Pages.AdministrationComponents
{
    public class AdministrationDesignServices
    {
        public List<Administration> GetAllAdministrations()
        {
            return new List<Administration>
            {
                new Administration
                {
                    Id = Guid.NewGuid(),
                    NID = 123456789,
                    Name = "John Doe",
                    Phone = "123-456-7890",
                    Email = "mohammed3345@yahoo.com",
                    Discription = "Manager for Hostel Number 1"
                },
                new Administration
                {
                    Id = Guid.NewGuid(),
                    NID = 987654321,
                    Name = "Jane Smith",
                    Phone = "987-654-3210",
                    Email = "mahmoud422@yahoo.com",
                    Discription = "Manager for Hostel Number 2"
                },
                new Administration
                {
                    Id = Guid.NewGuid(),
                    NID = 456789123,
                    Name = "Alice Johnson",
                    Phone = "456-789-1234",
                    Email = "alhabib3031a@gmail.com",
                    Discription = "Manager for Hostel Number 3"
                }
            };
        }

        public Administration GetAdministrationById(Guid Id)
        {
            return new Administration
            {
                Id = Guid.NewGuid(),
                NID = 123456789,
                Name = "John Doe",
                Phone = "123-456-7890",
                Email = "mohammed3345@yahoo.com",
                Discription = "Manager for Hostel Number 1"
            };
        }

        public Administration Update(Administration administration)
        {
            // Simulate updating the administration in a database
            // In a real application, you would use a database context to update the entity
            return administration;
        }

        public Administration Save(Administration administration)
        {
            // Simulate saving the administration to a database
            // In a real application, you would use a database context to save the entity
            return administration;
        }

        public void Delete(Administration administration)
        {
            // Simulate deleting the administration from a database
            // In a real application, you would use a database context to delete the entity
            // No return value needed for deletion
        }
    }
}
