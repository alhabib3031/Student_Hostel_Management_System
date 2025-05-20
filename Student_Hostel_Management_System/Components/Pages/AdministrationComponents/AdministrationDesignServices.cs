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
    }
}
