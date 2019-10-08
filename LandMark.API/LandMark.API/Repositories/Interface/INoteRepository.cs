using LandMark.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandMark.API.Repositories.Interface
{
    /// <summary>
    /// This interface provides repository CRUD operations
    /// </summary>
    public interface INoteRepository
    {
        Note Add(Note note);
        IEnumerable<Note> GetNotes(Position position);
        IEnumerable<Note> GetNotes(string user, Position position);
        IEnumerable<Note> Search(string query);
        IEnumerable<Note> GetAllNotes();
    }
}
