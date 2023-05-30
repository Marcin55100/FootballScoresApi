using AutoMapper;
using FootballScoresApi.Model;
using static FootballScoresApi.Api.Model.Squads;

namespace FootballScoresApi.AutoMapper
{
    public class GlobalProfile : Profile
    {
        public GlobalProfile()
        {
            CreateMap<Player, PlayerDto>().ReverseMap();
        }

    }
}
