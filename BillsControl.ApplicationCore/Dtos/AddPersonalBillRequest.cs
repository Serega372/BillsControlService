using System.ComponentModel.DataAnnotations;

namespace BillsControl.ApplicationCore.Dtos
{
    public record AddPersonalBillRequest(
        [Length(10, 10)] string BillNumber,
        string? Address,
        float? PlaceArea);
}
