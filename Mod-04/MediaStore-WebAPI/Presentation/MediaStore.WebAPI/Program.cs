/* SDN-WebAPI: Mod04 MediaStore.WebAPI 
   - Added EF Core tools and nugets
      PM> dotnet tool install --global dotnet-ef
      PM> dotnet ef --version
      Added nuget Packages:
      Microsoft.EntityFrameworkCore
      Microsoft.EntityFrameworkCore.Design
      Microsoft.EntityFrameworkCore.Sqlite
   - Added     
      BackendDataContext: DbContext
      builder.Services.AddDbContext<BackendDataContext>( ...
      in appsettings.json
        "ConnectionStrings": {
          "DefaultConnection": "Data Source=backend.sqlite"
        },...
   - Migrations 
      dotnet ef migrations add initial
      dotnet ef database update
        to remove if necessary 
        (delete first migrations folder and database file)
      dotnet ef migrations remove
   - Tested with MediaStore.WebAPI.http ==> all working
   - To repeat test:
      - delete migrations folder
      - delete backend.sqlite (also *wal, also another)
      - run command: dotnet ef migrations remove
      - run commands:
           dotnet ef migrations add initial
           dotnet ef database update
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
