using System.Data.Entity;
using System.Linq;
using OrnekDevExtremeProjesi2.Models;

namespace OrnekDevExtremeProjesi2.DataAccess.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _db;

        public AccountRepository()
        {
            _db = new AppDbContext();
        }

        public Users GetUserForLogin(string username, string password)
        {
            return _db.Users
                .Include(u => u.UsersRole)
                .FirstOrDefault(u => u.UserName == username && u.Password == password);
        }

        public bool UserNameExists(string userName)
        {
            return _db.Users.Any(x => x.UserName == userName);
        }

        public void AddUser(Users user)
        {
            _db.Users.Add(user);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}