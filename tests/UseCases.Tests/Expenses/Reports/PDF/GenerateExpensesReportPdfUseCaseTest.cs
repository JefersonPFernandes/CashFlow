using CashFlow.Application.UseCases.Expenses.Reports.PDF;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Entities.Expenses;
using CommonTestUtilities.Entities.Users;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Tests.Expenses.Reports.PDF
{
    public class GenerateExpensesReportPdfUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();

            var expenses = ExpensesBuilder.Collection(loggedUser);

            var useCase = CreateUseCase(loggedUser, expenses);

            var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Success_Empty()
        {
            var loggedUser = UserBuilder.Build();

            var useCase = CreateUseCase(loggedUser, new List<Expense>());
            //var useCase = CreateUseCase(loggedUser, [];    modo simplificado de iniciar uma lista vazia!!!//

            var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

            result.Should().BeEmpty();
        }


        private GenerateExpensesReportPdfUseCase CreateUseCase(User user, List<Expense> expenses)
        {
            var repository = new ExpensesReadOnlyRepositoryBuilder().FilterByMonth(user, expenses).Build();

            var loggedUser = LoggedUserBuilder.Build(user);

            return new GenerateExpensesReportPdfUseCase(repository, loggedUser);
        }
    }
}
