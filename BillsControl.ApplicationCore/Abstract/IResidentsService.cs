using BillsControl.ApplicationCore.CustomExceptions;
using BillsControl.ApplicationCore.Dtos;

namespace BillsControl.ApplicationCore.Abstract
{
    /// <summary>
    /// Represents business logic operations for residents.
    /// </summary>
    public interface IResidentsService
    {
        /// <summary>
        /// Adds a resident.
        /// </summary>
        /// <param name="residentDto">Resident DTO that will be added.</param>
        /// <returns>The id of the added resident.</returns>
        /// <exception cref="BillNotFoundException">Thrown if the personal bill specified by the resident is not found.</exception>
        Task<Guid> AddResident(AddResidentRequest residentDto);
        
        /// <summary>
        /// Delete resident from by his id.
        /// </summary>
        /// <param name="id">Resident id.</param>
        /// <returns>The id of the deleted resident.</returns>
        /// <exception cref="ResidentNotFoundException">Thrown if the resident not found for delete.</exception>
        Task<Guid> DeleteResident(Guid id);

        /// <summary>
        /// Gets a list of residents with possible filtering and pagination.
        /// </summary>
        /// <param name="residentsQueryFilterParams">DTO with pagination data and filter params.</param>
        /// <returns>A filtered and/or paginated collection of DTO <see cref="ResidentsResponse"/>.
        /// If no filtering is performed, all residents are returned.</returns>
        Task<List<ResidentsResponse>> GetFiltered(ResidentsQueryFilterParams residentsQueryFilterParams);
        
        /// <summary>
        /// Gets a resident DTO by his id.
        /// </summary>
        /// <param name="id">Resident id.</param>
        /// <returns>The found DTO <see cref="ResidentsResponse"/>.</returns>
        /// <exception cref="ResidentNotFoundException">Thrown if the resident not found.</exception>
        Task<ResidentsResponse> GetByResidentId(Guid id);
        
        /// <summary>
        /// Updates resident data partially or completely.
        /// </summary>
        /// <param name="id">Resident id.</param>
        /// <param name="updatedResidentDto">Resident DTO that will be updated.</param>
        /// <returns>The id of the updated resident.</returns>
        /// <exception cref="ResidentNotFoundException">Thrown if the resident not found for update.</exception>
        /// <exception cref="BillNotFoundException">Thrown if the personal bill specified by the resident is not found.</exception>
        /// <exception cref="BillIsClosedException">Thrown if the bill is closed.</exception>
        Task<Guid> UpdateResident(Guid id, UpdateResidentRequest updatedResidentDto);
    }
}