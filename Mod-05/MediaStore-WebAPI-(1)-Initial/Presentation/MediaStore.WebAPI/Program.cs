/* Training SDN-WebAPI 
   Module 05 MediaStore WebAPI project
   - This is an initial version of the project showing the TODO list
   TODO
   [1] Extend the model by adding more tables in Core Models
   [2] Add Value Objects by including them in entities having Id
   [3] Handle Value objects in DbContext at creation stage
       using new .NET 8 approach for complex types
   [4] Add Auditable objects
   [5] Implement tracking via Auditable objects
       using EF entity ChangeTracker 
   [6] Implement DbContextFactory
   [7] Implement DesignTimeDbContextFactoryBase
*/

using MediaStore.WebAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace MediaStore.WebAPI
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // CORS
      var corsConfigName = "MediaStoreCORS";
      builder.Services.AddCors(
        options => 
        { 
          options.AddPolicy(
          corsConfigName, policy => 
          {
             policy.WithOrigins("http://localhost",
                                "https://localhost")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
          });
        });

      // Add services to the container.
      builder.Services.AddControllers();
      builder.Services.AddDbContext<BackendDataContext>(
        options => options.UseSqlite(
          builder.Configuration.GetConnectionString("DefaultConnection")
        )
      );

      // Learn more about configuring Swagger/OpenAPI 
      // at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      var app = builder.Build();
      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();
      app.UseCors(corsConfigName);
      app.UseAuthorization();
      app.MapControllers();
      app.Run();
    }
  }
}
