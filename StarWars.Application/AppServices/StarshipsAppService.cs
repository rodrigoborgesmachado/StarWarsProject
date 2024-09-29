using Newtonsoft.Json;
using StarWars.Application.DTO;
using StarWars.Application.Interfaces;
using StarWars.Domain.Interfaces.Services;

namespace StarWars.Application.AppServices
{
    public class StarshipsAppService : IStarshipsAppService
    {
        private readonly ISWApiService _service;
        public StarshipsAppService(ISWApiService apiService) 
        {
            _service = apiService;
        }

        public async Task<List<StartshipsDTO>> GetStarships(string manufacturer = "")
        {
            var response = await _service.GetFromSwApi();

            var result = JsonConvert.DeserializeObject<ResponseStarshipsDTO>(response.Content);

            if (result == null || result.Results == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(manufacturer))
            {
                result.Results = result.Results.Where(i => i.manufacturer.ToUpper().Contains(manufacturer.ToUpper()));
            }

            return result.Results.ToList();
        }
    }
}
