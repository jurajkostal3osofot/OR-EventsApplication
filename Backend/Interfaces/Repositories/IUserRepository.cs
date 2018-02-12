using Backend.Domain;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Interfaces.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        void Edit(User user);
        void Delete(long id);
        User Get(int id, UserFetchlnclusion userInclusion);
        User Get(string email, UserFetchlnclusion userInclusion);
        IEnumerable<User> GetUsers();
    }
}