using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace INWalks.APIs.Data
{
    public class INWalksAuthDbContext : IdentityDbContext
    {
        public INWalksAuthDbContext(DbContextOptions<INWalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "e88ad7cf-da40-4a41-9c50-1faba05ee43a";
            var writerRoleId = "ae3f62bd-8f07-4f8e-8470-a56f7f42748a";

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            //seed roles to database.
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
