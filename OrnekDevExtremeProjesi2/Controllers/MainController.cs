using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
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
            ViewBag.Categories = _categoryService.GetAllCategories();
            //TempData 
              //ViewData  
            return View();
        }

        public ActionResult TestLogin()
        {
            Session["UserId"] = 1;
            Session["UserName"] = "Volkan";
            Session["Role"] = "Admin";

            return RedirectToAction("Index");
        }

        public JsonResult DebugSession()
        {
            return Json(new
            {
                userId = Session["UserId"],
                userName = Session["UserName"],
                role = Session["Role"]
            }, JsonRequestBehavior.AllowGet);
        }

        
        [HttpGet]
        public ContentResult GetMainList()
        {
            int CurrentUserId = Session["UserId"] != null ? (int)Session["UserId"] : 0;
            string CurrentRole = Session["Role"] != null ? Session["Role"].ToString() : "";
            var data = _mainService.GetMainList(CurrentUserId, CurrentRole);

            return Content(
                Newtonsoft.Json.JsonConvert.SerializeObject(data),
                "application/json"
            );
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateMain(string values)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(values))
                    return Json(new { success = false, message = "values boş geliyor." });

                var json = Newtonsoft.Json.Linq.JObject.Parse(values);
                var main = new Main();

                if (json["Title"] != null)
                    main.Title = json["Title"].ToString();

                if (json["Description"] != null)
                    main.Description = json["Description"].ToString();

                if (json["CategoryId"] != null && !string.IsNullOrWhiteSpace(json["CategoryId"].ToString()))
                    main.CategoryId = json["CategoryId"].ToObject<int>();

                if (json["IsActive"] != null)
                    main.IsActive = json["IsActive"].ToObject<bool>();
                else
                    main.IsActive = true;

                if (string.IsNullOrWhiteSpace(main.Title))
                    return Json(new { success = false, message = "Başlık alanı boş bırakılamaz!" });

                int currentUserId = Session["UserId"] != null ? (int)Session["UserId"] : 1;
                _mainService.CreateMain(main, currentUserId);

                return Json(new { success = true, message = "Kayıt başarıyla eklendi." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message, values = values });
            }
        }
        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Delete)]
        public JsonResult DeleteMain(int key)
        {
            try
            {
                int currentUserId = Session["UserId"] != null ? (int)Session["UserId"] : 1;
                string currentUserName = Session["UserName"]?.ToString() ?? "Volkan";
                string role = Session["Role"]?.ToString() ?? "Admin";

                var result = _mainService.DeleteMain(key, currentUserId, currentUserName, role);

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

        [HttpPut]
        public JsonResult UpdateMain(int key, string values)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(values))
                    return Json(new { success = false, message = "values boş geliyor." });

                var dbKayit = _mainService.GetById(key);
                if (dbKayit == null)
                    return Json(new { success = false, message = "Kayıt bulunamadı." });

                Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(values);

                if (json["Title"] != null)
                    dbKayit.Title = json["Title"].ToString();

                if (json["Description"] != null)
                    dbKayit.Description = json["Description"].ToString();

                if (json["IsActive"] != null)
                    dbKayit.IsActive = json["IsActive"].ToObject<bool>();

                if (json["CategoryId"] != null && !string.IsNullOrWhiteSpace(json["CategoryId"].ToString()))
                    dbKayit.CategoryId = json["CategoryId"].ToObject<int>();

                int currentUserId = Session["UserId"] != null ? (int)Session["UserId"] : 1;
                _mainService.UpdateMain(dbKayit, currentUserId);

                return Json(new { success = true, values = values });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message,
                    key = key,
                    values = values
                });
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
        public JsonResult GetCategoryList(DataSourceLoadOptions loadOptions)
        {
            var categories = _categoryService.GetAllCategories();
            var result = DataSourceLoader.Load(categories, loadOptions);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Details(int id)
        {
            var data = _mainService.GetById(id);
            if (data == null) return HttpNotFound();

            var categories = _categoryService.GetAllCategories();
            var category = categories.FirstOrDefault(x => x.Id == data.CategoryId);
            ViewBag.CategoryName = category != null ? category.Name : "";

            return PartialView("~/Views/Shared/Detail.cshtml", data);
        }

        [HttpGet]
        public ContentResult GetLogsByMainId(int id)
        {
            var logs = _activityLogService.GetLogsByMainId(id);
            return Content(
                Newtonsoft.Json.JsonConvert.SerializeObject(logs),
                "application/json"
            );
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
        x.Id,
        x.Title,
        x.AdminNote,
        x.LastApprovalDate
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
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["UserId"] == null)
            {
                filterContext.Result = RedirectToAction("Login", "Account");
                return;
            }

            base.OnActionExecuting(filterContext);
        }   




    }

}