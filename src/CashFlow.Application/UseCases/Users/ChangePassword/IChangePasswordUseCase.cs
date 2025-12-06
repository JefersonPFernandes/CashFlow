using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Users.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        Task Execute(RequestChangePasswordJson request);
    }
}
