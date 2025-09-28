namespace BillsControl.ApplicationCore.Dtos;

public record ResidentsQueryFilterParams(
    int? Page,
    int? PageSize,
    Guid? BillId,
    string? BillNumber);