using System.ComponentModel.DataAnnotations;

namespace BillsControl.Core.Dtos
{
    public record AddPersonalBillRequest(
        [Length(10, 10)] string BillNumber,
        string? Address,
        float? PlaceArea);
}
