using RestSharp;

namespace StarWars.Domain.Interfaces.Services
{
    public interface ISWApiService
    {
        Task<RestResponse> GetFromSwApi();
    }
}
