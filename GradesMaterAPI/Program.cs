
using GradesMaterAPI.DB;
using GradesMaterAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace GradesMaterAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register Global DB Service with EF
            // when you want to work with the db so provide my with the GradeMasterDbContext where there is tables.
            // Global Service - we can you use it the service from any where in the application
            builder.Services.AddDbContext<GradeMasterDbContext>(options =>
            {
                options.UseSqlServer("Data Source=DESKTOP-PR461MF\\SQLEXPRESS;Initial Catalog=GradeMasterdb_EFDb;Integrated Security=True;Connect Timeout=30;");
            });

            // TODO Add Onther Service Here....
            // When the application load she build the CsvLoader
            // AddSingleton when that it will inilaze only one intsatnce at a time
            builder.Services.AddSingleton<ICsvLoader, CsvLoader>();


            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    // Get Register GradeMasterDbContext Service
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<GradeMasterDbContext>();
                    // Initialize db
                    DbInitializer.Initialize(context);
                } catch (Exception ex) 
                {
                    //TODO print to looger
                }
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
