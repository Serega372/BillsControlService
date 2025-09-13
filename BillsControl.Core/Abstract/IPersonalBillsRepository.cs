using BillsControl.Core.Dtos;
using BillsControl.Core.Entities;

namespace BillsControl.Core.Abstract
{
    public interface IPersonalBillsRepository
    {
        Task<Guid> AddBill(PersonalBillEntity billEntity);
        Task<Guid> DeleteBill(Guid id);
        Task<List<PersonalBillEntity>> GetFiltered(PersonalBillsQueryFilterParams personalBillsQueryFilterParams, FullNameDto fullNameDto);
        Task<PersonalBillEntity?> GetByBillId(Guid? id);
        Task<Guid> UpdateBill(PersonalBillEntity updatedBillEntity);
    }
}