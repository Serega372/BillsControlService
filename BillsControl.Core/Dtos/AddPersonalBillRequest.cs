namespace BillsControl.Core
{
    public record AddPersonalBillRequest(
        Guid Id,
        string BillNumber,
        DateTime OpenTime,
        string Address,
        float PlaceArea,
        List<ResidentDto>? Residents);
}
