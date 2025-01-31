namespace BillsControl.Core
{
    public record ResidentDto(
        Guid Id,
        string Name,
        string Surname,
        Guid PersonalBillId,
        bool IsOwner);
}
