/* SDN Web API Mod03 L 
   Web API demo project: MediaStore.WebAPI 
   Clean Architecture ready 
   - Presentation 
   - Core
   - Infrastructure
   Includes:
   - CORS
   - api path
   - controllers
   - Tests via .http file: 7 tests
   - Tests via Swagger collection: added in root  
*/

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
