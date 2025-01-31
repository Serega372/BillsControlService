using AutoMapper;
using BillsControl.Core;

namespace BillsControl.Application
{
    public class ResidentsService : IResidentsService
    {
        private readonly IResidentsRepository _residentsRepository;
        private readonly IMapper _mapper;

        public ResidentsService(
            IResidentsRepository residentsRepository,
            IMapper mapper)
        {
            _residentsRepository = residentsRepository;
            _mapper = mapper;
        }

        public async Task AddResident(Guid id, string name, string surname,
            Guid personalBillId, bool isOwner = false)
        {
            await _residentsRepository.AddResident(id, name, surname, personalBillId, isOwner);
        }

        public async Task DeleteResident(Guid id)
        {
            await _residentsRepository.DeleteResident(id);
        }

        public async Task<List<ResidentDto>> GetAll()
        {
            var residents = await _residentsRepository.GetAll();

            return residents.Select(MapEntityToDto).ToList();
        }

        public async Task<ResidentDto?> GetByBillId(Guid id)
        {
            var resident = await _residentsRepository.GetByBillId(id);

            return MapEntityToDto(resident);
        }

        public async Task<List<ResidentDto>> GetByPage(int page, int pageSize)
        {
            var residents = await _residentsRepository.GetByPage(page, pageSize);

            return residents.Select(MapEntityToDto).ToList();
        }

        public async Task<ResidentDto?> GetByResidentId(Guid id)
        {
            var resident =  await _residentsRepository.GetByResidentId(id);

            return MapEntityToDto(resident);
        }

        public async Task UpdateResident(Guid id, string name, string surname,
            Guid personalBillId, bool isOwner = false)
        {
            await _residentsRepository
                .UpdateResident(id, name, surname, personalBillId, isOwner);
        }

        private ResidentDto MapEntityToDto(ResidentEntity residentEntity)
        {
            return new ResidentDto(
                residentEntity.Id,
                residentEntity.Name,
                residentEntity.Surname,
                residentEntity.PersonalBillId,
                residentEntity.IsOwner);
        }
    }
}
