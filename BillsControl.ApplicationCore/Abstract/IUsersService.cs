using BillsControl.ApplicationCore.Dtos;

namespace BillsControl.ApplicationCore.Abstract;

public interface IUsersService
{
    Task Register(RegisterUserRequest registerUserRequest, bool isAdmin = false);
    Task<string> Login(LoginUserRequest loginUserRequest);
}