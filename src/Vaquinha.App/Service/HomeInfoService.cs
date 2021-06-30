using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Vaquinha.App.Config;
using Vaquinha.App.Entities;
using Vaquinha.App.Interfaces.Repository;
using Vaquinha.App.Interfaces.Service;
using Vaquinha.App.Models;

namespace Vaquinha.App.Service
{
    public class HomeInfoService : IHomeInfoService
    {
        private readonly IMapper _mapper;
        private readonly IDonationService _donationService;
        private readonly GlobalAppConfig _globalSettings;
        private readonly IHomeInfoRepository _homeRepository;
        private readonly ICauseRepository _causeRepository;

        public HomeInfoService(IMapper mapper,
                               IDonationService donationService,
                               GlobalAppConfig globalSettings,
                               IHomeInfoRepository homeRepository,
                               ICauseRepository causeRepository)
        {
            _mapper = mapper;
            _donationService = donationService;
            _homeRepository = homeRepository;
            _globalSettings = globalSettings;
            _causeRepository = causeRepository;
        }
        
        public async Task<IEnumerable<CauseViewModel>> RestoreCausesAsync()
        {
            var causes = await _causeRepository.RestoreCauses();
            return _mapper.Map<IEnumerable<Cause>, IEnumerable<CauseViewModel>>(causes);
        }

        public async Task<HomeViewModel> RestoreHomeInfoAsync()
        {
            var initialDataHome = await RestoreTotalDataHome();

            var institutions = RestoreCausesAsync();
            var donations = RestoreDonorsAsync();

            var diffDate = _globalSettings.DeadlineCrowfunding.Subtract(DateTime.Now);

            initialDataHome.RemainingDays = diffDate.Days;
            initialDataHome.RemainingHours = diffDate.Hours;
            initialDataHome.RemainingMinutes = diffDate.Minutes;

            initialDataHome.RemainingAmount = _globalSettings.GoalCrowfunding - initialDataHome.CollectedAmount;
            initialDataHome.CollectedPercentage = initialDataHome.CollectedAmount * 100 / _globalSettings.GoalCrowfunding;

            await Task.WhenAll();
            initialDataHome.Donors = await donations;
            initialDataHome.Institutions = await institutions;

            return initialDataHome;
        }

        private async Task<IEnumerable<DonorViewModel>> RestoreDonorsAsync()
        {
            return await _donationService.RestoreDonorsAsync();
        }

        private async Task<HomeViewModel> RestoreTotalDataHome()
        {
            return await _homeRepository.RestoreHomeInfoAsync();
        }
    }
}