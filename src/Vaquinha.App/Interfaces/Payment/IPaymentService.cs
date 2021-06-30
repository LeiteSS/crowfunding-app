using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.App.Models;

namespace Vaquinha.App.Interfaces.Payment
{
    public interface IPaymentService
    {
        Task<IEnumerable<CauseViewModel>> RestoreInstitutionsAsync(int page = 0);
        Task AddDonationAsync(DonationViewModel doacao);
    }
}