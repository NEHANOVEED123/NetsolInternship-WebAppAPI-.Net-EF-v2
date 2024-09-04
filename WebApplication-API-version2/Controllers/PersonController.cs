using Microsoft.AspNetCore.Mvc; // Imports the ASP.NET Core MVC functionalities required to create controllers.
using WebApplication_API_version2.DAL; // Imports the data access layer, specifically the ApplicationDBContext class.
using WebApplication_API_version2.DTO; // Imports the DTO classes for person-related data.
using WebApplication_API_version2.Mappers; // Imports the mapping methods for converting between models and DTOs.
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication_API_version2.Interfaces;
using WebApplication_API_version2.FilterQuery;




namespace WebApplication_API_version2.Controllers // Defines the namespace for the controller class, which is essential for organizing code.
{
    [ApiController] // Specifies that this class is an API controller, which means it can handle HTTP requests.
    [Route("api/person")] // Sets the base route for all actions within this controller, meaning all endpoints will start with "api/person".


    public class PersonController : ControllerBase // Inherits from ControllerBase, providing basic functionalities like returning responses.
    {
        private readonly ApplicationDBContext _context; // Declares a private read-only variable to hold the database context instance.
        private readonly IPersonRepository _personRepo;


        public PersonController(ApplicationDBContext context, IPersonRepository personRepo) // Constructor that receives the database context via dependency injection.
        {
            _personRepo =personRepo;
            _context = context; // Assigns the injected context to the private variable for use within the class.
        }



        [HttpGet] // Specifies that this method responds to HTTP GET requests.
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query) // Method to get all persons from the database.
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var persons = await _personRepo.GetAllAsync( query);
            var personDto=persons.Select(s => s.ToPersonDto()); // Retrieves all persons, converts them to a list, and maps them to DTOs.
            return Ok(personDto); // Returns the list of persons as a 200 OK response.
        }



        [HttpGet("{Id:int}")] // Specifies that this method responds to GET requests with an ID parameter in the URL.
        public async Task<IActionResult> GetById([FromRoute] int Id) // Method to get a person by their ID.
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person =await _personRepo.GetByIdAsync(Id); // Finds the person with the given ID in the database.
            if (person == null) // Checks if the person is not found.
            {
                return NotFound(); // Returns a 404 Not Found response if the person does not exist.
            }
            return Ok(person.ToPersonDto()); // Returns the found person mapped to a DTO as a 200 OK response.
        }



        [HttpPost] // Specifies that this method responds to HTTP POST requests.
        public async Task<IActionResult> Create([FromBody] CreatePersonDTO personDto) // Method to create a new person.
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var personModel = personDto.ToPersonFromCreateDto(); // Maps the incoming DTO to a Person model.
            await _personRepo.CreateAsync(personModel);
            return CreatedAtAction(nameof(GetById), new { id = personModel.Id }, personModel.ToPersonDto()); // Returns a 201 Created response, including the new person's data.
        }



        [HttpPut("{id:int}")] // Specifies that this method responds to HTTP PUT requests with an ID parameter in the URL.
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePersonDTO updateDto) // Method to update an existing person.
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var personModel = await _personRepo.UpdateAsync(id,updateDto); // Finds the person by ID.
            if (personModel == null) // Checks if the person is not found.
            {
                return NotFound(); // Returns a 404 Not Found response if the person does not exist.
            }         
            return Ok(personModel.ToPersonDto()); // Returns the updated person mapped to a DTO as a 200 OK response.
        }



        [HttpDelete("{id:int}")] // Specifies that this method responds to HTTP DELETE requests with an ID parameter in the URL.
        public async Task<IActionResult> Delete([FromRoute] int id) // Method to delete an existing person.
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var personModel = await _personRepo.DeleteAsync(id);
            if (personModel == null) // Checks if the person is not found.
            {
                return NotFound(); // Returns a 404 Not Found response if the person does not exist.
            }

            return NoContent(); // Returns a 204 No Content response, indicating successful deletion.
        }


    }
}
