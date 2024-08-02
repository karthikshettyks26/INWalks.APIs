using INWalks.APIs.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace INWalks.API.Data
{
    public class INWalksDbContext : DbContext 
    {
        public INWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        //Tables
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
