using System.Web.Mvc;
using System.Web.Security;
using OrnekDevExtremeProjesi2.Business.Account;
using OrnekDevExtremeProjesi2.Models;

namespace OrnekDevExtremeProjesi2.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController()
        {
            _accountService = new AccountService();
        }

        // LOGIN SAYFASI
        public ActionResult Login()
        {
            return View();
        }

        // LOGIN POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            var user = _accountService.Login(username, password);

            if (user != null)
            {
                Session["UserId"] = user.Id;
                Session["UserName"] = user.UserName;
                Session["Role"] = user.UsersRole.RoleName;
                //FormsAuthentication.SetAuthCookie(user.UserName, false);
                return RedirectToAction("Index", "Main");
            }

            ViewBag.Error = "Kullanıcı adı veya şifre hatalı!";
            return View();
        }

        // REGISTER SAYFASI
        public ActionResult Register()
        {
            return View();
        }

        // REGISTER POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users user)
        {
            var result = _accountService.Register(user);

            if (result)
            {
                return RedirectToAction("Login");
            }

            ViewBag.Error = "Bu kullanıcı adı zaten kayıtlı!";
            return View(user);
        }

        // LOGOUT
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}