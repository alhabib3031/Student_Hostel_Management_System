namespace Student_Hostel_Management_System.Services.Interfaces
{
    public interface IDataService<T> where T : class
    {
        public  Task<T?> GetById(Guid id);
        public Task<T> Add(T entity);

        public Task<T> DeleteById(Guid id);

        public Task<T> Update(Guid id,T updatedEntity);

        public Task<List<T>> GetAll();

    }
}
