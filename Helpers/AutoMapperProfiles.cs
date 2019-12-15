using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Dtos;
using WebApplication2.Models;

namespace WebApplication2.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Users, UserForDetailedDTO>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.isMain).url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculatAge()));
            CreateMap<Users, UserForListDto>()
                 .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.isMain).url))
                  .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculatAge()));
            CreateMap<Photos, PhotosForDetailsDto>();
            CreateMap<UserForUpdateDto, Users>();
            CreateMap<Photos, PhotoToReturnDTO>();
            CreateMap<PhotoForCreationDto, Photos>();

            CreateMap<UserToRegisterDto, Users>();
        }

     
    }
}
