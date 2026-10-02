using Microsoft.EntityFrameworkCore;
using MyBackendApp.Services;
using myProject02.Models;
using myProject02.Services;
using myProject02.Services.Interfaces;
using myProject02.Services.Pdf;
using QuestPDF.Infrastructure;

namespace myProject02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            QuestPDF.Settings.License = LicenseType.Evaluation;

            builder.Services.AddScoped<Ivoterservice, VoterService>();
            builder.Services.AddScoped<IRoleService,RoleService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<VoterPdfService>(); 
            builder.Services.AddScoped<UserPdfService>();
            builder.Services.AddScoped<PdfReportService>();
            builder.Services.AddScoped<IPartyService, PartyService>();
            builder.Services.AddScoped<ICandidateService, CandidateService>();
            builder.Services.AddScoped<IElectionService, ElectionService>();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ReactPolicy", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("VoterConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseCors("ReactPolicy");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
