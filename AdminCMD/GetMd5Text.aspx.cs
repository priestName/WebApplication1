using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SignalRChat1
{
    public partial class GetMd5Text : System.Web.UI.Page
    {
        private KeyValue _dbContext = DbContextFactory.GetIntance();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string Value = ValueText.Value;
            Md5Test md5 = SetMd5(string.Empty, Value);
            if (md5!=null)
            {
                KeyText.Value = md5.Key;
                ValueText.Value = md5.Value;
            }
            else {
                Md5TestUser Usermd5 = SetUserMd5(string.Empty, Value);
                if (Usermd5 != null)
                {
                    KeyText.Value = Usermd5.Key;
                    ValueText.Value = Usermd5.Value;
                }else
                KeyText.Value = "暂未收录";
            }
            
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string Key = KeyText.Value;
            string Value = ValueText.Value;

            if (!SetMd5Count(Key,string.Empty) && !SetUserMd5Count(Key, string.Empty))
            {
                try
                {
                    addMd5User(Key, MD5(Key));
                }
                    catch (Exception ex)
                {
                    insertText(ex.Message, "Erro" + DateTime.Now.ToString("yyyy-MM-dd"));
                }

        }
        }

        bool SetMd5Count(string Key, string Value)
        {
            DbSet<Md5Test> _Md5dbSet = _dbContext.Set<Md5Test>();
            return _Md5dbSet.Count(m => (string.IsNullOrEmpty(Key) || m.Key == Key) && (string.IsNullOrEmpty(Value) || m.Value == Value)) >0;
        }
        bool SetUserMd5Count(string Key, string Value)
        {
            DbSet<Md5TestUser> _Md5dbSet = _dbContext.Set<Md5TestUser>();
            return _Md5dbSet.Count(m => (string.IsNullOrEmpty(Key) || m.Key == Key) && (string.IsNullOrEmpty(Value) || m.Value == Value)) > 0;
        }
        Md5Test SetMd5(string Key, string Value)
        {
            DbSet<Md5Test> _Md5dbSet = _dbContext.Set<Md5Test>();
            return _Md5dbSet.FirstOrDefault(m => (string.IsNullOrEmpty(Key) || m.Key == Key) && (string.IsNullOrEmpty(Value) || m.Value == Value));
        }
        Md5TestUser SetUserMd5(string Key, string Value)
        {
            DbSet<Md5TestUser> _Md5UserdbSet = _dbContext.Set<Md5TestUser>();
            return _Md5UserdbSet.FirstOrDefault(m => (string.IsNullOrEmpty(Key) || m.Key == Key) && (string.IsNullOrEmpty(Value) || m.Value == Value));
        }
        bool addMd5User(string Key,string Value)
        {
            DbSet<Md5TestUser> _Md5UserdbSet = _dbContext.Set<Md5TestUser>();
            _Md5UserdbSet.AddOrUpdate(new Md5TestUser() { Key = Key, Value = Value });
            return _dbContext.SaveChanges() > 0;
        }

        void insertText(string text, string name)
        {
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes(text + ",");
            Directory.CreateDirectory(@"C:\\ErrorLog\");
            using (FileStream fsWrite = new FileStream(@"C:\\ErrorLog\" + name + ".txt", FileMode.Append))
            {
                fsWrite.Position = fsWrite.Length;
                fsWrite.Write(myByte, 0, myByte.Length);
            };
        }

        string MD5(string input)
        {
            MD5 md5Hasher = System.Security.Cryptography.MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            foreach (byte d in data)
            {
                sBuilder.Append(d.ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}