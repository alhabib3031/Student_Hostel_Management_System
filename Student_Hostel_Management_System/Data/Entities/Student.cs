using Student_Hostel_Management_System.Data.Enums;

namespace Student_Hostel_Management_System.Data.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string CollegeName { get; set; }
        public enStatus Status { get; set; } // نظامي ام غير نظامي ام موقوف ام متخرج ام مفصول

    }
}
