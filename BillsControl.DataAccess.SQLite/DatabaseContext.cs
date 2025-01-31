using BillsControl.Core;
using Microsoft.EntityFrameworkCore;

namespace BillsControl.DataAccess.SQLite
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

        public DbSet<PersonalBillEntity> PersonalBills { get; set; }

        public DbSet<ResidentEntity> Residents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.ApplyConfiguration(new PersonalBillConfiguration());
            //modelBuilder.ApplyConfiguration(new ResidentConfiguration());

            modelBuilder.Entity<ResidentEntity>()
                .HasOne(resident => resident.PersonalBill)
                .WithMany(bill => bill.Residents)
                .HasForeignKey(resident => resident.PersonalBillId);

            //base.OnModelCreating(modelBuilder);
        }
    }
}
