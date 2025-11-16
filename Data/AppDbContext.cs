using Microsoft.EntityFrameworkCore;
using notesy_api_c_sharp.Models;
namespace notesy_api_c_sharp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        public DbSet<Note> Notes { get; set; }
    }
}
