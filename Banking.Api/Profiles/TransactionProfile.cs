using AutoMapper;
using Banking.Api.Dtos;
using Banking.Api.Entities;

namespace Banking.Api.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionSummary, TransactionSummaryDto>();
        }
    }
}
