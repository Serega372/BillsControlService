using BillsControl.ApplicationCore.Entities;

namespace BillsControl.ApplicationCore.Abstract.Auth;

public interface IJwtProvider
{
    string GenerateToken(UserEntity userEntity);
}