using Microsoft.EntityFrameworkCore; // Imports the Entity Framework Core library for working with databases.
using WebApplication_API_version2.Models; // Imports the models, specifically the Person class.

namespace WebApplication_API_version2.DAL // Defines the namespace for the data access layer.
{
    public class ApplicationDBContext : DbContext // Inherits from DbContext, which provides methods for database operations.
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) // Constructor that receives the options for the database context.
            : base(dbContextOptions) // Passes the options to the base DbContext class.
        {
        }

        public DbSet<Person> Persons { get; set; } // Represents the "Persons" table in the database.
        public DbSet<Comment> Comments { get; set; } // Represents the "Comments" table in the database.
    }
}
