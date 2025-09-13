using BillsControl.Core.Dtos;
using BillsControl.Core.Entities;

namespace BillsControl.Core.Abstract
{
    public interface IResidentsRepository
    {
        Task<Guid> AddResident(ResidentEntity residentEntity);
        Task<Guid> DeleteResident(Guid id);
        Task<List<ResidentEntity>> GetFiltered(ResidentsQueryFilterParams residentsQueryFilterParams);
        Task<ResidentEntity?> GetByResidentId(Guid id);
        Task<Guid> UpdateResident(ResidentEntity updatedResidentEntity);
    }
}