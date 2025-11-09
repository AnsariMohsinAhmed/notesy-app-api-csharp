using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using notesy_api_c_sharp.Data;
using notesy_api_c_sharp.Models;
using System.Threading.Tasks;

namespace notesy_api_c_sharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private List<Note> Notes =
        [
            new Note { ID = 1, Text = "First note"},
            new Note { ID = 2, Text = "Second note"}
        ];

        private readonly AppDbContext _context;

        public NotesController(AppDbContext context)
        {
            _context = context;
        }



        // get all notes

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetAll()
        {
            List<Note> notes = await _context.Notes.ToListAsync();
            return Ok(notes);
        }

        // add a note
        [HttpPost]
        public async Task<ActionResult<Note>> AddNote([FromBody] Note note)
        {
            _context.Notes.Add(note); //syntax :- context.Table.Add(data);
            await _context.SaveChangesAsync();
            return Created(string.Empty, new { id = note.ID});
        }

        // update an existing note
        [HttpPut("{id}")]
        public async Task<ActionResult> updateNote(int id, [FromBody] Note note)
        {
            Note? existingNote = await _context.Notes.FindAsync(id);
            if (existingNote == null)
            {
                return NotFound(new { message = "Note not found" });
            }

            existingNote.Text = note.Text;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Note updated successfully!" });
        }

        // delete a note
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNote(int id)
        {
            Note? existingNote = await _context.Notes.FindAsync(id);
            if (existingNote == null)
            {
                return NotFound(new { message = "Note not found" });
            }

            _context.Notes.Remove(existingNote);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Note deleted successfully" });
        }
    }
}
