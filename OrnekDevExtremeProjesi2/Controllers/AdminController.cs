using System;
using System.Linq;
using System.Web.Mvc;
using OrnekDevExtremeProjesi2.Models; // Model namespace'in

namespace OrnekDevExtremeProjesi2.Controllers
{
    public class AdminController : Controller
    {
        AppDbContext db = new AppDbContext();

        [HttpGet]
        public JsonResult GetAllSystemLogs()
        {
            try
            {
                // 1. Aşama: Veritabanından Select ile sadece gerekli alanları çekiyoruz.
                // RequestingUser üzerinden UserName'i çekmek dairesel referansı engeller.
                var rawLogs = db.ActivityLogs
                    .OrderByDescending(x => x.LogDate)
                    .Select(x => new
                    {
                        x.Id,
                        x.MainId,
                        // RequestingUser üzerinden UserName alıyoruz, nesnenin tamamını değil!
                        UserName = x.RequestingUser != null ? x.RequestingUser.UserName : "Sistem",
                        ActionType = x.Action,
                        x.Description,
                        x.LogDate
                    }).ToList();

                // 2. Aşama: Bellekte (C# tarafında) tarih formatlaması yapıyoruz.
                var result = rawLogs.Select(x => new
                {
                    x.Id,
                    x.MainId,
                    x.UserName,
                    x.ActionType,
                    x.Description,
                    x.LogDate
                }).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SystemDetails() // İsim tam olarak böyle olmalı!
        {
            return View(); // Burası "Views/Admin/SystemDetails.cshtml" dosyasına bakar.
        }
    }
}