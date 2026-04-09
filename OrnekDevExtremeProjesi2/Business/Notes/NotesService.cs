using System;
using System.Collections.Generic;
using OrnekDevExtremeProjesi2.Business.Logging;
using OrnekDevExtremeProjesi2.DataAccess.Notes;
using NoteEntity = global::OrnekDevExtremeProjesi2.Models.Notes;
using OrnekDevExtremeProjesi2.Models.DTOs;

namespace OrnekDevExtremeProjesi2.Business.Notes
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;
        private readonly IActivityLogService _activityLogService;

        public NotesService()
        {
            _notesRepository = new NotesRepository();
            _activityLogService = new ActivityLogService();
        }

        public List<NoteListDto> GetByMainId(int mainId)
        {
            return _notesRepository.GetByMainId(mainId);
        }

        public NoteActionResultDto AddNote(int mainId, string noteText, int userId)
        {
            if (mainId <= 0)
            {
                return new NoteActionResultDto
                {
                    Success = false,
                    Message = "Geçersiz kayıt bilgisi."
                };
            }

            if (string.IsNullOrWhiteSpace(noteText))
            {
                return new NoteActionResultDto
                {
                    Success = false,
                    Message = "Not boş olamaz."
                };
            }

            if (userId <= 0)
            {
                return new NoteActionResultDto
                {
                    Success = false,
                    Message = "Geçersiz kullanıcı."
                };
            }

            var note = new NoteEntity
            {
                MainId = mainId,
                NoteText = noteText.Trim(),
                CreatedDate = DateTime.Now,
                UsersId = userId
            };

            _notesRepository.Add(note);
            _notesRepository.Save();

            _activityLogService.AddLog(note.MainId, "Note", "Delete", userId);

            return new NoteActionResultDto
            {
                Success = true,
                Message = "Not başarıyla eklendi."
            };
        }

        public NoteActionResultDto DeleteNote(int noteId, int userId)
        {
            if (noteId <= 0)
            {
                return new NoteActionResultDto
                {
                    Success = false,
                    Message = "Geçersiz not id."
                };
            }

            var note = _notesRepository.GetById(noteId);

            if (note == null)
            {
                return new NoteActionResultDto
                {
                    Success = false,
                    Message = "Not bulunamadı."
                };
            }

            var mainId = note.MainId;

            _notesRepository.Delete(note);
            _notesRepository.Save();

            _activityLogService.AddLog(mainId, "Note", "Delete", userId);

            return new NoteActionResultDto
            {
                Success = true,
                Message = "Not silindi."
            };
        }
    }
}