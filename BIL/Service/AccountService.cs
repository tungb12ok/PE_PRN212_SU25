using DAL.Entities;
using BIL.IServices;
using DAL.Repository;

namespace BIL.Service;

public class AccountService : GenericService<UserAccount>, IAccountService
{
    public AccountService() : base() 
    {
    }
}