using System.Collections.Generic;
using System.Linq;
using NoteEntity = global::OrnekDevExtremeProjesi2.Models.Notes;
using OrnekDevExtremeProjesi2.Models;
using OrnekDevExtremeProjesi2.Models.DTOs;

namespace OrnekDevExtremeProjesi2.DataAccess.Notes
{
    public class NotesRepository : INotesRepository
    {
        private readonly AppDbContext _db;

        public NotesRepository()
        {
            _db = new AppDbContext();
        }

        public List<NoteListDto> GetByMainId(int mainId)
        {
            return _db.notes
                .Where(x => x.MainId == mainId)
                .OrderByDescending(x => x.CreatedDate)
                .ToList()
                .Select(x => new NoteListDto
                {
                    Id = x.Id,
                    UserName = x.RequestingUser != null ? x.RequestingUser.UserName : "",
                    NoteText = x.NoteText,
                    CreatedDate = x.CreatedDate.ToString("dd.MM.yyyy HH:mm")
                })
                .ToList();
        }

        public NoteEntity GetById(int id)
        {
            return _db.notes.FirstOrDefault(x => x.Id == id);
        }

        public void Add(NoteEntity note)
        {
            _db.notes.Add(note);
        }

        public void Delete(NoteEntity note)
        {
            _db.notes.Remove(note);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}