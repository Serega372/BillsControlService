using BillsControl.ApplicationCore.Dtos;
using BillsControl.ApplicationCore.Entities;

namespace BillsControl.ApplicationCore.Abstract
{
    /// <summary>
    /// Represents data access operations for residents.
    /// </summary>
    public interface IResidentsRepository
    {
        /// <summary>
        /// Adds a resident to the database.
        /// </summary>
        /// <param name="residentEntity">Resident entity that will be added</param>
        /// <returns>The id of the added resident.</returns>
        Task<Guid> AddResident(ResidentEntity residentEntity);
        
        /// <summary>
        /// Delete resident from database by his id.
        /// </summary>
        /// <param name="id">Resident id.</param>
        /// <returns>The id of the deleted resident.</returns>
        Task<Guid> DeleteResident(Guid id);
        
        /// <summary>
        /// Gets a list of residents with possible filtering and pagination.
        /// </summary>
        /// <param name="residentsQueryFilterParams">DTO with pagination data and filter params.</param>
        /// <returns>A filtered and/or paginated collection of residents. If no filtering is performed, all residents are returned.</returns>
        Task<IEnumerable<ResidentEntity>> GetFiltered(ResidentsQueryFilterParams residentsQueryFilterParams);
        
        /// <summary>
        /// Gets a resident by his id.
        /// </summary>
        /// <param name="id">Resident id.</param>
        /// <returns>The found resident. If the resident is not found, it returns null.</returns>
        Task<ResidentEntity?> GetByResidentId(Guid id);
        
        /// <summary>
        /// Updates resident data partially or completely.
        /// </summary>
        /// <param name="updatedResidentEntity">Resident entity that will be updated.</param>
        /// <returns>The id of the updated resident.</returns>
        Task<Guid> UpdateResident(ResidentEntity updatedResidentEntity);
    }
}