using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using BillsControl.Core;
using System.Runtime.InteropServices;

namespace BillsControl.DataAccess.SQLite.Repositories
{
    public class PersonalBillsRepository : IPersonalBillsRepository
    {
        private readonly DatabaseContext _dbContext;

        public PersonalBillsRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PersonalBillEntity>> GetAll()
        {
            return await _dbContext.PersonalBills
                .AsNoTracking()
                .Include(bill => bill.Residents)
                .ToListAsync();
        }

        public async Task<List<PersonalBillEntity>> GetOnlyWithResidents()
        {
            return await _dbContext.PersonalBills
                .AsNoTracking()
                .Where(bill => bill.Residents.Count() > 0)
                .Include(bill => bill.Residents)
                .ToListAsync();
        }

        public async Task<PersonalBillEntity?> GetByBillId(Guid id)
        {
            return await _dbContext.PersonalBills
                .AsNoTracking()
                .Include(bill => bill.Residents)
                .FirstOrDefaultAsync(bill => bill.Id == id);
        }
        public async Task<PersonalBillEntity?> GetByBillNumber(string number)
        {
            return await _dbContext.PersonalBills
                .AsNoTracking()
                .Include(bill => bill.Residents)
                .FirstOrDefaultAsync(bill => bill.BillNumber == number);
        }

        public async Task<List<PersonalBillEntity>> GetByOpenDate(DateTime openTime)
        {
            return await _dbContext.PersonalBills
                .AsNoTracking()
                .Include(bill => bill.Residents)
                .Where(bill => bill.OpenTime
                .Equals(openTime))
                .ToListAsync();
        }

        public async Task<PersonalBillEntity?> GetByAddress(string address)
        {
            return await _dbContext.PersonalBills
                .AsNoTracking()
                .Include(bill => bill.Residents)
                .FirstOrDefaultAsync(bill => bill.Address
                .Contains(address));
        }

        public async Task<List<PersonalBillEntity>> GetByPage(int page, int pageSize)
        {
            return await _dbContext.PersonalBills
                .AsNoTracking()
                .Include(bill => bill.Residents)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task AddBill(
            Guid id, string billNumber, string address, float placeArea,
            List<ResidentEntity>? residents)
        {
            var personalBillEntity = new PersonalBillEntity
            {
                Id = id,
                BillNumber = billNumber,
                Address = address,
                OpenTime = DateTime.Now,
                PlaceArea = placeArea,
                Residents = residents,
            };

            await _dbContext.AddAsync(personalBillEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateBill(
            Guid id, string billNumber, string address, float placeArea)
        {
            await _dbContext.PersonalBills
                .Where(bill => bill.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(bill => bill.BillNumber, billNumber)
                .SetProperty(bill => bill.Address, address)
                .SetProperty(bill => bill.PlaceArea, placeArea));

            await _dbContext.SaveChangesAsync();
            //var personalBillEntity = await _dbContext.PersonalBills
            //    .FirstOrDefaultAsync(bill => bill.Id == id)
            //    ?? throw new Exception();

            //personalBillEntity.BillNumber = billNumber;
            //personalBillEntity.Address = address;
            //personalBillEntity.PlaceArea = placeArea;
            //personalBillEntity.Residents = residents;

            //await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBill(Guid id)
        {
            await _dbContext.PersonalBills
                .Where(bill => bill.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
