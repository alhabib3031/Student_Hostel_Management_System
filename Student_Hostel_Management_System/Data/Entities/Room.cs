namespace Student_Hostel_Management_System.Data.Entities
{
    public class Room
    {
        public int Id { get; set; } // رقم الغرفة
        public List<Student> Students { get; set; } = new List<Student>(); //  طالبين كحد أقصى
    }
}
