namespace Student_Hostel_Management_System.Data.Entities
{
    public class RoomAssignment
    {
        public Guid Id { get; set; }
        public List<Hostel> Hostel { get; set; }
        public List<Student> Student { get; set; }
    }
}
