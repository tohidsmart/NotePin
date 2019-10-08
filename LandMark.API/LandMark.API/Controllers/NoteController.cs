using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LandMark.API.Entities;
using LandMark.API.Repositories.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LandMark.API.Controllers
{
    /// <summary>
    /// This controller provides REST APIs to Front end application
    /// CORS is enabled on this controller and 
    /// </summary>
    [Route("api/notes")]
    [ApiController]
    [EnableCors("AllowPolicy")]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository noteRepository;
        public NoteController(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        /// <summary>
        /// This endpoint returns all the notes or specific notes based on user position
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Get([FromQuery]string lat, [FromQuery]string lng)
        {
            if (string.IsNullOrEmpty(lat) && string.IsNullOrEmpty(lng))
            {
                IEnumerable<Note> allNotes = noteRepository.GetAllNotes();
                if (allNotes.Any())
                {
                    return Ok(allNotes);
                }
            }
            Position position = new Position
            {
                Latitude = Convert.ToDouble(lat),
                Longitude = Convert.ToDouble(lng)
            };

            IEnumerable<Note> notes = noteRepository.GetNotes(position);
            if (notes.Any())
            {
                return Ok(notes);
            }

            return NotFound();

        }

        /// <summary>
        /// This end point returns all the notes for specific user and location
        /// </summary>
        /// <param name="user"></param>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        [HttpGet("{user}")]
        public ActionResult Get(string user, [FromQuery]string lat, [FromQuery] string lng)
        {
            Position position = new Position
            {
                Latitude = Convert.ToDouble(lat),
                Longitude = Convert.ToDouble(lng)
            };
            IEnumerable<Note> notes = noteRepository.GetNotes(user, position);
            if (notes.Any())
            {
                return Ok(notes);
            }
            return NotFound();
        }

        /// <summary>
        /// This endpoint post a new note to repository and save it into database.
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] Note note)
        {

            Note insertedNote = noteRepository.Add(note);
            if (insertedNote != null)
            {
                return new CreatedResult("api/post", insertedNote);
            }
            return new BadRequestObjectResult("Something went wrong");

        }

        /// <summary>
        /// This endpoint returns search result based on search query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("search/{query}")]
        public ActionResult Search(string query)
        {
            IEnumerable<Note> notes = noteRepository.Search(query);
            if (notes.Any())
            {
                return Ok(notes);
            }
            return NotFound(query);
        }


    }
}
