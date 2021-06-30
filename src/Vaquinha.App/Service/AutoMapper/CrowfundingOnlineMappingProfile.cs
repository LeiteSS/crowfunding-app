using AutoMapper;
using System;
using Vaquinha.App.Entities;
using Vaquinha.App.Models;

namespace Vaquinha.App.Service.AutoMapper
{
    public class CrowfundingOnlineMappingProfile : Profile
    {
        public CrowfundingOnlineMappingProfile()
        {
            CreateMap<Person, PersonViewModel>();
            CreateMap<Donation, DonationViewModel>();
            CreateMap<Address, AddressViewModel>();
            CreateMap<Cause, CauseViewModel>();
            CreateMap<CreditCard, CreditCardViewModel>();

            CreateMap<Donation, DonorViewModel>()
                .ForMember(dest => dest.Name, m => m.MapFrom(src => src.PersonalData.Name))
                .ForMember(dest => dest.Anonymous, m => m.MapFrom(src => src.PersonalData.Anonymous))
                .ForMember(dest => dest.Message, m => m.MapFrom(src => src.PersonalData.Message))
                .ForMember(dest => dest.Value, m => m.MapFrom(src => src.Value))             
                .ForMember(dest => dest.DateAndTime, m => m.MapFrom(src => src.DateAndTime));

            CreateMap<PersonViewModel, Person>()
                .ConstructUsing(src => new Person(Guid.NewGuid(), src.Name, src.Anonymous, src.Message, src.Email));

            CreateMap<CreditCardViewModel, CreditCard>()
                .ConstructUsing(src => new CreditCard(src.HolderName, src.CreditCardNumber, src.Validity, src.CVV));

            CreateMap<CauseViewModel, Cause>()
                .ConstructUsing(src => new Cause(Guid.NewGuid(), src.Name, src.City, src.State));

            CreateMap<AddressViewModel, Address>()
                .ConstructUsing(src => new Address(Guid.NewGuid(), src.ZipCode, src.Address, src.Complement, src.City, src.State, src.Phone, src.Number));

            CreateMap<DonationViewModel, Donation>()
                .ForCtorParam("value", opt => opt.MapFrom(src => src.Value))
                .ForCtorParam("personalData", opt => opt.MapFrom(src => src.PersonalData))
                .ForCtorParam("formOfPayment", opt => opt.MapFrom(src => src.FormOfPayment))
                .ForCtorParam("billingAddress", opt => opt.MapFrom(src => src.BillingAddress));
        }
    }
}