using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.App.Models;

namespace Vaquinha.App.Interfaces.Service
{
    public interface ICauseService
    {
        Task Add(CauseViewModel model);
        Task<IEnumerable<CauseViewModel>> RestoreCauses();
    }
}