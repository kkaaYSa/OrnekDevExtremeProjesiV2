using System.Web.Mvc;
using OrnekDevExtremeProjesi2.Business.Profile;

namespace OrnekDevExtremeProjesi2.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController()
        {
            _profileService = new ProfileService();
        }
        [HttpGet]
        public JsonResult GetProfileData()
        {
            int userId = (int)Session["UserId"];
            var user = _profileService.GetProfileData(userId);

            if (user == null)
                return Json(new { success = false, message = "Kullanıcı bulunamadı!" }, JsonRequestBehavior.AllowGet);

            return Json(new { success = true, data = user }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateProfile(int id, string userName, string oldPassword, string newPassword)
        {
            var sessionUser = Session["UserId"];
            if (sessionUser == null)
                return Json(new { Success = false, Message = "Oturum süresi dolmuş!" });

            int currentUserId = (int)sessionUser;

            var result = _profileService.UpdateProfile(id, userName, oldPassword, newPassword, currentUserId);

            if (result.Success)
            {
                Session["UserName"] = userName;
            }

            return Json(new
            {
                Success = result.Success,
                Message = result.Message
            });
        }
        public ActionResult Profile() // İsim tam olarak böyle olmalı!
        {
            return View(); // Burası "Views/Admin/SystemDetails.cshtml" dosyasına bakar.
        }
    }
}