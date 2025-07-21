using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DAL.Repository
{
    public class AccountRepository
    {
        private readonly Su25researchDbContext _context;

        public AccountRepository( )
        {
            _context = new();
        }

        public UserAccount? GetOne(string email, string password)
        {
           return _context.UserAccounts.FirstOrDefault(x => x.Email.ToLower().Trim() == email.ToLower().Trim() && x.Password == password);
        }
    }

}