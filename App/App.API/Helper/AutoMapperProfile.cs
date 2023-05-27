using Domain= APP.Domain.DTOs  ;
using Api= App.API.DTOs;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace App.API.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Domain.DisplayStudentDTO, Api.DisplayStudentDTO>().ReverseMap();
            CreateMap<Domain.SaveStudentDTO, Api.SaveStudentDTO>().ReverseMap();
            CreateMap<Domain.DisplayClassRoomDTO, Api.DisplayClassRoomDTO>().ReverseMap();
            CreateMap<Domain.DisplaySubjectDTO, Api.DisplaySubjectDTO>().ReverseMap();
            CreateMap<Domain.PagingParameters, Api.PagingParameters>().ReverseMap();
        }
    }
}
