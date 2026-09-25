using Microsoft.EntityFrameworkCore;
using MyBackendApp.Services;
using myProject02.Models;
using myProject02.Services;
using myProject02.Services.Interfaces;

namespace myProject02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<Ivoterservice, VoterService>();
            builder.Services.AddScoped<IRoleService,RoleService>();
            builder.Services.AddScoped<IUserService, UserService>();

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("VoterConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
