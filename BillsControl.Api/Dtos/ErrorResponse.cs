namespace BillsControl.Api.Dtos;

public record ErrorResponse(
    int StatusCode,
    string Error,
    string? Details);