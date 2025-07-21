using DAL.Entities;
using BIL.IServices;
using DAL.Repository;

namespace BIL.Service;

public class AccountService : GenericService<UserAccount>, IAccountService
{
    public AccountService() : base() 
    {
    }

    public UserAccount? Login(string email, string password)
    {
        return _repository.FirstOrDefault(x => x.Email == email && x.Password == password);
    }
}