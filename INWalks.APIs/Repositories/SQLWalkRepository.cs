using INWalks.API.Data;
using INWalks.APIs.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace INWalks.APIs.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly INWalksDbContext dbContext;

        public SQLWalkRepository(INWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            //Navigation Prop using Include
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }
    }
}
