namespace BillsControl.Core.Dtos
{
    public record UpdatePersonalBillRequest(
        string? Address,
        DateOnly? CloseDate,
        bool? IsClosed,
        float? PlaceArea);
}
