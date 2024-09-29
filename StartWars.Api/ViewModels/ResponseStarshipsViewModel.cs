namespace StartWars.Api.ViewModels
{
    public class ResponseStarshipsViewModel
    {
        public int Count { get; set; }
        public IEnumerable<StartshipsViewModel> Results { get; set; }
    }
}
