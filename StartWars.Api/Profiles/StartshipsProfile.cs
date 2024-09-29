using StartWars.Api.ViewModels;
using StarWars.Application.DTO;

namespace StartWars.Api.Profiles
{
    public class StartshipsProfile : AutoMapper.Profile
    {
        public StartshipsProfile() 
        {
            CreateMap<StartshipsDTO, StartshipsViewModel>().PreserveReferences();
            CreateMap<StartshipsViewModel, StartshipsDTO>().PreserveReferences();
        }
    }
}
