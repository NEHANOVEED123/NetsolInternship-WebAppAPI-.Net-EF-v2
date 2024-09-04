using WebApplication_API_version2.DTO;
using WebApplication_API_version2.FilterQuery;
using WebApplication_API_version2.Models;
using WebApplication_API_version2.FilterQuery;

namespace WebApplication_API_version2.Interfaces
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAllAsync(QueryObject query);
        Task<Person> GetByIdAsync(int id);//first or default 
        Task<Person> CreateAsync(Person personModel);
        Task<Person> UpdateAsync(int id,UpdatePersonDTO personDTO);
        Task<Person> DeleteAsync(int id);
        Task<bool> PersonExists(int id);
    }
}
