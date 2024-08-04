using INWalks.APIs.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace INWalks.API.Data
{
    public class INWalksDbContext : DbContext 
    {
        public INWalksDbContext(DbContextOptions<INWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        //Tables
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        //Seed Database

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for Difficulties
            //Easy,Medium,Hard

            #region Seed database - Difficulty
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("d91666cb-a11d-4b02-9021-30acbb4420d3"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("07e117da-4af0-4d67-a726-f1ef77c8bd30"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("137bfca4-40da-4d74-8439-0cb65d8c4a14"),
                    Name = "Hard"
                }
            };

            //seed difficulties to the database.
            modelBuilder.Entity<Difficulty>().HasData(difficulties);
            #endregion

            #region Seed database - Regions

            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("4ede635c-0b25-4f6d-8893-2e4e44570517"),
                    Code = "BOM",
                    Name = "Bombay region",
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("bf5b41fa-2415-488c-943d-a69d1df0ff7e"),
                    Code = "PUN",
                    Name = "Pune region",
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("2d398afc-dc76-4da9-893f-dd84b243ff44"),
                    Code = "KLR",
                    Name = "Kerala region",
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("ac9d1931-9f78-4a6e-9247-9067b2d96d2f"),
                    Code = "CHN",
                    Name = "Chennai region",
                    RegionImageUrl = null
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);

            #endregion


        }


    }
}
