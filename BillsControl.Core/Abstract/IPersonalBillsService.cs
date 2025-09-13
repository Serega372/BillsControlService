using BillsControl.Core.Dtos;

namespace BillsControl.Core.Abstract
{
    public interface IPersonalBillsService
    {
        Task<Guid> AddBill(AddPersonalBillRequest billDto);
        Task<Guid> DeleteBill(Guid id);
        Task<Guid> CloseBill(Guid id);
        Task<List<PersonalBillsResponse>> GetFiltered(PersonalBillsQueryFilterParams personalBillsQueryFilterParams);
        Task<PersonalBillsResponse> GetByBillId(Guid id);
        Task<Guid> UpdateBill(Guid id, UpdatePersonalBillRequest updatedBillDto);
    }
}