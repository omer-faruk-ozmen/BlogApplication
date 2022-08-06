using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlogApplication.Api.Domain.Models;
using BlogApplication.Common.Models.Queries;
using BlogApplication.Common.Models.RequestModels;

namespace BlogApplication.Api.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserViewModel>().ReverseMap();

            CreateMap<CreateUserCommandRequest, User>();

            CreateMap<UpdateUserCommandRequest, User>();
        }

    }
}
