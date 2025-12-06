using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Users.Get
{
    public interface IGetUserProfileUseCase
    {
        public Task<ResponseUserProfileJson> Execute();
    }
}
