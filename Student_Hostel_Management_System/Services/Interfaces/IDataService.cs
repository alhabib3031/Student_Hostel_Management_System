namespace Student_Hostel_Management_System.Services.Interfaces
{
    // Generics in C# 
    // Struct  value type that can be used to create a data structure that can hold any type of data.
    // records 

    public interface IDataService<T> where T : class
    {
        // public Task<string> AddStringToText(string textToADD); Don't Panic
      //   public Task<List<string>> GetAll();
       
        //public Task<List<List<string>>> Count { get; }

        public Task<T?> GetById(Guid id);
        public Task<T> Add(T entity);

        public Task<T> DeleteById(Guid id);

        public Task<T> Update(Guid id,T updatedEntity);

        public Task<List<T>> GetAll();

    }
}
