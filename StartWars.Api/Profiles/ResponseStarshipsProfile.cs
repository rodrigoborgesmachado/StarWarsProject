using StartWars.Api.ViewModels;
using StarWars.Application.DTO;

namespace StartWars.Api.Profiles
{
    public class ResponseStarshipsProfile : AutoMapper.Profile
    {
        public ResponseStarshipsProfile() 
        {
            CreateMap<ResponseStarshipsDTO, ResponseStarshipsViewModel>().PreserveReferences();
            CreateMap<ResponseStarshipsViewModel, ResponseStarshipsDTO>().PreserveReferences();
        }
    }
}
