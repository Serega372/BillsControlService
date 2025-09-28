namespace BillsControl.ApplicationCore.Dtos
{
    public record UpdatePersonalBillRequest(
        string? Address,
        DateOnly? CloseDate,
        bool? IsClosed,
        float? PlaceArea);
}
