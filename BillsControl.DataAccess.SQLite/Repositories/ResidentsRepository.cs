using BillsControl.Core.Abstract;
using BillsControl.Core.Dtos;
using BillsControl.Core.Entities;
using BillsControl.DataAccess.SQLite.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BillsControl.DataAccess.SQLite.Repositories
{
    public class ResidentsRepository(DatabaseContext dbContext) : IResidentsRepository
    {
        public async Task<List<ResidentEntity>> GetFiltered(ResidentsQueryFilterParams residentsQueryFilterParams)
        {
            var (page, pageSize, billId, billNumber) = residentsQueryFilterParams;
            return await dbContext.Residents
                .AsNoTracking()
                .AsQueryable()
                .ApplyPagination(page, pageSize)
                .FilterByBillId(billId)
                .FilterByBillNumber(billNumber)
                .ToListAsync();
        }

        public async Task<ResidentEntity?> GetByResidentId(Guid id)
        {
            return await dbContext.Residents
                .AsNoTracking()
                .FirstOrDefaultAsync(resident => resident.Id == id);
        }

        public async Task<Guid> AddResident(ResidentEntity residentEntity)
        {
            await dbContext.AddAsync(residentEntity);
            await dbContext.SaveChangesAsync();
            return residentEntity.Id;
        }

        public async Task<Guid> UpdateResident(ResidentEntity updatedResidentEntity)
        {
            dbContext.Residents.Update(updatedResidentEntity);
            await dbContext.SaveChangesAsync();
            return updatedResidentEntity.Id;
        }

        public async Task<Guid> DeleteResident(Guid id)
        {
            await dbContext.Residents
                .Where(resident => resident.Id == id)
                .ExecuteDeleteAsync();
            await dbContext.SaveChangesAsync();
            return id;
        }
    }
}
