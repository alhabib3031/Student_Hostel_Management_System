namespace Student_Hostel_Management_System.Data.Entites
{
    public class Student
    {
        public Guid Id { get; set; }
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public Guid AdminId { get; set; }
        public Admin? Admin { get; set; }
        
        // إضافة خاصيات التعديل والحذف المنطقي
        public DateTime LastModified { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }
}