using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.Repository
{
    public class UserRepository
    {
        private readonly GenderHealthCareSystemContext _context;

        public UserRepository()
        {
            _context = new GenderHealthCareSystemContext();
        }

        public User? GetUserByAccountId(Account account)
        {
            var userId = account.UserId;
            return _context.Users.Find(userId);
        }

        public void SignUpUser(User user, string email, string username, string password)
        {
            var id = _context.Users.Max(x => x.UserId) + 1;
            _context.Users.Add(user);
            _context.SaveChanges();
            var account = new Account()
            {
                Status = "ACTIVE",
                Email = email,
                UserName = username,
                Password = password,
                UserId = id,
            };
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }

        public User CreateCustomer(User customer)
        {
            // Set default role for customer (assuming RoleId 2 is for customers)
            customer.RoleId = 2; // Customer role
            _context.Users.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public List<User> GetAllConsultants()
        {
            return _context.Users
                .Include(u => u.Role)
                .Where(u => u.Role != null && u.Role.RoleName == "Consultant")
                .ToList();
        }


        public void UpdateProfile(User user)
        {
            var existUser = _context.Users.Find(user.UserId);
            if (existUser != null)
            {
                existUser.FullName = user.FullName;
                existUser.Address = user.Address;
                existUser.BirthDate = user.BirthDate;
                existUser.Gender = user.Gender;
                existUser.Phone = user.Phone;

                _context.Users.Update(existUser);
                _context.SaveChanges();
            }
        }
    }
}
