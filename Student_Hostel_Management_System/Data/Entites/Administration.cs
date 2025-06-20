namespace Student_Hostel_Management_System.Data.Entites
{
    public class Administration
    {
        public Guid Id { get; set; }
        public int NID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Discription { get; set; }
        public List<Student> Students { get; set; } = new();
    }
}
