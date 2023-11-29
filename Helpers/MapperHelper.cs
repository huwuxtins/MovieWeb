using AutoMapper;
using MovieWeb.DTOs;
using MovieWeb.Models;

namespace MovieWeb.Helpers
{
    public class MapperHelper : Profile
    {
        public MapperHelper()
{
    CreateMap<FilmModel, FilmModel>();
    CreateMap<UserModel, UserModel>();
    CreateMap<CommentModel, CommentModel>();

    CreateMap<UserModel, UserDTO>().ReverseMap();   
    CreateMap<FilmModel, FilmDTO>().ReverseMap();
    CreateMap<CommentModel, CommentDTO>()
        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
        .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
        .ForMember(dest => dest.FilmId, opt => opt.MapFrom(src => src.FilmId))
        .ReverseMap();
}

    }
}
