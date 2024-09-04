using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApplication_API_version2.DAL;
using WebApplication_API_version2.Interfaces;
using WebApplication_API_version2.Models;
using System.Threading.Tasks;
using WebApplication_API_version2.DTO;
using WebApplication_API_version2.FilterQuery;

namespace WebApplication_API_version2.Repository
{
    public class PersonRepository : IPersonRepository
    {

        private readonly ApplicationDBContext _context;
        //dependency injection
        public PersonRepository(ApplicationDBContext context) 
        {
            _context = context;
            
        }
        public Task<bool> PersonExists(int id)
        {
            return _context.Persons.AnyAsync(x => x.Id == id); 
        }
        public async Task<Person> CreateAsync(Person personModel)
        {
            await _context.Persons.AddAsync(personModel);
            await _context.SaveChangesAsync();
            return personModel;
        }

        public async Task<Person?> DeleteAsync(int id)
        {
            var personModel = await _context.Persons
                                            .Include(p => p.Comments)
                                            .FirstOrDefaultAsync(x => x.Id == id);
            if (personModel == null)
            {
                return null;
            }

            // Remove the associated comments first
            _context.Comments.RemoveRange(personModel.Comments);

            // Then remove the person
            _context.Persons.Remove(personModel);

            await _context.SaveChangesAsync();

            return personModel;
        }

        //repository pattern
        public async Task<List<Person>> GetAllAsync(QueryObject query)
        {
            var person = _context.Persons.Include(c => c.Comments).AsQueryable(); 
            if(!string.IsNullOrEmpty(query.Name))
            {
                person = person.Where(s=>s.Name.Contains(query.Name));
            }
            if (query.Salary.HasValue)
            {
                person = person.Where(s => s.Salary >= query.Salary.Value);
            }
            return await person.ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await _context.Persons.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

      

        public async Task<Person?> UpdateAsync(int id, UpdatePersonDTO personDTO)
        {
            var existingPerson =await _context.Persons.FirstOrDefaultAsync(x =>x.Id == id);
            if (existingPerson == null)
            {
                return null;
            }
            existingPerson.Name= personDTO.Name;
            existingPerson.Salary = personDTO.Salary;
            existingPerson.MarketCapital = personDTO.MarketCapital;

            await _context.SaveChangesAsync();
            return existingPerson;
        }
    }
}
