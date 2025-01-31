using BillsControl.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsControl.DataAccess.SQLite.Repositories
{
    public class ResidentsRepository : IResidentsRepository
    {
        private readonly DatabaseContext _dbContext;

        public ResidentsRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ResidentEntity>> GetAll()
        {
            return await _dbContext.Residents
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ResidentEntity?> GetByResidentId(Guid id)
        {
            return await _dbContext.Residents
                .AsNoTracking()
                .FirstOrDefaultAsync(resident => resident.Id == id);
        }

        public async Task<ResidentEntity?> GetByBillId(Guid id)
        {
            return await _dbContext.Residents
                .AsNoTracking()
                .FirstOrDefaultAsync(resident => resident.PersonalBillId == id);
        }

        public async Task<List<ResidentEntity>> GetByPage(int page, int pageSize)
        {
            return await _dbContext.Residents
                .AsNoTracking()
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task AddResident(
            Guid id, string name, string surname, 
            Guid personalBillId, bool isOwner = false)
        {
            var residentEntity = new ResidentEntity
            {
                Id = id,
                Name = name,
                Surname = surname,
                PersonalBillId = personalBillId,
                IsOwner = isOwner,
            };

            await _dbContext.AddAsync(residentEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateResident(
            Guid id, string name, string surname,
            Guid personalBillId, bool isOwner = false)
        {
            await _dbContext.Residents
                .Where(resident => resident.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(resident => resident.Id, id)
                .SetProperty(resident => resident.Name, name)
                .SetProperty(resident => resident.Surname, surname)
                .SetProperty(resident => resident.PersonalBillId, personalBillId)
                .SetProperty(resident => resident.IsOwner, isOwner));

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteResident(Guid id)
        {
            await _dbContext.Residents
                .Where(resident => resident.Id == id)
                .ExecuteDeleteAsync();

            await _dbContext.SaveChangesAsync();
        }
    }
}
