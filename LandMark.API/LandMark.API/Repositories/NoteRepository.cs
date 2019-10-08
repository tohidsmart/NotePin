using LandMark.API.Data;
using LandMark.API.Entities;
using LandMark.API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using LinqKit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LandMark.API.Repositories
{
    /// <summary>
    /// This class implements INoteRepository interface and has DbContext as its dependency.
    /// CRUD operations are performed in this 
    /// It is injected as dependency into Controller API 
    /// </summary>
    public class NoteRepository : INoteRepository
    {
        private readonly LandmarkDbContext landmarkDbContext;

        public NoteRepository(LandmarkDbContext landmarkDbContext)
        {
            this.landmarkDbContext = landmarkDbContext;
        }

        /// <summary>
        /// This method saves a note into database and returns the inserted record if successful
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public Note Add(Note note)
        {
            note.Id = Guid.NewGuid();
            landmarkDbContext.Notes.Add(note);
            int insertedRecord = landmarkDbContext.SaveChanges();
            if (insertedRecord > 0)
            {
                return note;
            }
            return null;
        }

        /// <summary>
        /// This method gets all the notes for a particular position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public IEnumerable<Note> GetNotes(Position position)
        {
            var notes = GetNotesByPosition(position);
            return notes;
        }

        /// <summary>
        /// This method gets all the notes for a particular user at a particular position
        /// </summary>
        /// <param name="user"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public IEnumerable<Note> GetNotes(string user, Position position)
        {
            var userNotes = GetNotesByPosition(position).Where(note => note.User == user);
            return userNotes;
        }

        /// <summary>
        /// This method searches for notes based on query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<Note> Search(string query)
        {
            IEnumerable<Note> notes = SearchAndGetNotes(query);
            return notes;
        }

        private IEnumerable<Note> GetNotesByPosition(Position position)
        {
            var notes = landmarkDbContext.Notes.
                Where(note => note.Latitude == position.Latitude && note.Longitude == position.Longitude);
            return notes.AsEnumerable();
        }


        private IEnumerable<Note> SearchAndGetNotes(string query)
        {
            IEnumerable<Note> filteredNotes = landmarkDbContext.Notes.AsExpandable().Where(CreateSearchExpression(query));
            return filteredNotes;

        }

        /// <summary>
        /// This method builds a search predicate based on tokenized search query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private Expression<Func<Note, bool>> CreateSearchExpression(string query)
        {
            string[] searchToken = query.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var predicate = PredicateBuilder.False<Note>(); ;
            foreach (var token in searchToken)
            {
                var tempToken = token;
                predicate = predicate.Or(note => EF.Functions.Like(note.User, $"%{tempToken}%"));
                predicate = predicate.Or(note => EF.Functions.Like(note.Content, $"%{tempToken}%"));
            }
            return predicate;
        }

        public IEnumerable<Note> GetAllNotes()
        {
            return landmarkDbContext.Notes.AsEnumerable();
        }
    }
}
