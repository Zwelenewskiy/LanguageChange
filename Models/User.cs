using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LanguageChange.Models
{
    public class User
    {
        public int ID { get; set; }

        public string FIO { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Lang { get; set; }
    }

    public class UserDBContext: DbContext
    {
        public DbSet<User> users { get; set; }
    }
}