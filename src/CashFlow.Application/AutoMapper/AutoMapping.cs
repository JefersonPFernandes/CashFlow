using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityResponse();
        }

        private void RequestToEntity()
        {
            CreateMap<RequestRegisterUserJson, User>().ForMember(entityUser => entityUser.Password, config => config.Ignore());
            CreateMap<RequestExpenseJson, Expense>().ForMember(dest => dest.Tags, config => config.MapFrom(source => source.Tags.Distinct()));
            CreateMap<Communication.Enums.Tag, Tag>().ForMember(dest => dest.Value, config => config.MapFrom(source => source));
        }

        private void EntityResponse()
        {
            CreateMap<Expense, ResponseExpenseJson>().ForMember(dest => dest.Tags, config => config.MapFrom(source => source.Tags.Select(tag => tag.Value)));

            CreateMap<Expense, ResponseRegisteredExpenseJson>();
            CreateMap<Expense, ResponseShortExpenseJson>();
            CreateMap<User, ResponseUserProfileJson>();
        }
    }
}
