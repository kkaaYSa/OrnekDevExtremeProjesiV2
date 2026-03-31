using OrnekDevExtremeProjesi2.Models;

namespace OrnekDevExtremeProjesi2.DataAccess.Account
{
    public interface IAccountRepository
    {
        Users GetUserForLogin(string username, string password);
        bool UserNameExists(string userName);
        void AddUser(Users user);
        void Save();
    }
}