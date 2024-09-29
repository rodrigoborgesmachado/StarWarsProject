namespace StarWars.Application.DTO
{
    public class ResponseStarshipsDTO
    {
        public int Count { get; set; }
        public IEnumerable<StartshipsDTO> Results { get; set; }
    }
}
