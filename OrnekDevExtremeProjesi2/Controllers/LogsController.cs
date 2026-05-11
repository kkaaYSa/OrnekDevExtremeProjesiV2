using System.Web.Mvc;
using OrnekDevExtremeProjesi2.Business.Logging;

namespace OrnekDevExtremeProjesi2.Controllers
{
    public class LogsController : Controller
    {
        private readonly IActivityLogService _logService;

        public LogsController()
        {
            _logService = new ActivityLogService();
        }

        public ActionResult Index()
        {
            string currentRole = Session["Role"] != null ? Session["Role"].ToString() : "";

            if (currentRole != "Admin")
                return RedirectToAction("Index", "Main");

            return View();
        }

        [HttpGet]
        public ActionResult GetLogs()
        {
            string currentRole = Session["Role"] != null ? Session["Role"].ToString() : "";

            if (currentRole != "Admin")
                return new HttpStatusCodeResult(403);

            var data = _logService.GetAllLogs();

            return Content(
                Newtonsoft.Json.JsonConvert.SerializeObject(data),
                "application/json"
            );
        }
    }
}
