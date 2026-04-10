using System.Web.Mvc;
using OrnekDevExtremeProjesi2.Business.Approvals;

namespace OrnekDevExtremeProjesi2.Controllers
{
    public class ApprovalController : Controller
    {
        private readonly IApprovalService _approvalService;

        public ApprovalController()
        {
            _approvalService = new ApprovalService();
        }

        public ActionResult Approvals()
        {
            string currentRole = Session["Role"] != null ? Session["Role"].ToString() : "";

            if (currentRole != "Admin")
                return RedirectToAction("Index", "Main");

            return View();
        }

        [HttpGet]
        public ContentResult GetPendingApprovals()
        {
            var data = _approvalService.GetPendingApprovals();

            return Content(
                Newtonsoft.Json.JsonConvert.SerializeObject(data),
                "application/json"
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ActionRequest(int id, bool isApprove, string adminNote)
        {
            int currentUserId = Session["UserId"] != null ? (int)Session["UserId"] : 1;
            string currentUserName = Session["UserName"] != null
                ? Session["UserName"].ToString()
                : "Bilinmeyen Kullanıcı";

            var result = _approvalService.ActionRequest(id, isApprove, adminNote, currentUserId, currentUserName);

            return Json(new
            {
                success = result.Success,
                message = result.Message
            });
        }
    }
}