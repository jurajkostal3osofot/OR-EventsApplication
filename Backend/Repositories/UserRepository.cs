using Backend.Domain;
using Backend.Interfaces.Repositories;
using Backend.Models;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext context)
        {
            _databaseContext = context;
        }
        public void Add(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            _databaseContext.Users.Remove(new User { Id = id });
        }

        public void Edit(User user)
        {
            throw new NotImplementedException();
        }

        public User Get(int id, UserFetchlnclusion userInclusion)
        {
            return _databaseContext.Users.Find(id);
        }

        public User Get(string email, UserFetchlnclusion userInclusion)
        {
            IQueryable<User> user = _databaseContext.Users;
            if (userInclusion.Group)
            {
                user = user.Include(x => x.Group);
            }

            return user.FirstOrDefault(x => x.Email.Equals(email));
        }

        public IEnumerable<User> GetUsers()
        {
            return _databaseContext.Users;
        }
    }
}