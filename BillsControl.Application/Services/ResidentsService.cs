using AutoMapper;
using BillsControl.Core.Abstract;
using BillsControl.Core.Dtos;
using BillsControl.Core.Entities;

namespace BillsControl.Application.Services
{
    public class ResidentsService(
        IResidentsRepository residentsRepository,
        IPersonalBillsRepository personalBillsRepository,
        IMapper mapper)
        : IResidentsService
    {
        public async Task<Guid> AddResident(AddResidentRequest residentDto)
        {
            var residentEntity = mapper.Map<ResidentEntity>(residentDto);
            await residentsRepository.AddResident(residentEntity);
            return residentEntity.Id;
        }

        public async Task<Guid> DeleteResident(Guid id)
        {
            var residentEntity = await residentsRepository.GetByResidentId(id) 
                               ?? throw new Exception($"Resident with id \"{id}\" not found");
            await residentsRepository.DeleteResident(id);
            return id;
        }

        public async Task<List<ResidentsResponse>> GetFiltered(ResidentsQueryFilterParams residentsQueryFilterParams)
        {
            var residentEntities = await residentsRepository.GetFiltered(residentsQueryFilterParams) 
                                   ?? throw new Exception("Residents not found");
            return mapper.Map<List<ResidentsResponse>>(residentEntities);
        }

        public async Task<ResidentsResponse?> GetByResidentId(Guid id)
        {
            var residentEntity = await residentsRepository.GetByResidentId(id) 
                                   ?? throw new Exception($"Resident with id \"{id}\" not found");
            return mapper.Map<ResidentsResponse>(residentEntity);
        }

        public async Task<Guid> UpdateResident(Guid id, UpdateResidentRequest updatedResidentDto)
        {
            var currentResidentEntity = await residentsRepository.GetByResidentId(id) 
                                        ?? throw new Exception($"Resident with id \"{id}\" not found for update");
            if (updatedResidentDto.IsOwner.HasValue) currentResidentEntity.IsOwner = updatedResidentDto.IsOwner.Value;
            if (updatedResidentDto.FirstName != null) currentResidentEntity.FirstName = updatedResidentDto.FirstName;
            if (updatedResidentDto.LastName != null) currentResidentEntity.Lastname = updatedResidentDto.LastName;
            if (updatedResidentDto.MiddleName != null) currentResidentEntity.MiddleName = updatedResidentDto.MiddleName;
            var personalBillEntity = await personalBillsRepository.GetByBillId(updatedResidentDto.PersonalBillId) 
                                     ?? throw new Exception($"Bill with id \"{updatedResidentDto.PersonalBillId}\" not found");
            if (personalBillEntity.IsClosed) 
                throw new Exception($"Bill with id \"{updatedResidentDto.PersonalBillId}\" is closed, adding resident forbidden");
            
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
