using System.Collections.Generic;

namespace BillsControl.Core
{
    public class PersonalBillEntity
    {
        public Guid Id { get; set; }

        public string BillNumber { get; set; } = string.Empty;

        public DateTime OpenTime { get; set; }

        public DateTime CloseTime { get; set; }

        public string Address { get; set; } = string.Empty;

        public float PlaceArea { get; set; } = 0;

        public virtual List<ResidentEntity>? Residents { get; set; } = [];
    }
}
