using DAL.Entities;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Service
{
    public class AccountService
    {
        //GUI  -> Services -> REPO -> DBCONTEXT -> TABLE
        private readonly AccountRepository _repo ;

        public AccountService()
        {
            _repo = new();
        }
        public UserAccount? Login(string email, string password)
        {
            
            return _repo.GetOne(email, password);
        }
    }
}
