namespace BillsControl.Core
{
    public record UpdatePersonalBillRequest(
        string BillNumber,
        DateTime OpenTime,
        string Address,
        float PlaceArea);
}
