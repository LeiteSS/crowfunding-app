using System.Threading.Tasks;
using Vaquinha.App.Models;

namespace Vaquinha.App.Interfaces.Repository
{
    public interface IHomeInfoRepository
    {
         Task<HomeViewModel> RestoreHomeInfoAsync();
    }
}