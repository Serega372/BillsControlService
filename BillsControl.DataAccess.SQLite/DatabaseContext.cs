using BillsControl.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillsControl.DataAccess.SQLite
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {
        public DbSet<PersonalBillEntity> PersonalBills { get; set; }
        public DbSet<ResidentEntity> Residents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<ResidentEntity>()
                .HasOne(resident => resident.PersonalBill)
                .WithMany(bill => bill.Residents)
                .HasForeignKey(resident => resident.PersonalBillId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<PersonalBillEntity>()
                .HasMany(bill => bill.Residents)
                .WithOne(resident => resident.PersonalBill)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
