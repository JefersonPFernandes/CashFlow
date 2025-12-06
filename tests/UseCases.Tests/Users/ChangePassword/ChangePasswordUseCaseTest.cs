using CashFlow.Application.UseCases.Users.ChangePassword;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtilities.Cryptograph;
using CommonTestUtilities.Entities.Users;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace UseCases.Tests.Users.ChangePassword
{
    public class ChangePasswordUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var user = UserBuilder.Build();

            var request = RequestChangePasswordJsonBuilder.Build();

            var useCase = CreateUseCase(user, request.Password);

            var act = async () => await useCase.Execute(request);

            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Error_NewPassword_Empty()
        {
            var user = UserBuilder.Build();

            var request = RequestChangePasswordJsonBuilder.Build();

            request.NewPassword = string.Empty;

            var useCase = CreateUseCase(user, request.Password);

            var act = async () => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

            result.Where(error => error.GetErrors().Count == 1 && error.GetErrors().Contains(ResourceErrorMessages.INVALID_PASSWORD));
        }

        [Fact]
        public async Task Error_CurrentPassword_Different()
        {
            var user = UserBuilder.Build();

            var request = RequestChangePasswordJsonBuilder.Build();

            var useCase = CreateUseCase(user);

            var act = async () => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

            result.Where(error => error.GetErrors().Count == 1 && error.GetErrors().Contains(ResourceErrorMessages.PASSWORD_DIFFERENT_CURRENT_PASSWORD));
        }

        private static ChangePasswordUseCase CreateUseCase(User user, string? password = null)
        {
            var loggedUser = LoggedUserBuilder.Build(user);

            var passwodEncrypter = new PasswordEncrypterBuilder().Verify(password).Build();

            var userUpdateRepository = UserUpdateOnlyRepositoryBuilder.Build(user);

            var unitOfWork = UnitOfWorkBuilder.Build();

            return new ChangePasswordUseCase(loggedUser, userUpdateRepository, unitOfWork, passwodEncrypter);
        }
    }
}
