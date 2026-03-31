using System.Collections.Generic;
using OrnekDevExtremeProjesi2.Models.DTOs;

namespace OrnekDevExtremeProjesi2.Business.Notes
{
    public interface INotesService
    {
        List<NoteListDto> GetByMainId(int mainId);
        NoteActionResultDto AddNote(int mainId, string noteText, int userId);
        NoteActionResultDto DeleteNote(int noteId, int userId);
    }
}