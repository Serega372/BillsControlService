using BillsControl.ApplicationCore.Enums;

namespace BillsControl.ApplicationCore.Entities;

public class UserEntity
{
    public Guid Id { get; init; } =  Guid.NewGuid();
    public required string UserName { get; set; }
    public required string PasswordHash { get; set; }
    public required UserRole Role { get; set; }
}