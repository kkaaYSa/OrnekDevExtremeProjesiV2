using System.Web.Mvc;
using System.Web.Helpers;
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

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is HttpAntiForgeryException)
            {
                var cookieName = AntiForgeryConfig.CookieName ?? "__RequestVerificationToken";

                if (Request.Cookies[cookieName] != null)
                {
                    Response.Cookies.Add(new System.Web.HttpCookie(cookieName)
                    {
                        Expires = System.DateTime.Now.AddDays(-1)
                    });
                }

                TempData["Error"] = "Oturum doğrulaması yenilendi. Lütfen tekrar deneyin.";
                filterContext.ExceptionHandled = true;
                filterContext.Result = RedirectToAction("Login");
                return;
            }

            base.OnException(filterContext);
        }

        // LOGIN SAYFASI
        public ActionResult Login()
        {
            if (TempData["Error"] != null)
                ViewBag.Error = TempData["Error"];

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
