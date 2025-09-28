using BillsControl.ApplicationCore.CustomExceptions;
using BillsControl.ApplicationCore.Dtos;

namespace BillsControl.ApplicationCore.Abstract
{
    /// <summary>
    /// Represents business logic operations for personal bills.
    /// </summary>
    public interface IPersonalBillsService
    {
        /// <summary>
        /// Adds a personal bill.
        /// </summary>
        /// <param name="billDto">Personal bill DTO that will be added</param>
        /// <returns>The id of the added bill.</returns>
        Task<Guid> AddBill(AddPersonalBillRequest billDto);
        
        /// <summary>
        /// Delete personal bill by his id.
        /// </summary>
        /// <param name="id">Personal bill id.</param>
        /// <returns>The id of the deleted bill.</returns>
        /// <exception cref="BillNotFoundException">Thrown if the bill not found for delete</exception>
        Task<Guid> DeleteBill(Guid id);
        
        /// <summary>
        /// Close personal bill by his id.
        /// </summary>
        /// <param name="id">Personal bill id.</param>
        /// <returns>The id of the closed bill.</returns>
        /// <exception cref="BillNotFoundException">Thrown if the bill not found for close.</exception>
        /// <exception cref="BillAlreadyClosedException">Thrown if the bill already closed.</exception>
        Task<Guid> CloseBill(Guid id);
        
        /// <summary>
        /// Gets a list of personal bills with possible filtering and pagination.
        /// </summary>
        /// <param name="personalBillsQueryFilterParams">DTO with pagination data and filter params.</param>
        /// <returns>A filtered and/or paginated collection of DTO <see cref="PersonalBillsResponse"/>.
        /// If no filtering is performed, all bills are returned.</returns>
        Task<List<PersonalBillsResponse>> GetFiltered(PersonalBillsQueryFilterParams personalBillsQueryFilterParams);
        
        /// <summary>
        /// Gets a personal bill DTO by his id.
        /// </summary>
        /// <param name="id">Personal bill id.</param>
        /// <returns>The found DTO <see cref="PersonalBillsResponse"/>.</returns>
        /// <exception cref="BillNotFoundException">Thrown if the bill not found.</exception>
        Task<PersonalBillsResponse> GetByBillId(Guid id);
        
        /// <summary>
        /// Updates personal bill partially or completely.
        /// </summary>
        /// <param name="updatedBillDto">Personal bill DTO that will be updated.</param>
        /// <param name="id">Personal bill id.</param>
        /// <returns>The id of the updated bill.</returns>
        /// <exception cref="BillNotFoundException">Thrown if the bill not found for update</exception>
        /// <exception cref="BillIsClosedException">Thrown if the bill is closed</exception>
        /// <exception cref="InvalidCloseDateInBillException">Thrown if the bill close date earlier than open date</exception>
        Task<Guid> UpdateBill(Guid id, UpdatePersonalBillRequest updatedBillDto);
    }
}