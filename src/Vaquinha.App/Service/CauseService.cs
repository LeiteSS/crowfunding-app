using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.App.Entities;
using Vaquinha.App.Interfaces.Repository;
using Vaquinha.App.Interfaces.Service;
using Vaquinha.App.Models;

namespace Vaquinha.App.Service
{
    public class CauseService : ICauseService
    {
        private readonly IMapper _mapper;
        private readonly ICauseRepository _causeRepository;

        public CauseService(ICauseRepository causeRepository,
                            IMapper mapper)
        {
            _mapper = mapper;
            _causeRepository = causeRepository;
        }

        public async Task Add(CauseViewModel model)
        {
            var cause = _mapper.Map<CauseViewModel, Cause>(model);
            await _causeRepository.Add(cause);
        }

        public async Task<IEnumerable<CauseViewModel>> RestoreCauses()
        {
            var causes = await _causeRepository.RestoreCauses();
            return _mapper.Map<IEnumerable<Cause>, IEnumerable<CauseViewModel>>(causes);
        }
    }
}