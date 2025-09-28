namespace BillsControl.ApplicationCore.Dtos
{
    public record AddResidentRequest(
        string FirstName,
        string LastName,
        string? MiddleName,
        Guid? PersonalBillId,
        bool? IsOwner);
}