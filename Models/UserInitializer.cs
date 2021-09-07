using System.Collections.Generic;
using System.Data.Entity;
namespace LanguageChange.Models
{
    public class UserInitializer : DropCreateDatabaseIfModelChanges<UserDBContext>
    {
        protected override void Seed(UserDBContext context)
        {
            var users = new List<User>() 
            {
                new User()
                {
                    FIO = "Иванов Иван Иванович",
                    Login = "User1",
                    Password = "Pass1",
                    Lang = "ru"
                },
                new User()
                {
                    FIO = "Petrov Pyotr Petrovich",
                    Login = "User2",
                    Password = "Pass2",
                    Lang = "en"
                },
                new User()
                {
                    FIO = "Petrov Pyotr Petrovich",
                    Login = "User3",
                    Password = "Pass3",
                    Lang = "es"
                },
                new User()
                {
                    FIO = "Sidorov Sidorovic",
                    Login = "User4",
                    Password = "Pass4",
                    Lang = "fr"
                }
            };

            users.ForEach(u => context.users.Add(u));
        }
    }
}