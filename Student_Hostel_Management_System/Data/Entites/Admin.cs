namespace Student_Hostel_Management_System.Data.Entites
{
    public class Admin
    {
        public Guid Id { get; set; }
        public int NID { get; set; }
        public short HostelNo { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        
        // إضافة خاصية الحذف المنطقي
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        
        public List<Student> Students { get; set; } = new();
    }
}