using BillsControl.ApplicationCore.Dtos;
using BillsControl.ApplicationCore.Entities;

namespace BillsControl.ApplicationCore.Abstract
{
    /// <summary>
    /// Represents data access operations for personal bills.
    /// </summary>
    public interface IPersonalBillsRepository
    {
        /// <summary>
        /// Adds a personal bill to the database.
        /// </summary>
        /// <param name="billEntity">Personal bill entity that will be added</param>
        /// <returns>The id of the added bill.</returns>
        Task<Guid> AddBill(PersonalBillEntity billEntity);
        
        /// <summary>
        /// Delete personal bill from database by his id.
        /// </summary>
        /// <param name="id">Personal bill id.</param>
        /// <returns>The id of the deleted bill.</returns>
        Task<Guid> DeleteBill(Guid id);
        
        /// <summary>
        /// Gets a list of personal bills from database with possible filtering and pagination.
        /// </summary>
        /// <param name="personalBillsQueryFilterParams">DTO with pagination data and filter params.</param>
        /// <param name="fullNameDto">DTO with optional resident initials.</param>
        /// <returns>A filtered and/or paginated collection of <see cref="PersonalBillEntity"/>.
        /// If no filtering is performed, all bills are returned.
        /// </returns>
        Task<IEnumerable<PersonalBillEntity>> GetFiltered(PersonalBillsQueryFilterParams personalBillsQueryFilterParams, FullNameDto fullNameDto);
        
        /// <summary>
        /// Gets a personal bill from database by his id.
        /// </summary>
        /// <param name="id">Personal bill id.</param>
        /// <returns>The found <see cref="PersonalBillEntity"/>. If the bill is not found, it returns null.</returns>
        Task<PersonalBillEntity?> GetByBillId(Guid? id);
        
        /// <summary>
        /// Updates personal bill data partially or completely.
        /// </summary>
        /// <param name="updatedBillEntity">Personal bill entity that will be updated.</param>
        /// <returns>The id of the updated bill.</returns>
        Task<Guid> UpdateBill(PersonalBillEntity updatedBillEntity);
    }
}