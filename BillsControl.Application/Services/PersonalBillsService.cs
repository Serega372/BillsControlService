using AutoMapper;
using BillsControl.Core.Abstract;
using BillsControl.Core.Dtos;
using BillsControl.Core.Entities;

namespace BillsControl.Application.Services
{
    public class PersonalBillsService(
        IPersonalBillsRepository personalBillsRepository,
        IResidentsRepository residentsRepository,
        IMapper mapper) 
        : IPersonalBillsService
    {
        public async Task<Guid> AddBill(AddPersonalBillRequest billDto)
        {
            var billEntity = mapper.Map<PersonalBillEntity>(billDto);
            await personalBillsRepository.AddBill(billEntity);
            return billEntity.Id;
        }

        public async Task<Guid> DeleteBill(Guid id)
        {
            var personalBill = await personalBillsRepository.GetByBillId(id) 
                               ?? throw new Exception($"Bill with id \"{id}\" not found");
            await personalBillsRepository.DeleteBill(id);
            return id;
        }

        public async Task<List<PersonalBillsResponse>> GetFiltered(PersonalBillsQueryFilterParams personalBillsQueryFilterParams)
        {
            var splitFullName = personalBillsQueryFilterParams.FullName?.Split();
            var fullNameDto = new FullNameDto(
                splitFullName?.Length >= 1 ? splitFullName[0] : null, 
                splitFullName?.Length >= 2 ? splitFullName[1] : null,
                splitFullName?.Length == 3 ? splitFullName[2] : null);
            var billEntities = await personalBillsRepository.GetFiltered(personalBillsQueryFilterParams, fullNameDto) 
                               ?? throw new Exception("Bills not found");
            return mapper.Map<List<PersonalBillsResponse>>(billEntities);
        }

        public async Task<PersonalBillsResponse> GetByBillId(Guid id)
        {
            var billEntity = await personalBillsRepository.GetByBillId(id) 
                               ??  throw new Exception($"Bill with id \"{id}\" not found");
            return mapper.Map<PersonalBillsResponse>(billEntity);
        }

        public async Task<Guid> UpdateBill(Guid id, UpdatePersonalBillRequest updatedBillDto)
        {
            var currentBill = await personalBillsRepository.GetByBillId(id) 
                              ?? throw new Exception($"Bill with id \"{id}\" not found for update");
            if (currentBill.IsClosed)
                throw new Exception($"Bill with id \"{id}\" is closed");
            
            currentBill.Address = updatedBillDto.Address;
            currentBill.PlaceArea = updatedBillDto.PlaceArea.Value;
            if (updatedBillDto.IsClosed == true)
            {
                currentBill.IsClosed = true;
                currentBill.CloseDate = updatedBillDto.CloseDate ?? DateOnly.FromDateTime(DateTime.UtcNow.ToLocalTime());
            }
            
            await personalBillsRepository.UpdateBill(currentBill);
            return id;
        }

        public async Task<Guid> CloseBill(Guid id)
        {
            var billEntity = await personalBillsRepository.GetByBillId(id) 
                             ?? throw new Exception($"Bill with id \"{id}\" not found for close");
            if (billEntity.IsClosed)
                throw new Exception($"Bill with id \"{id}\" is already closed");
            
            billEntity.IsClosed = true;
            billEntity.CloseDate = DateOnly.FromDateTime(DateTime.UtcNow.ToLocalTime());
            await personalBillsRepository.UpdateBill(billEntity);
            return id;
        }
    }
}
