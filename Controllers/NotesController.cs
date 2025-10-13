using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using notesy_api_c_sharp.Models;

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

        // get all notes

        [HttpGet]
        public ActionResult<IEnumerable<Note>> GetAll()
        {
            return Ok(Notes);
        }
    }
}
