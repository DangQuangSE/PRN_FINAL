using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            return _context.Accounts
                .Include(a => a.User)
                .ThenInclude(u => u.Role)
                .FirstOrDefault(a => a.UserName == username && a.Password == password && a.Status == "ACTIVE");
        }

        public bool checkAcount(string username, string email)
        {
            return _context.Accounts.Any(a => a.UserName == username || a.Email == email);
        }
    }
}