using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Md5TestFor
{
    class Class3
    {
        public void Md5For()
        {
            
            
            string Key = Console.ReadLine();
            string Value = MD5(Key);

            if (!getmd5count(Key))
            {
                Console.WriteLine(addMd5(Key, Value));
            }
            
            Console.ReadLine();
        }
        bool getmd5count(string Key)
        {
            KeyValue _dbContext = DbContextFactory.GetIntance();
            DbSet<Md5Test> _Md5dbSet = _dbContext.Set<Md5Test>();
            return _Md5dbSet.Count(a => a.Key == Key) > 0;
        }
        bool addMd5(string Key,string Value)
        {
            KeyValue _dbContext = DbContextFactory.GetIntance();
            DbSet<Md5TestUser> _Md5UserdbSet = _dbContext.Set<Md5TestUser>();
            _Md5UserdbSet.Add(new Md5TestUser() { Key = Key, Value = Value });
            return _dbContext.SaveChanges() > 0;
        }

        string MD5(string input)
        {
            MD5 md5Hasher = System.Security.Cryptography.MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            foreach (byte d in data)
            {
                sBuilder.Append(d.ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
