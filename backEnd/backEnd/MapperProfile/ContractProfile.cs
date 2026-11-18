using AutoMapper;
using backEnd.Contracts;
using Bll.DTOs;
using BLL.DTOs;

namespace backEnd.MapperProfile
{
    public class ContractProfile:Profile
    {
        public ContractProfile()
        {
            //User
            CreateMap<UserDTO, UserResponse>().ReverseMap();
            CreateMap<RegisterRequest, UserRegisterDTO>().ReverseMap();
            //Movie
            CreateMap<MovieDTO, MovieResponse>().ReverseMap();
            CreateMap<MovieDTO, CreateMovieRequest>().ReverseMap();
            CreateMap<MovieDTO, UpdateMovieRequest>().ReverseMap();
            //Comment
            CreateMap<CommentDTO, CommentResponse>().ReverseMap();
            CreateMap<CommentDTO, CommentRequest>().ReverseMap();
            //Filter
            CreateMap<SearchFilterContract, SearchFilterDTO>().ReverseMap();

        }
    }
}
