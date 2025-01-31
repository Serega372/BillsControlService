namespace BillsControl.Core
{
    public record PersonalBillsResponse(
        Guid Id,
        string BillNumber,
        DateTime OpenTime,
        DateTime CloseTime,
        string Address,
        float PlaceArea,
        List<ResidentDto>? Residents);
}
