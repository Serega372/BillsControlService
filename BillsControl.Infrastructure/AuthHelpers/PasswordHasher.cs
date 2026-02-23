using BillsControl.ApplicationCore.Abstract.Auth;

namespace BillsControl.Infrastructure.AuthHelpers;

public class PasswordHasher : IPasswordHasher
{
    public string Generate(string password) => 
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    
    public bool Verify(string password, string hashedPassword) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
}