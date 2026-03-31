using System.Collections.Generic;
using NoteEntity = global::OrnekDevExtremeProjesi2.Models.Notes;
using OrnekDevExtremeProjesi2.Models.DTOs;

namespace OrnekDevExtremeProjesi2.DataAccess.Notes
{
    public interface INotesRepository
    {
        List<NoteListDto> GetByMainId(int mainId);
        NoteEntity GetById(int id);
        void Add(NoteEntity note);
        void Delete(NoteEntity note);
        void Save();
    }
}