using RestSharp;
using StarWars.Domain.Interfaces.Services;

namespace StarWars.Domain.Services
{
    public class SWApiService : ISWApiService
    {
        public async Task<RestResponse> GetFromSwApi()
        {
            var client = new RestClient("https://swapi.dev/api/");
            RestRequest request = new RestRequest($"starships/", Method.Get);

            return await client.ExecuteAsync(request);
        }
    }
}
