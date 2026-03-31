using System;
using OrnekDevExtremeProjesi2.DataAccess.Account;
using OrnekDevExtremeProjesi2.Models;

namespace OrnekDevExtremeProjesi2.Business.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService()
        {
            _accountRepository = new AccountRepository();
        }

        public Users Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            return _accountRepository.GetUserForLogin(username, password);
        }

        public bool Register(Users user)
        {
            if (user == null)
                return false;

            if (string.IsNullOrWhiteSpace(user.UserName))
                return false;

            if (_accountRepository.UserNameExists(user.UserName))
                return false;

            user.RoleId = 2;
            user.IsActive = true;
            user.CreateDate = DateTime.Now;

            _accountRepository.AddUser(user);
            _accountRepository.Save();

            return true;
        }
    }
}