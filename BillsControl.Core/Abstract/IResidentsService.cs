using BillsControl.Core.Dtos;

namespace BillsControl.Core.Abstract
{
    public interface IResidentsService
    {
        Task<Guid> AddResident(AddResidentRequest residentDto);
        Task<Guid> DeleteResident(Guid id);

        Task<List<ResidentsResponse>> GetFiltered(ResidentsQueryFilterParams residentsQueryFilterParams);
        Task<ResidentsResponse?> GetByResidentId(Guid id);
        Task<Guid> UpdateResident(Guid id, UpdateResidentRequest updatedResidentDto);
    }
}