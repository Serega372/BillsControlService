namespace BillsControl.ApplicationCore.Dtos
{
    public record PersonalBillsResponse(
        Guid Id,
        string BillNumber,
        DateOnly OpenDate,
        DateOnly? CloseDate,
        bool? IsClosed,
        string? Address,
        float? PlaceArea,
        List<ResidentsResponse> Residents);
}
