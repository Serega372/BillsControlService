using AutoMapper;
using BillsControl.ApplicationCore.Abstract;
using BillsControl.ApplicationCore.CustomExceptions;
using BillsControl.ApplicationCore.Dtos;
using BillsControl.ApplicationCore.Entities;

namespace BillsControl.ApplicationCore.Services
{
    /// <summary>
    /// Represents a service for managing personal bills. Implements the business logic related to updating and retrieving data.
    /// Uses <see cref="IPersonalBillsRepository"/> for data access and <see cref="IMapper"/> for model mapping.
    /// </summary>
    /// <param name="personalBillsRepository">Personal bills repository.</param>
    /// <param name="mapper">AutoMapper component for model mapping.</param>
    public class PersonalBillsService(
        IPersonalBillsRepository personalBillsRepository,
        IMapper mapper) 
        : IPersonalBillsService
    {
        /// <inheritdoc/>
        public async Task<Guid> AddBill(AddPersonalBillRequest billDto)
        {
            var billEntity = mapper.Map<PersonalBillEntity>(billDto);
            await personalBillsRepository.AddBill(billEntity);
            return billEntity.Id;
        }

        /// <inheritdoc/>
        public async Task<Guid> DeleteBill(Guid id)
        {
            if (await personalBillsRepository.GetByBillId(id) == null) 
                throw new BillNotFoundException(id);
            await personalBillsRepository.DeleteBill(id);
            return id;
        }

        /// <inheritdoc/>
        public async Task<List<PersonalBillsResponse>> GetFiltered(PersonalBillsQueryFilterParams personalBillsQueryFilterParams)
        {
            var splitFullName = personalBillsQueryFilterParams.FullName?.Split();
            var fullNameDto = new FullNameDto(
                splitFullName?.Length >= 1 ? splitFullName[0] : null, 
                splitFullName?.Length >= 2 ? splitFullName[1] : null,
                splitFullName?.Length == 3 ? splitFullName[2] : null);
            var billEntities = await personalBillsRepository.GetFiltered(personalBillsQueryFilterParams, fullNameDto);
            return mapper.Map<List<PersonalBillsResponse>>(billEntities);
        }

        /// <inheritdoc/>
        public async Task<PersonalBillsResponse> GetByBillId(Guid id)
        {
            var billEntity = await personalBillsRepository.GetByBillId(id) 
                               ??  throw new BillNotFoundException(id);
            return mapper.Map<PersonalBillsResponse>(billEntity);
        }

        /// <inheritdoc/>
        public async Task<Guid> UpdateBill(Guid id, UpdatePersonalBillRequest updatedBillDto)
        {
            var currentBill = await personalBillsRepository.GetByBillId(id) 
                              ?? throw new BillNotFoundException(id);
            if (currentBill.IsClosed)
                throw new BillIsClosedException(id);
            
            if (updatedBillDto.CloseDate.HasValue && updatedBillDto.CloseDate < currentBill.OpenDate)
                throw new InvalidCloseDateInBillException(currentBill.OpenDate, updatedBillDto.CloseDate.Value);
            
            currentBill.Address = updatedBillDto.Address;
            currentBill.PlaceArea = updatedBillDto.PlaceArea.Value;
            if (updatedBillDto.IsClosed.HasValue && updatedBillDto.IsClosed.Value)
            {
                currentBill.IsClosed = true;
                currentBill.CloseDate = updatedBillDto.CloseDate ?? DateOnly.FromDateTime(DateTime.UtcNow.ToLocalTime());
            }
            
            await personalBillsRepository.UpdateBill(currentBill);
            return id;
        }

        /// <inheritdoc/>
        public async Task<Guid> CloseBill(Guid id)
        {
            var billEntity = await personalBillsRepository.GetByBillId(id) 
                             ?? throw new BillNotFoundException(id);
            if (billEntity.IsClosed)
                throw new BillAlreadyClosedException(id);
            
            billEntity.IsClosed = true;
            billEntity.CloseDate = DateOnly.FromDateTime(DateTime.UtcNow.ToLocalTime());
            await personalBillsRepository.UpdateBill(billEntity);
            return id;
        }
    }
}
