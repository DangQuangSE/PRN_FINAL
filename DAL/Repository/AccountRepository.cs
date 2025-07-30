using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repository
{
    public class AccountRepository
    {
        private readonly GenderHealthCareSystemContext _context;

        public AccountRepository()
        {
            _context = new GenderHealthCareSystemContext();
        }

        public Account? Login(string username, string password)
        {
            //var hashedPass = BCrypt.Net.BCrypt.HashPassword(password);
            return _context.Accounts.FirstOrDefault(x => (x.UserName == username
                || x.Email == username)
                && x.Password == password);
        }

        public bool checkAcount(string username, string email)
        {
            return _context.Accounts.Any(x => x.UserName == username || x.Email == email);
        }

        public Account? GetAccountByUserId(int userId)
        {
            return _context.Accounts.FirstOrDefault(x => x.UserId == userId);
        }
    }
}