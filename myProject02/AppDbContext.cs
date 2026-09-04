using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Cryptography.X509Certificates;

namespace myProject02.Models

{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           
    }
        public DbSet<Voter> Voters { get; set; }

    }
}
