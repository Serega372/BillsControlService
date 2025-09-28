using AutoMapper;
using BillsControl.ApplicationCore.Dtos;
using BillsControl.ApplicationCore.Entities;

namespace BillsControl.ApplicationCore.Services
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
