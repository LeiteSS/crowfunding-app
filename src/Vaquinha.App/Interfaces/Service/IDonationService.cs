using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.App.Models;

namespace Vaquinha.App.Interfaces.Service
{
    public interface IDonationService
    {
        Task AccomplishDonationAsync(DonationViewModel model);
        Task<IEnumerable<DonorViewModel>> RestoreDonorsAsync(int pageIndex = 0);
    }
}