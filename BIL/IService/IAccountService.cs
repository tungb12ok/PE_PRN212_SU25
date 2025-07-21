using DAL.Entities;

namespace BIL.IServices;

public interface IAccountService : IGenericService<UserAccount>
{
    UserAccount? Login(string email, string password);
}