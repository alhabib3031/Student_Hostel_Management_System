namespace Student_Hostel_Management_System.Data.Entites
{
    public class Student
    {
        public Guid Id { get; set; }
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Guid AdministrationId { get; set; }
        public Administration Administration { get; set; }
    }
}
