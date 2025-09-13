using BillsControl.Core.Abstract;
using BillsControl.Core.Dtos;
using Microsoft.EntityFrameworkCore;
using BillsControl.Core.Entities;
using BillsControl.DataAccess.SQLite.Extensions;

namespace BillsControl.DataAccess.SQLite.Repositories
{
    public class PersonalBillsRepository(DatabaseContext dbContext) : IPersonalBillsRepository
    {
        public async Task<List<PersonalBillEntity>> GetFiltered(
            PersonalBillsQueryFilterParams personalBillsQueryFilterParams, FullNameDto fullNameDto)
        {
            var (page, pageSize, withResidents, address, billNumber, openDate, _) = personalBillsQueryFilterParams;
            var (lastName, firstName, middleName) = fullNameDto;
            return await dbContext.PersonalBills
                .AsNoTracking()
                .Include(bill => bill.Residents)
                .AsQueryable()
                .ApplyPagination(page, pageSize)
                .FilterByWithResidents(withResidents)
                .FilterByBillNumber(billNumber)
                .FilterByOpenDate(openDate)
                .FilterByAddress(address)
                .FilterByName(firstName, lastName, middleName)
                .ToListAsync();
        }

        public async Task<PersonalBillEntity?> GetByBillId(Guid? id)
        {
            return await dbContext.PersonalBills
                .AsNoTracking()
                .FirstOrDefaultAsync(bill => bill.Id == id);
        }

        public async Task<Guid> AddBill(PersonalBillEntity billEntity)
        {
            await dbContext.PersonalBills.AddAsync(billEntity);
            await dbContext.SaveChangesAsync();
            return billEntity.Id;
        }

        public async Task<Guid> UpdateBill(PersonalBillEntity updatedBillEntity)
        {
            dbContext.PersonalBills.Update(updatedBillEntity);
            await dbContext.SaveChangesAsync();
            return updatedBillEntity.Id;
        }
        
        public async Task<Guid> DeleteBill(Guid id)
        {
            await dbContext.Residents
                .Where(resident => resident.PersonalBillId == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(resident => resident.PersonalBillNumber, string.Empty));
            await dbContext.PersonalBills
                .Where(bill => bill.Id == id)
                .ExecuteDeleteAsync();
            await dbContext.SaveChangesAsync();
            return id;
        }
    }
}
