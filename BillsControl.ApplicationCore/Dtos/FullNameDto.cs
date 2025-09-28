namespace BillsControl.ApplicationCore.Dtos;

public record FullNameDto(
    string? LastName,
    string? FirstName,
    string? MiddleName);