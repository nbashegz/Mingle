using API.DTOs.Users;
using API.Entities;
using AutoMapper;

namespace API.Utilities;
public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile(){
                CreateMap<RegisteredRequest, AppUser>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.Username!.Trim().ToLower()))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email!.Trim().ToLower()))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.Phonenumber));
            }
        }
    
