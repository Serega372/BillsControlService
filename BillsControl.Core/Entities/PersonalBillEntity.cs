using System.ComponentModel.DataAnnotations;

namespace BillsControl.Core.Entities
{
    public class PersonalBillEntity
    {
        public required Guid Id { get; init; } = Guid.NewGuid();
        [Length(10, 10)]
        public required string BillNumber { get; init; }
        public required DateOnly OpenDate { get; init; } = DateOnly.FromDateTime(DateTime.UtcNow.ToLocalTime());
        public DateOnly? CloseDate { get; set; }
        public bool IsClosed { get; set; } = false;
        public required string Address { get; set; } = string.Empty;
        public required float PlaceArea { get; set; }
        public ICollection<ResidentEntity>? Residents { get; set; }
    }
}
