namespace BillsControl.Core.Dtos;

public record FullNameDto(
    string? LastName,
    string? FirstName,
    string? MiddleName);