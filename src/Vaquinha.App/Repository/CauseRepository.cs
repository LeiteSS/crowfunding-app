using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.App.Entities;
using Vaquinha.App.Interfaces.Repository;
using Vaquinha.App.Repository.Context;

namespace Vaquinha.App.Repository
{
    public class CauseRepository : ICauseRepository
    {
        private readonly CrowfundingOnlineDBContext _dbContext;

        public CauseRepository(CrowfundingOnlineDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cause> Add(Cause cause)
        {
            await _dbContext.AddAsync(cause);
            await _dbContext.SaveChangesAsync();

            return cause;
        }

        public async Task<IEnumerable<Cause>> RestoreCauses()
        {
            return await _dbContext.Causes.ToListAsync();
        }
    }
}