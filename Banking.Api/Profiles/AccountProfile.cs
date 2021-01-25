using AutoMapper;
using Banking.Api.Dtos;
using Banking.Api.Entities;

namespace Banking.Api.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDto>()
                .ForMember(dest => dest.ResourceId, opt => opt.MapFrom(source => source.AccountId));
        }
    }
}
