using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Entities.Expenses;
using CommonTestUtilities.Entities.Users;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Tests.Expenses.GetAll
{
    public class GetAllExpensesUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();

            var expenses = ExpensesBuilder.Collection(loggedUser);

            var useCase = CreateUseCase(loggedUser, expenses);

            var result = await useCase.Execute();

            result.Should().NotBeNull();
            result.Expenses.Should().NotBeNullOrEmpty().And.AllSatisfy(expense =>
            {
                expense.Id.Should().BeGreaterThan(0);
                expense.Title.Should().NotBeNullOrEmpty();
                expense.Amount.Should().BeGreaterThan(0);
            });


        }

        private GetAllExpenseUseCase CreateUseCase(User user, List<Expense> expense)
        {
            var repository = new ExpensesReadOnlyRepositoryBuilder().GetAll(user, expense).Build();

            var mapper = MapperBuilder.Build();

            var loggedUser = LoggedUserBuilder.Build(user);

            return new GetAllExpenseUseCase(repository, mapper, loggedUser);
        }
    }
}
