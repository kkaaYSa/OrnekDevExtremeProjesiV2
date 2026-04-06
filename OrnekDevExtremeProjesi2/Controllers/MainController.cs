using OrnekDevExtremeProjesi2.Business.Categories;
using OrnekDevExtremeProjesi2.Business.Logging;
using OrnekDevExtremeProjesi2.Business.Mains;
using OrnekDevExtremeProjesi2.Models;
using System;
using System.Linq;
using System.Web.Mvc;


namespace OrnekDevExtremeProjesi2.Controllers
{
    public class MainController : Controller
    {
        private readonly IMainService _mainService;
        private readonly IActivityLogService _activityLogService;
        private readonly ICategoryService _categoryService;


        public MainController()
        {
            _activityLogService = new ActivityLogService();
            _categoryService = new CategoryService();

            _mainService = new MainService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult GetMainList()
        {
            try
            {
                int currentUserId = Session["UserId"] != null ? (int)Session["UserId"] : 0;
                string currentRole = Session["Role"] != null ? Session["Role"].ToString() : "";

                var data = _mainService.GetMainList(currentUserId, currentRole);

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Liste çekilirken hata oluştu.",
                    error = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult CreateMain(Main main)
        {
            if (string.IsNullOrEmpty(main.Title))
            {
                return Json(new { success = false, message = "Başlık alanı boş bırakılamaz!" });
            }

            try
            {
                int currentUserId = Session["UserId"] != null ? (int)Session["UserId"] : 1;

                _mainService.CreateMain(main, currentUserId);

                return Json(new { success = true, message = "Kayıt Başarıyla Eklendi." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Veritabanı hatası oluştu.", error = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult DeleteMain(int id)
        {
            try
            {
                int currentUserId = (int)Session["UserId"];
                string currentUserName = Session["UserName"].ToString();
                string role = Session["Role"].ToString();

                var result = _mainService.DeleteMain(id, currentUserId, currentUserName, role);

                if (!result)
                    return Json(new { success = false, message = "Kayıt bulunamadı!" });

                if (role != "Admin")
                    return Json(new { success = true, message = "Silme talebi Volkan'a (Admin) iletildi." });

                return Json(new { success = true, message = "Kayıt tamamen silindi." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Hata: " + ex.Message });
            }
        }

        [HttpPost] // Veya [HttpPut] ama Grid'dekiyle aynı olmalı
        public JsonResult UpdateMain(int key, string values)
        {
            try
            {
                var dbKayit = _mainService.GetById(key);
                if (dbKayit == null) return Json(new { success = false, message = "Kayıt bulunamadı." });

                // values içindeki json'u (örneğin sadece IsActive gelmiş) mevcut kayda yedir
                Newtonsoft.Json.JsonConvert.PopulateObject(values, dbKayit);

                int currentUserId = Session["UserId"] != null ? (int)Session["UserId"] : 1;
                _mainService.UpdateMain(dbKayit, currentUserId);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public JsonResult GetById(int id)
        {
            try
            {
                var data = _mainService.GetById(id);

                if (data == null)
                    return Json(new { success = true, message = "Kayıt Bulunamadı" }, JsonRequestBehavior.AllowGet);

                return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "veri çekilirken hata oluştu", error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetCategoryList()
        {
            var categories = _categoryService.GetAllCategories();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Details(int id)
        {
            var data = _mainService.GetById(id);
            if (data == null) return HttpNotFound();

            ViewBag.Id = id;
            return PartialView("~/Views/Shared/Detail.cshtml", data);
        }

        [HttpGet]
        public JsonResult GetLogsByMainId(int id)
        {
            try
            {
                var logs = _activityLogService.GetLogsByMainId(id);
                return Json(logs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult GetRejectedNotifications()
        {
            try
            {
                int currentUserId = Session["UserId"] != null ? (int)Session["UserId"] : 0;
                string currentRole = Session["Role"] != null ? Session["Role"].ToString() : "";

                var data = _mainService
                    .GetMainList(currentUserId, currentRole)
                    .Where(x => x.DeleteStatus == "Reddedildi")
                    .Select(x => new
                    {
                        x.Id
                    })
                    .ToList();

                return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Bildirimler alınırken hata oluştu.",
                    error = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }





    }

}