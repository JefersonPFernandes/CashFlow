using CashFlow.Application.UseCases.Users.Delete;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Entities.Users;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Tests.Users.Delete
{
    public class DeleteUserAccountUseCaseTest
    {

        [Fact]
        public void Success()
        {
            var user = UserBuilder.Build();

            var useCase = CreateUseCase(user);

            var act = async () => await useCase.Execute();

            act.Should().NotThrowAsync();
        }


        private DeleteUserAccountUseCase CreateUseCase(User user)
        {
            var repository = UserWriteOnlyRepositoryBuilder.Build();

            var loggedUser = LoggedUserBuilder.Build(user);

            var unitOfWork = UnitOfWorkBuilder.Build();

            return new DeleteUserAccountUseCase(loggedUser, repository, unitOfWork);

        }
    }
}
