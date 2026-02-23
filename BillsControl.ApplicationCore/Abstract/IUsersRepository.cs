using BillsControl.ApplicationCore.Entities;

namespace BillsControl.ApplicationCore.Abstract;

public interface IUsersRepository
{
    Task<UserEntity?> GetByUserName(string userName);
    Task<Guid> CreateUser(UserEntity userEntity);
}