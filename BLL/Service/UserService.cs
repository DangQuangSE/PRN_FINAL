using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repository;

namespace BLL.Service
{
    public class UserService
    {
        private UserRepository _rp;

        public UserService()
        {
            _rp = new UserRepository();
        }

        public User? GetUserByAccountId(Account account)
        {
            return _rp.GetUserByAccountId(account);
        }

        public void SignUpUser(User user, string email, string usename, string password)
        {
            _rp.SignUpUser(user, email, usename, password);
        }

        public void UpdateProfile(User user)
        {
            _rp.UpdateProfile(user);
        }

        public User? GetUserByUserId (int id)
        {
            return _rp.GetUserByUserId(id);
        }
    }
}
