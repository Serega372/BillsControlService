using AutoMapper;
using BillsControl.ApplicationCore.Abstract;
using BillsControl.ApplicationCore.CustomExceptions;
using BillsControl.ApplicationCore.Dtos;
using BillsControl.ApplicationCore.Entities;

namespace BillsControl.ApplicationCore.Services
{
    /// <summary>
    /// Represents a service for managing residents. Implements the business logic related to updating and retrieving data.
    /// Uses <see cref="IResidentsRepository"/> for data access and <see cref="IMapper"/> for model mapping.
    /// </summary>
    /// <param name="residentsRepository">Residents repository.</param>
    /// <param name="personalBillsRepository">Personal bills repository.</param>
    /// <param name="mapper">AutoMapper component for model mapping.</param>
    public class ResidentsService(
        IResidentsRepository residentsRepository,
        IPersonalBillsRepository personalBillsRepository,
        IMapper mapper)
        : IResidentsService
    {
        /// <inheritdoc/>
        public async Task<Guid> AddResident(AddResidentRequest residentDto)
        {
            var residentEntity = mapper.Map<ResidentEntity>(residentDto);
            if (residentEntity.PersonalBillId != null)
            {
                var personalBillEntity = await personalBillsRepository.GetByBillId(residentEntity.PersonalBillId) 
                                         ?? throw new BillNotFoundException(residentEntity.PersonalBillId);
                residentEntity.PersonalBillNumber = personalBillEntity.BillNumber;
            }
            
            await residentsRepository.AddResident(residentEntity);
            return residentEntity.Id;
        }
        
        /// <inheritdoc/>
        public async Task<Guid> DeleteResident(Guid id)
        {
            if (await residentsRepository.GetByResidentId(id) == null)
                throw new ResidentNotFoundException(id);
            await residentsRepository.DeleteResident(id);
            return id;
        }

        /// <inheritdoc/>
        public async Task<List<ResidentsResponse>> GetFiltered(ResidentsQueryFilterParams residentsQueryFilterParams)
        {
            var residentEntities = await residentsRepository.GetFiltered(residentsQueryFilterParams);
            return mapper.Map<List<ResidentsResponse>>(residentEntities);
        }
        
        /// <inheritdoc/>
        public async Task<ResidentsResponse> GetByResidentId(Guid id)
        {
            var residentEntity = await residentsRepository.GetByResidentId(id) 
                                   ?? throw new ResidentNotFoundException(id);
            return mapper.Map<ResidentsResponse>(residentEntity);
        }

        /// <inheritdoc/>
        public async Task<Guid> UpdateResident(Guid id, UpdateResidentRequest updatedResidentDto)
        {
            var currentResidentEntity = await residentsRepository.GetByResidentId(id) 
                                        ?? throw new ResidentNotFoundException(id);
            if (updatedResidentDto.IsOwner.HasValue) currentResidentEntity.IsOwner = updatedResidentDto.IsOwner.Value;
            if (updatedResidentDto.FirstName != null) currentResidentEntity.FirstName = updatedResidentDto.FirstName;
            if (updatedResidentDto.LastName != null) currentResidentEntity.LastName = updatedResidentDto.LastName;
            if (updatedResidentDto.MiddleName != null) currentResidentEntity.MiddleName = updatedResidentDto.MiddleName;
            var personalBillEntity = await personalBillsRepository.GetByBillId(updatedResidentDto.PersonalBillId) 
                                     ?? throw new BillNotFoundException(updatedResidentDto.PersonalBillId);
            if (personalBillEntity.IsClosed)
                throw new BillIsClosedException(updatedResidentDto.PersonalBillId);
            
            if (updatedResidentDto.PersonalBillId != currentResidentEntity.PersonalBillId)
            {
                currentResidentEntity.PersonalBillId = updatedResidentDto.PersonalBillId;
                currentResidentEntity.PersonalBillNumber = personalBillEntity.BillNumber;
            }

            await residentsRepository.UpdateResident(currentResidentEntity);
            return id;
        }
    }
}
