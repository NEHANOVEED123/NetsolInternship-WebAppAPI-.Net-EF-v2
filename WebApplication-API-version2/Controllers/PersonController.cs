using Microsoft.AspNetCore.Mvc; // Imports the ASP.NET Core MVC functionalities required to create controllers.
using WebApplication_API_version2.DAL; // Imports the data access layer, specifically the ApplicationDBContext class.
using WebApplication_API_version2.DTO; // Imports the DTO classes for person-related data.
using WebApplication_API_version2.Mappers; // Imports the mapping methods for converting between models and DTOs.
using Microsoft.AspNetCore.JsonPatch;

namespace WebApplication_API_version2.Controllers // Defines the namespace for the controller class, which is essential for organizing code.
{
    [ApiController] // Specifies that this class is an API controller, which means it can handle HTTP requests.
    [Route("api/person")] // Sets the base route for all actions within this controller, meaning all endpoints will start with "api/person".
    public class PersonController : ControllerBase // Inherits from ControllerBase, providing basic functionalities like returning responses.
    {
        private readonly ApplicationDBContext _context; // Declares a private read-only variable to hold the database context instance.

        public PersonController(ApplicationDBContext context) // Constructor that receives the database context via dependency injection.
        {
            _context = context; // Assigns the injected context to the private variable for use within the class.
        }

        [HttpGet] // Specifies that this method responds to HTTP GET requests.
        public IActionResult GetAll() // Method to get all persons from the database.
        {
            var persons = _context.Persons.ToList().Select(s => s.ToPersonDto()); // Retrieves all persons, converts them to a list, and maps them to DTOs.
            return Ok(persons); // Returns the list of persons as a 200 OK response.
        }

        [HttpGet("{Id}")] // Specifies that this method responds to GET requests with an ID parameter in the URL.
        public IActionResult GetById([FromRoute] int Id) // Method to get a person by their ID.
        {
            var person = _context.Persons.Find(Id); // Finds the person with the given ID in the database.
            if (person == null) // Checks if the person is not found.
            {
                return NotFound(); // Returns a 404 Not Found response if the person does not exist.
            }
            return Ok(person.ToPersonDto()); // Returns the found person mapped to a DTO as a 200 OK response.
        }

        [HttpPost] // Specifies that this method responds to HTTP POST requests.
        public IActionResult Create([FromBody] CreatePersonDTO personDto) // Method to create a new person.
        {
            var personModel = personDto.ToPersonFromCreateDto(); // Maps the incoming DTO to a Person model.
            _context.Persons.Add(personModel); // Adds the new person to the database context.
            _context.SaveChanges(); // Saves the changes to the database.
            return CreatedAtAction(nameof(GetById), new { id = personModel.Id }, personModel.ToPersonDto()); // Returns a 201 Created response, including the new person's data.
        }

        [HttpPut("{id}")] // Specifies that this method responds to HTTP PUT requests with an ID parameter in the URL.
        public IActionResult Update([FromRoute] int id, [FromBody] UpdatePersonDTO updateDto) // Method to update an existing person.
        {
            var personModel = _context.Persons.FirstOrDefault(s => s.Id == id); // Finds the person by ID.
            if (personModel == null) // Checks if the person is not found.
            {
                return NotFound(); // Returns a 404 Not Found response if the person does not exist.
            }
            personModel.Name = updateDto.Name; // Updates the person's name.
            personModel.Salary = updateDto.Salary; // Updates the person's salary.
            personModel.MarketCapital = updateDto.MarketCapital; // Updates the person's market capital.
            _context.SaveChanges(); // Saves the changes to the database.
            return Ok(personModel.ToPersonDto()); // Returns the updated person mapped to a DTO as a 200 OK response.
        }

        [HttpDelete("{id}")] // Specifies that this method responds to HTTP DELETE requests with an ID parameter in the URL.
        public IActionResult Delete([FromRoute] int id) // Method to delete an existing person.
        {
            var personModel = _context.Persons.FirstOrDefault(s => s.Id == id); // Finds the person by ID.
            if (personModel == null) // Checks if the person is not found.
            {
                return NotFound(); // Returns a 404 Not Found response if the person does not exist.
            }
            _context.Persons.Remove(personModel); // Removes the person from the database context.
            _context.SaveChanges(); // Saves the changes to the database.
            return NoContent(); // Returns a 204 No Content response, indicating successful deletion.
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<UpdatePersonDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var person = _context.Persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            var personToPatch = new UpdatePersonDTO
            {
                Name = person.Name,
                Salary = person.Salary ?? 0,
                MarketCapital = person.MarketCapital
            };

            patchDoc.ApplyTo(personToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Manually map the patched fields back to the entity model
            person.Name = personToPatch.Name ?? person.Name;
            person.Salary = personToPatch.Salary != 0 ? personToPatch.Salary : person.Salary;
            person.MarketCapital = personToPatch.MarketCapital != 0 ? personToPatch.MarketCapital : person.MarketCapital;

            _context.SaveChanges();

            return Ok(person.ToPersonDto());
        }

    }
}
