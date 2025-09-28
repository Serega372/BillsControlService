namespace BillsControl.ApplicationCore.Dtos
{
    public record ResidentsResponse(
        Guid Id,
        string? PersonalBillNumber,
        string FirstName,
        string LastName,
        string? MiddleName,
        Guid? PersonalBillId,
        bool IsOwner);
}
