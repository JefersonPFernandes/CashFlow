using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebApi.Test
{
    public class CashFlowClassFixture : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        public CashFlowClassFixture(CustomWebApplicationFactory webApplicationFactory)
        {
            _httpClient = webApplicationFactory.CreateClient();
        }

        protected async Task<HttpResponseMessage> DoPost(string RequestUri, object request, string token = "", string culture = "en")
        {
            AuthorizeRequest(token);

            ChangeRequestCulture(culture);

            return await _httpClient.PostAsJsonAsync(RequestUri, request);

        }

        protected async Task<HttpResponseMessage> DoPut(string RequestUri, object request, string token, string culture = "en")
        {
            AuthorizeRequest(token);

            ChangeRequestCulture(culture);

            return await _httpClient.PutAsJsonAsync(RequestUri, request);

        }

        protected async Task<HttpResponseMessage> DoGet(string requestUri, string token, string culture = "en")
        {
            AuthorizeRequest(token);

            ChangeRequestCulture(culture);

            return await _httpClient.GetAsync(requestUri);
        }

        protected async Task<HttpResponseMessage> DoDelete(string requestUri, string token, string culture = "en")
        {
            AuthorizeRequest(token);

            ChangeRequestCulture(culture);

            return await _httpClient.DeleteAsync(requestUri);
        }

        private void AuthorizeRequest(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }

        private void ChangeRequestCulture(string culture)
        {
            _httpClient.DefaultRequestHeaders.AcceptLanguage.Clear();

            _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(culture));
        }
    }
}
