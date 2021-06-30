using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.App.Models;

namespace Vaquinha.App.Interfaces.Service
{
    public interface IHomeInfoService
    {
        Task<HomeViewModel> RestoreHomeInfoAsync();        
        Task<IEnumerable<CauseViewModel>> RestoreCausesAsync();
    }
}