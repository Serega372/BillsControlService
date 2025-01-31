namespace BillsControl.Core
{
    public interface IResidentsRepository
    {
        Task AddResident(Guid id, string name, string surname, Guid personalBillId, bool isOwner = false);
        Task DeleteResident(Guid id);
        Task<List<ResidentEntity>> GetAll();
        Task<ResidentEntity?> GetByBillId(Guid id);
        Task<List<ResidentEntity>> GetByPage(int page, int pageSize);
        Task<ResidentEntity?> GetByResidentId(Guid id);
        Task UpdateResident(Guid id, string name, string surname, Guid personalBillId, bool isOwner = false);
    }
}