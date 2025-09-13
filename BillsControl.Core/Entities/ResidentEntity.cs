using System.ComponentModel.DataAnnotations;

namespace BillsControl.Core.Entities
{
    public class ResidentEntity
    {
        public required Guid Id { get; init; } = Guid.NewGuid();
        [Length(10, 10)]
        public string PersonalBillNumber { get; set; } = string.Empty;
        public required string FirstName { get; set; }
        public required string Lastname { get; set; }
        public string? MiddleName { get; set; } = string.Empty;
        public Guid? PersonalBillId { get; set; } = Guid.Empty;
        public PersonalBillEntity? PersonalBill { get; set; }
        public required bool IsOwner { get; set; } = false;
    }
}
