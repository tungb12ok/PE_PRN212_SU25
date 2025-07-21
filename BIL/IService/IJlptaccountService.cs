
using DAL.Models;

namespace BIL.IServices;

public interface IJlptaccountService : IGenericService<Jlptaccount>
{
    Jlptaccount? Login(string email, string password);
}