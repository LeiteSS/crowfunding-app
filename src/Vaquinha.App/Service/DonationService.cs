using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.App.Entities;
using Vaquinha.App.Interfaces.Repository;
using Vaquinha.App.Interfaces.Service;
using Vaquinha.App.Models;

namespace Vaquinha.App.Service
{
    public class DonationService : IDonationService
    {
        private readonly IMapper _mapper;
        private readonly IDonationRepository _donationRepository;
        private readonly IDomainNotificationService _domainNotificationService;

        public DonationService(IMapper mapper,
                             IDonationRepository donationRepository,
                             IDomainNotificationService domainNotificationService)
        {
            _mapper = mapper;
            _donationRepository = donationRepository;
            _domainNotificationService = domainNotificationService;
        }
        public async Task AccomplishDonationAsync(DonationViewModel model)
        {
            var entity = _mapper.Map<DonationViewModel, Donation>(model);

            entity.UpdateDateAndTime();

            if (entity.Valid())
            {
                await _donationRepository.AddAsync(entity);
                return;
            }

            _domainNotificationService.Add(entity);
        }

        public async Task<IEnumerable<DonorViewModel>> RestoreDonorsAsync(int pageIndex = 0)
        {
            var donors = await _donationRepository.RestoreDonorsAsync(pageIndex);
            return _mapper.Map<IEnumerable<Donation>, IEnumerable<DonorViewModel>>(donors);
        }
    }
}