using AutoMapper;
using Bll.DTOs;
using BLL.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.MapperProfile
{
    public class DtoProfile:Profile
    {
        public DtoProfile()
        {
            //User
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserRegisterDTO>().ReverseMap();
            //Movie
            CreateMap<Movie, MovieDTO>().ReverseMap();
            //Comment
            CreateMap<MovieComment, CommentDTO>().ReverseMap();
            //Filter
            CreateMap<SearchFilter, SearchFilterDTO>().ReverseMap();
        }
    }
}
