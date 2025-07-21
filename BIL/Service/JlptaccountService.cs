using DAL.Models;
using BIL.IServices;
using DAL.IRepositories;

namespace BIL.Service;

public class JlptaccountService : GenericService<Jlptaccount>, IJlptaccountService
{
    public Jlptaccount? Login(string email, string password)
    {
        return _repository.Find(x => x.Email == email && x.Password == password).FirstOrDefault();
    }
}