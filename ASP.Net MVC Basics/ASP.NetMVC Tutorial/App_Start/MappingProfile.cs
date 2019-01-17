using ASP.NetMVC_Tutorial.Dtos;
using ASP.NetMVC_Tutorial.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NetMVC_Tutorial.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(m=>m.Id, opt=>opt.Ignore());

            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MovieDto, Movie>().ForMember(m=>m.Id, opt=>opt.Ignore());

            Mapper.CreateMap<MembershipType, MembershipTypeDto>();
            Mapper.CreateMap<MembershipTypeDto, MembershipType>();

            Mapper.CreateMap<Genre, GenreDto>();
            Mapper.CreateMap<GenreDto, Genre>();
        }
    }
}