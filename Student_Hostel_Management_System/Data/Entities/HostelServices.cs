using Student_Hostel_Management_System.Data.Entities;

public class HostelDataService
{
    public List<Hostel> Hostels { get; set; } = new();

    public HostelDataService()
    {
        // تحميل بيانات النزل والغرف مرة واحدة عند تشغيل التطبيق
        for (int i = 1; i <= 5; i++)
        {
            var hostel = new Hostel
            {
                Id = i,
                Name = $"بيت {i}"
            };

            for (int j = 1; j <= 200; j++)
            {
                hostel.Rooms.Add(new Room
                {
                    Id = j
                });
            }

            Hostels.Add(hostel);
        }
    }
}
