namespace BillsControl.ApplicationCore.Dtos
{
    public record UpdateResidentRequest(
        string? FirstName,
        string? LastName,
        string? MiddleName,
        Guid? PersonalBillId,
        bool? IsOwner);
}