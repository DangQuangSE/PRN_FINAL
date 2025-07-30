using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repository;

namespace BLL.Service
{
    public class AccountService
    {
        private readonly AccountRepository _rp;

        public AccountService()
        {
            _rp = new AccountRepository();
        }

        public Account? Login(string username, string password)
        {
            return _rp.Login(username, password);
        }

        public bool checkAcount(string username, string email)
        {
            return _rp.checkAcount(username, email);
        }

        public Account? GetAccountByUserId(int userId)
        {
            return _rp.GetAccountByUserId(userId);
        }
    }
}