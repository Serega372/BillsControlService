using AutoMapper;
using BillsControl.Core.Dtos;
using BillsControl.Core.Entities;

namespace BillsControl.Application.Services
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<ResidentsResponse, ResidentEntity>();
            CreateMap<AddResidentRequest, ResidentEntity>();
            CreateMap<UpdateResidentRequest, ResidentEntity>();
            CreateMap<AddPersonalBillRequest, PersonalBillEntity>();
            CreateMap<PersonalBillsResponse, PersonalBillEntity>();
            CreateMap<UpdatePersonalBillRequest, PersonalBillEntity>();
            
            CreateMap<PersonalBillEntity, PersonalBillsResponse>();
            CreateMap<ResidentEntity, ResidentsResponse>();
        }
    }
}
