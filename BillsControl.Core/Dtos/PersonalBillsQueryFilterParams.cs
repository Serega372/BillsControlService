namespace BillsControl.Core.Dtos;

public record PersonalBillsQueryFilterParams(
    int? Page, 
    int? PageSize,
    bool? WithResidents,
    string? Address,
    string? BillNumber,
    DateOnly? OpenDate,
    string? FullName);