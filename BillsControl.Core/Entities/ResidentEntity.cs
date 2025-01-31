using System.ComponentModel.DataAnnotations.Schema;

namespace BillsControl.Core
{
    public class ResidentEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public Guid PersonalBillId { get; set; }
        [ForeignKey("PersonalBillId")]
        public PersonalBillEntity? PersonalBill { get; set; }

        public bool IsOwner { get; set; } = false;
    }
}
