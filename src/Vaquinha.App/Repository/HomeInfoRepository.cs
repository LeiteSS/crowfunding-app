using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Vaquinha.App.Interfaces.Repository;
using Vaquinha.App.Models;
using Vaquinha.App.Repository.Context;

namespace Vaquinha.App.Repository
{
    public class HomeInfoRepository : IHomeInfoRepository
    {
        private readonly CrowfundingOnlineDBContext _dbContext;

        public HomeInfoRepository(CrowfundingOnlineDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<HomeViewModel> RestoreHomeInfoAsync()
        {
            var donorsTotal = _dbContext.Donations.CountAsync();
            var totalValue = _dbContext.Donations.SumAsync(a => a.Value);

            return new HomeViewModel
            {
                CollectedAmount = await totalValue,
                QuantityHonors = await donorsTotal
            };
        }
    }
}