using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.App.Entities;

namespace Vaquinha.App.Interfaces.Repository
{
    public interface IDonationRepository
    {
        Task AddAsync(Donation model);
        Task<IEnumerable<Donation>> RestoreDonorsAsync(int pageIndex = 0);
    }
}