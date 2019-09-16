using SignalRChat1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace SignalRChat1
{
    public class UserDLL
    {
        private TestSocked _dbContext = DbContextFactory.GetIntance();
        public bool AddUser(SockedUser sockedUser)
        {
            DbSet<SockedUser> _SockedUser = _dbContext.Set<SockedUser>();
            _SockedUser.AddOrUpdate(sockedUser);
            return _dbContext.SaveChanges()>0;
        }
        public bool UpdateUser(SockedUser sockedUser)
        {
            DbSet<SockedUser> _SockedUser = _dbContext.Set<SockedUser>();
            _SockedUser.Attach(sockedUser);
            _dbContext.Entry(sockedUser).State = EntityState.Modified;
            return _dbContext.SaveChanges() > 0;
        }
        public bool delUser(SockedUser sockedUser)
        {
            DbSet<SockedUser> _SockedUser = _dbContext.Set<SockedUser>();
            _SockedUser.Remove(sockedUser);
            return _dbContext.SaveChanges() > 0;
        }
        public SockedUser SetUser(string Password,string Name)
        {
            DbSet<SockedUser> _SockedUser = _dbContext.Set<SockedUser>();
            return _SockedUser.FirstOrDefault(s => (string.IsNullOrEmpty(Password) || s.Password == Password) && (string.IsNullOrEmpty(Name) || s.Name == Name));
        }
    }
}