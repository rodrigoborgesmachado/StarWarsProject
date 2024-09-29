using StarWars.Application.DTO;

namespace StarWars.Application.Interfaces
{
    public interface IStarshipsAppService
    {
        Task<List<StartshipsDTO>> GetStarships(string manufacturer = "");
    }
}
