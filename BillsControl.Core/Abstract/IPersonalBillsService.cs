
namespace BillsControl.Core
{
    public interface IPersonalBillsService
    {
        Task AddBill(Guid id, string billNumber, string address, float placeArea, List<ResidentDto>? residents);
        Task DeleteBill(Guid id);
        Task<List<PersonalBillsResponse>> GetAll();
        Task<PersonalBillsResponse?> GetByAddress(string address);
        Task<PersonalBillsResponse?> GetByBillId(Guid id);
        Task<PersonalBillsResponse?> GetByBillNumber(string number);
        Task<List<PersonalBillsResponse>> GetByOpenDate(DateTime openTime);
        Task<List<PersonalBillsResponse>> GetByPage(int page, int pageSize);
        Task<List<PersonalBillsResponse>> GetOnlyWithResidents();
        Task UpdateBill(Guid id, string billNumber, string address, float placeArea);
    }
}