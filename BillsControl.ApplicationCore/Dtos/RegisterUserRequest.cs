using BillsControl.ApplicationCore.Enums;

namespace BillsControl.ApplicationCore.Dtos;

public record RegisterUserRequest(
    string UserName,
    string Password,
    UserRole? Role);