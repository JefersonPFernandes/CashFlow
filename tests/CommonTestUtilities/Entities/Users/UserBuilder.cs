using Bogus;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CommonTestUtilities.Cryptograph;

namespace CommonTestUtilities.Entities.Users
{
    public class UserBuilder
    {
        public static User Build(string role = Rules.TEAM_MEMBER)
        {
            var passwordEncripter = new PasswordEncrypterBuilder().Build();

            var user = new Faker<User>()
                .RuleFor(user => user.Id, _ => 1)
                .RuleFor(user => user.Name, faker => faker.Person.FirstName)
                .RuleFor(user => user.Email, (faker, user) => faker.Internet.Email(user.Name))
                .RuleFor(user => user.Password, (_, user) => passwordEncripter.Encrypt(user.Password))
                .RuleFor(user => user.UserIdentifier, _ => Guid.NewGuid())
                .RuleFor(user => user.Rule, _ => role);

            return user;          
        }
    }
}
