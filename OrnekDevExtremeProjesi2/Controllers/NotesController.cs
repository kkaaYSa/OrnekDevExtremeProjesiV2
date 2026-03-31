using System.Web.Mvc;
using OrnekDevExtremeProjesi2.Business.Notes;

namespace OrnekDevExtremeProjesi2.Controllers
{
    public class NotesController : Controller
    {
        private readonly INotesService _notesService;

        public NotesController()
        {
            _notesService = new NotesService();
        }

        [HttpGet]
        public JsonResult GetByMainId(int id)
        {
            var notes = _notesService.GetByMainId(id);
            return Json(notes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddNote(int mainId, string noteText)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Oturum bulunamadı."
                });
            }

            int userId = (int)Session["UserId"];
            var result = _notesService.AddNote(mainId, noteText, userId);

            return Json(new
            {
                success = result.Success,
                message = result.Message
            });
        }

        [HttpPost]
        public JsonResult DeleteNote(int id)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Oturum bulunamadı."
                });
            }

            int userId = (int)Session["UserId"];
            var result = _notesService.DeleteNote(id, userId);

            return Json(new
            {
                success = result.Success,
                message = result.Message
            });
        }
    }
}