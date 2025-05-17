namespace Student_Hostel_Management_System.Data.Entities
{
    public class Hostel
    {
        public int Id { get; set; }
        public string Name { get; set; } // اسم البيت (بيت1، بيت2 ...)
        public List<Room> Rooms { get; set; } = new List<Room>();
    }
}
