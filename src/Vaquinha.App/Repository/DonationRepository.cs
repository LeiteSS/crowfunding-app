using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.App.Config;
using Vaquinha.App.Entities;
using Vaquinha.App.Interfaces.Repository;
using Vaquinha.App.Repository.Context;

namespace Vaquinha.App.Repository
{
    public class DonationRepository : IDonationRepository
    {
        private readonly ILogger<DonationRepository> _logger;
        private readonly GlobalAppConfig _globalSettings;
        private readonly CrowfundingOnlineDBContext _crowfundingOnlineDBContext;

        public DonationRepository(GlobalAppConfig globalSettings,
                                CrowfundingOnlineDBContext crowfundingDbContext,
                                ILogger<DonationRepository> logger)
        {
            _globalSettings = globalSettings;
            _crowfundingOnlineDBContext = crowfundingDbContext;
            _logger = logger;
        }

        public async Task AddAsync(Donation model)
        {
            await _crowfundingOnlineDBContext.Donations.AddAsync(model);
            await _crowfundingOnlineDBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Donation>> RestoreDonorsAsync(int pageIndex = 0)
        {
            return await _crowfundingOnlineDBContext.Donations.Include("PersonalData").ToListAsync();
        }
    }
}