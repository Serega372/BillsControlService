namespace BillsControl.Core
{
    public interface IResidentsService
    {
        Task AddResident(Guid id, string name, string surname, Guid personalBillId, bool isOwner = false);
        Task DeleteResident(Guid id);
        Task<List<ResidentDto>> GetAll();
        Task<ResidentDto?> GetByBillId(Guid id);
        Task<List<ResidentDto>> GetByPage(int page, int pageSize);
        Task<ResidentDto?> GetByResidentId(Guid id);
        Task UpdateResident(Guid id, string name, string surname, Guid personalBillId, bool isOwner = false);
    }
}