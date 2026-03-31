using OrnekDevExtremeProjesi2.Models;

namespace OrnekDevExtremeProjesi2.Business.Account
{
    public interface IAccountService
    {
        Users Login(string username, string password);
        bool Register(Users user);
    }
}