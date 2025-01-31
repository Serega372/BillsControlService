namespace BillsControl.Core
{
    public interface IPersonalBillsRepository
    {
        Task AddBill(Guid id, string billNumber, string address, float placeArea, List<ResidentEntity>? residents);
        Task DeleteBill(Guid id);
        Task<List<PersonalBillEntity>> GetAll();
        Task<PersonalBillEntity?> GetByAddress(string address);
        Task<PersonalBillEntity?> GetByBillId(Guid id);
        Task<PersonalBillEntity?> GetByBillNumber(string number);
        Task<List<PersonalBillEntity>> GetByOpenDate(DateTime openTime);
        Task<List<PersonalBillEntity>> GetByPage(int page, int pageSize);
        Task<List<PersonalBillEntity>> GetOnlyWithResidents();
        Task UpdateBill(Guid id, string billNumber, string address, float placeArea);
    }
}