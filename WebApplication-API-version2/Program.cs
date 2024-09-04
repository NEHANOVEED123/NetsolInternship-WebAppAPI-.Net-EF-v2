using Microsoft.EntityFrameworkCore;
using WebApplication_API_version2.DAL;
using WebApplication_API_version2.Interfaces;
using WebApplication_API_version2.Repository;

namespace WebApplication_API_version2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();//This line registers the controllers with the dependency injection (DI) container. Controllers are responsible for handling HTTP requests in an MVC pattern.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();//is used to help discover API endpoints in the application.
            builder.Services.AddSwaggerGen();//registers the Swagger generator, which is used to produce the OpenAPI specification. Swagger is used to describe the API and create interactive documentation.


            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IPersonRepository, PersonRepository>();



            var app = builder.Build();//method compiles and returns an instance of the WebApplication. This is where the app’s request pipeline is configured.

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())//This block checks if the application is running in the development environment (IsDevelopment() returns true).
            {
                app.UseSwagger();//enables the Swagger middleware, which serves the generated OpenAPI specification as JSON.
                app.UseSwaggerUI();//enables the Swagger UI middleware, which serves an interactive API documentation page.
            }

            app.UseHttpsRedirection();//orces the application to redirect all HTTP requests to HTTPS, which is a more secure protocol.

            app.UseAuthorization();//dds the authorization middleware to the request pipeline, ensuring that access to endpoints is controlled based on the authorization policies.


            app.MapControllers();// sets up the endpoints for the controllers. This maps incoming HTTP requests to the appropriate controller actions.

            app.Run();//starts the application and begins listening for incoming HTTP requests. This is the last step that makes the application live.
        }
    }
}
