namespace BillsControl.ApplicationCore.Dtos;

public record LoginUserRequest(
    string UserName,
    string Password);