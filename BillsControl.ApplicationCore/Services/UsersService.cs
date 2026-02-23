using AutoMapper;
using BillsControl.ApplicationCore.Abstract;
using BillsControl.ApplicationCore.Abstract.Auth;
using BillsControl.ApplicationCore.Dtos;
using BillsControl.ApplicationCore.Entities;
using BillsControl.ApplicationCore.Enums;

namespace BillsControl.ApplicationCore.Services;

public class UsersService(
    IUsersRepository usersRepository,
    IPasswordHasher passwordHasher,
    IMapper mapper,
    IJwtProvider jwtProvider) : IUsersService
{
    public async Task Register(RegisterUserRequest registerUserRequest, bool isAdmin = false)
    {
        if (!isAdmin && registerUserRequest.Role != null)
            //TODO
            throw new InvalidOperationException("You not admin!");
        
        var hashedPassword = passwordHasher.Generate(registerUserRequest.Password);
        var role = isAdmin ? registerUserRequest.Role ?? UserRole.Resident : UserRole.Resident;
        var userEntity = new UserEntity
        {
            UserName = registerUserRequest.UserName,
            PasswordHash = hashedPassword,
            Role = role
        };
        await usersRepository.CreateUser(userEntity);
    }
    
    public async Task<string> Login(LoginUserRequest loginUserRequest)
    {
        //TODO exception
        var userEntity = await usersRepository.GetByUserName(loginUserRequest.UserName) 
                         ?? throw new Exception("User not found");
        var isVerified = passwordHasher.Verify(loginUserRequest.Password, userEntity.PasswordHash);
        //TODO exception
        if (!isVerified) throw new Exception("Login failed");

        var token = jwtProvider.GenerateToken(userEntity);
        return token;
    }

    // public async Task<UsersResponse> GetByUserId(Guid id)
    // {
    //     //TODO
    //     var userEntity = await usersRepository.GetByUserName() 
    //                      ?? throw new Exception("User not found");
    // }
}