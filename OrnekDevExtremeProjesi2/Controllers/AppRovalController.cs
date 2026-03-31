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
            if (Session["Role"]?.ToString() != "Admin")
                return RedirectToAction("Index", "Main");

            var data = _approvalService.GetPendingApprovals();
            return View(data);
        }
        public JsonResult GetPendingApprovals()
        {
            var data = _approvalService.GetPendingApprovals();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ActionRequest(int id, bool isApprove)
        {
            int currentUserId = Session["UserId"] != null ? (int)Session["UserId"] : 1;
            string currentUserName = Session["UserName"] != null ? Session["UserName"].ToString() : "Bilinmeyen Kullanıcı";

            var result = _approvalService.ActionRequest(id, isApprove, currentUserId, currentUserName);

            return Json(new
            {
                success = result.Success,
                message = result.Message
            });
        }
    }
}