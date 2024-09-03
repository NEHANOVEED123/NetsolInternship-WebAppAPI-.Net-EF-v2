using WebApplication_API_version2.DTO; // Imports the DTOs.
using WebApplication_API_version2.Models; // Imports the models.



// trim fields

namespace WebApplication_API_version2.Mappers // Defines the namespace for the mappers.
{
    public static class PersonMappers // Static class containing extension methods for mapping.
    {
        public static PersonDTO ToPersonDto(this Person personModel) // Extension method to map a Person model to a PersonDTO.
        {
            return new PersonDTO
            {
                Id = personModel.Id, // Maps the ID.
                Name = personModel.Name, // Maps the Name.
                JoiningDate = personModel.JoiningDate, // Maps the Joining Date.
                Salary = personModel.Salary, // Maps the Salary.
                MarketCapital = personModel.MarketCapital, // Maps the Market Capital.
            };
        }

        public static Person ToPersonFromCreateDto(this CreatePersonDTO personDto) // Extension method to map a CreatePersonDTO to a Person model.
        {
            return new Person
            {
                Name = personDto.Name, // Maps the Name.
                Salary = personDto.Salary, // Maps the Salary.
                MarketCapital = personDto.MarketCapital, // Maps the Market Capital.
            };
        }
    }
}
