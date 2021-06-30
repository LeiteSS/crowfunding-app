using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.App.Entities;

namespace Vaquinha.App.Interfaces.Repository
{
    public interface ICauseRepository
    {
        Task<Cause> Add(Cause cause);
        Task<IEnumerable<Cause>> RestoreCauses();
    }
}