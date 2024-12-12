using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Ball_Game_API.Data
{

    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        
        }
        
            

         public DbSet<ScoreHistory> ScoreHistories { get; set; }

    }


}
