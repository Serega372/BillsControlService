using AutoMapper;
using BillsControl.Core;

namespace BillsControl.Application
{
    public class PersonalBillsService : IPersonalBillsService
    {
        private readonly IPersonalBillsRepository _personalBillsRepository;
        private readonly IMapper _mapper;

        public PersonalBillsService(
            IPersonalBillsRepository personalBillsRepository,
            IMapper mapper)
        {
            _personalBillsRepository = personalBillsRepository;
            _mapper = mapper;
        }

        public async Task AddBill(Guid id, string billNumber, string address, float placeArea,
            List<ResidentDto>? residents)
        {
            var _residents = _mapper.Map<List<ResidentEntity>>(residents);

            await _personalBillsRepository.AddBill(id, billNumber, address, placeArea, _residents);
        }

        public async Task DeleteBill(Guid id)
        {
            await _personalBillsRepository.DeleteBill(id);
        }

        public async Task<List<PersonalBillsResponse>> GetAll()
        {
            var billEntities = await _personalBillsRepository.GetAll();

            return billEntities.Select(MapEntityToDto).ToList();
        }

        public async Task<PersonalBillsResponse?> GetByAddress(string address)
        {
            var billEntities = await _personalBillsRepository.GetByAddress(address);

            return MapEntityToDto(billEntities);
        }

        public async Task<PersonalBillsResponse?> GetByBillId(Guid id)
        {
            var billEntities = await _personalBillsRepository.GetByBillId(id);

            return MapEntityToDto(billEntities);
        }

        public async Task<PersonalBillsResponse?> GetByBillNumber(string number)
        {
            var billEntities = await _personalBillsRepository.GetByBillNumber(number);

            return MapEntityToDto(billEntities);
        }

        public async Task<List<PersonalBillsResponse>> GetByOpenDate(DateTime openTime)
        {
            var billEntities = await _personalBillsRepository.GetByOpenDate(openTime);

            return billEntities.Select(MapEntityToDto).ToList();
        }

        public async Task<List<PersonalBillsResponse>> GetByPage(int page, int pageSize)
        {
            var billEntities = await _personalBillsRepository.GetByPage(page, pageSize);

            return billEntities.Select(MapEntityToDto).ToList();
        }

        public async Task<List<PersonalBillsResponse>> GetOnlyWithResidents()
        {
            var billEntities = await _personalBillsRepository.GetOnlyWithResidents();

            return billEntities.Select(MapEntityToDto).ToList();
        }

        public async Task UpdateBill(Guid id, string billNumber, string address, float placeArea)
        {
            await _personalBillsRepository
                .UpdateBill(id, billNumber, address, placeArea);
        }

        private PersonalBillsResponse MapEntityToDto(PersonalBillEntity bill)
        {
            return new PersonalBillsResponse(
                bill.Id,
                bill.BillNumber,
                bill.OpenTime,
                bill.CloseTime,
                bill.Address,
                bill.PlaceArea,
                bill.Residents?.Select(resident => new ResidentDto(
                    resident.Id,
                    resident.Name,
                    resident.Surname,
                    resident.PersonalBillId,
                    resident.IsOwner)
                ).ToList());
        }
    }
}
