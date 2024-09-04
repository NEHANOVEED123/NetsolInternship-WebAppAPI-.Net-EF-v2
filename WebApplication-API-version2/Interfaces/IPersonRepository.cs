using WebApplication_API_version2.DTO;
using WebApplication_API_version2.Models;

namespace WebApplication_API_version2.Interfaces
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAllAsync();
        Task<Person> GetByIdAsync(int id);//first or default 
        Task<Person> CreateAsync(Person personModel);
        Task<Person> UpdateAsync(int id,UpdatePersonDTO personDTO);
        Task<Person> DeleteAsync(int id);
    }
}
