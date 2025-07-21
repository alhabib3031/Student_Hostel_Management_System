using Student_Hostel_Management_System.Data.Entites;

namespace Student_Hostel_Management_System.Services.Interfaces
{
    public interface IAdminstrationDataService: IDataService<Admin>
    {
        Task<int> GetStudentsCountByAdminId(Guid adminId);
    }
}
