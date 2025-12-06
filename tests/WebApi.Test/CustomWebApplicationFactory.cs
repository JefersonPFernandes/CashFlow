using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Infrastructure.DataAccess;
using CommonTestUtilities.Entities.Expenses;
using CommonTestUtilities.Entities.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Test.Resources;

namespace WebApi.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public UserIdentityManager User_Team_Member { get; private set; } = default!;
        public UserIdentityManager User_Admin { get; private set; } = default!;
        public ExpensesIdentityManager Expense_MemberTeam {  get; private set; } = default!;
        public ExpensesIdentityManager Expense_Admin {  get; private set; } = default!;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Tests")
                .ConfigureServices(services =>
                { 
                    var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                    services.AddDbContext<CashFlowDbContext>(Config =>
                    {
                        Config.UseInMemoryDatabase("InMemoryDbForTesting");
                        Config.UseInternalServiceProvider(provider);
                    });

                    var scope = services.BuildServiceProvider().CreateScope();

                    var dbContext = scope.ServiceProvider.GetRequiredService<CashFlowDbContext>();
                    var passwordEncrypter = scope.ServiceProvider.GetRequiredService<IPasswordEncrypter>();
                    var tokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();

                    StartDataBase(dbContext, passwordEncrypter, tokenGenerator);

                    
                });
        }       
     
        private void StartDataBase(CashFlowDbContext dbContext, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator tokenGenerator)
        {
            var userTeamMember = AddUserTeamMember(dbContext, passwordEncrypter, tokenGenerator);

            var expenseTeamMember = AddExpenses(dbContext, userTeamMember, expenseId: 1, tagId: 1);

            Expense_MemberTeam = new ExpensesIdentityManager(expenseTeamMember);

            var userAdmin = AddUserAdmin(dbContext, passwordEncrypter, tokenGenerator);

            var expenseAdmin = AddExpenses(dbContext, userAdmin, expenseId: 2, tagId: 2);

            Expense_Admin = new ExpensesIdentityManager(expenseAdmin);

            dbContext.SaveChanges();

            
        }

        private User AddUserTeamMember(CashFlowDbContext dbContext, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator tokenGenerator)
        {
            var user = UserBuilder.Build();

            user.Id = 1;

            var password = user.Password;

            user.Password = passwordEncrypter.Encrypt(user.Password);

            dbContext.Users.Add(user);

            var token = tokenGenerator.Generate(user);

            User_Team_Member = new UserIdentityManager(user, password, token);

            return user;

        }

        private User AddUserAdmin(CashFlowDbContext dbContext, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator tokenGenerator)
        {
            var user = UserBuilder.Build(Rules.ADMIN);

            user.Id = 2;

            var password = user.Password;

            user.Password = passwordEncrypter.Encrypt(user.Password);

            dbContext.Users.Add(user);

            var token = tokenGenerator.Generate(user);

            User_Admin = new UserIdentityManager(user, password, token);

            return user;

        }

        private Expense AddExpenses(CashFlowDbContext dbContext, User user, long expenseId, long tagId) 
        {
            var expense = ExpensesBuilder.Build(user);

            expense.Id = expenseId;

            foreach(var tag in expense.Tags)
            {
                tag.Id = tagId;
                tag.ExpenseId = expenseId;
            }

            dbContext.Expenses.Add(expense);            

            return expense;
        }
    }
}
