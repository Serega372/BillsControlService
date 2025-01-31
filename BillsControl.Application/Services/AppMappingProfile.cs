using AutoMapper;
using BillsControl.Core;

namespace BillsControl.Application.Services
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<ResidentDto, ResidentEntity>();
            CreateMap<PersonalBillsResponse, PersonalBillEntity>();
        }
    }
}
