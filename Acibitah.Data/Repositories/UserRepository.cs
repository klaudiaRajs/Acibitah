using Acibitah.Data.Data;
using Acibitah.Data.Repositories.Interfaces;
using Acibitah.Models;
using Acibitah.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acibitah.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _db;
        private readonly string _pepper;
        private readonly int _iteration = 3;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
            _pepper = Environment.GetEnvironmentVariable("Pepper");
        }
        public bool Save(User user, string password)
        {
            try
            {
                user.PasswordSalt = PasswordHasher.GenerateSalt();
                user.PasswordHash = PasswordHasher.ComputeHash(password, user.PasswordSalt, _pepper, _iteration); 
                _db.Users.Add(user);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public User LogIn(string username, string password)
        {
            User user = _db.Users.FirstOrDefault(user => user.Login == username); 
            if (user == null)
            {
                throw new Exception("There is no user for that login"); 
            }
            var passwordHash = PasswordHasher.ComputeHash(password, user.PasswordSalt, _pepper, _iteration); 
            if( passwordHash != user.PasswordHash) 
            {
                throw new Exception("Passwords does not match.");
            }
            return user; 
        }
    }
}
