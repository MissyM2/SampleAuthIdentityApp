using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SampleAuthWebApp.Authorization;
using SampleAuthWebApp.DTO;
using SampleAuthWebApp.Pages.Account;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace SampleAuthWebApp.Pages
{
    [Authorize (Policy = "HRManagerOnly")]
    public class HRManagerModel : PageModel
    {

        private readonly IHttpClientFactory httpClientFactory;

        [BindProperty]
        public List<WeatherForecastDTO> weatherForecastItems { get; set; } = new List<WeatherForecastDTO>();

        public HRManagerModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task OnGetAsync()
        {
            var httpClient = httpClientFactory.CreateClient("MyWebAPI");

            // in order to gain access to the weather forecast endpoint, we have to post to the auth endpoint the token
            // you must send a credential with username and pssword.  This would be kept in the AppSettings normally, but they are here for demo purposes
            var res = await httpClient.PostAsJsonAsync("auth", new Credential { UserName = "admin", Password="password"});

            // ensure success.  if not, then an exception will be thrown
            res.EnsureSuccessStatusCode();

            // read the response into a string: includes the expiry time
            string strJwt = await res.Content.ReadAsStringAsync();

            // need to deserialize this into an object
            var token = JsonConvert.DeserializeObject<JwtToken>(strJwt);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.AccessToken??string.Empty);

            // it is called WeatherForecast because it corresponds to WeatherForecastController from WebAPI
            weatherForecastItems = await httpClient.GetFromJsonAsync<List<WeatherForecastDTO>>("WeatherForecast")??new List<WeatherForecastDTO>();
        }
    }
}
