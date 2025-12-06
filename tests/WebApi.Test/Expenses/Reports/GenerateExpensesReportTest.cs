using FluentAssertions;
using System.Net;
using System.Net.Mime;

namespace WebApi.Test.Expenses.Reports
{
    public class GenerateExpensesReportTest : CashFlowClassFixture
    {
        private const string METHOD = "api/Report";

        private readonly string _AdminToken;
        private readonly string _TeamMemberToken;
        private readonly DateTime _expenseDate;

        public GenerateExpensesReportTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
        {
            _AdminToken = webApplicationFactory.User_Admin.GetToken();
            _TeamMemberToken = webApplicationFactory.User_Team_Member.GetToken();
            _expenseDate = webApplicationFactory.Expense_Admin.GetDate();
        }

        [Fact]
        public async Task Success_Pdf()
        {
            var result = await DoGet(requestUri: $"{METHOD}/pdf?month={_expenseDate:yyyy-MM}", token: _AdminToken);

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            result.Content.Headers.ContentType.Should().NotBeNull();

            result.Content.Headers.ContentType!.MediaType.Should().Be(MediaTypeNames.Application.Pdf);

        }

        [Fact]
        public async Task Success_Excel()
        {
            var result = await DoGet(requestUri: $"{METHOD}/excel?month={_expenseDate:yyyy-MM}", token: _AdminToken);

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            result.Content.Headers.ContentType.Should().NotBeNull();

            result.Content.Headers.ContentType!.MediaType.Should().Be(MediaTypeNames.Application.Octet);

        }

        [Fact]
        public async Task Error_Forbidden_User_Not_Allowed_Excel()
        {
            var result = await DoGet(requestUri: $"{METHOD}/excel?month={_expenseDate:Y}", token: _TeamMemberToken);

            result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Error_Forbidden_User_Not_Allowed_Pdf()
        {
            var result = await DoGet(requestUri: $"{METHOD}/pdf?month={_expenseDate:Y}", token: _TeamMemberToken);

            result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }
}
