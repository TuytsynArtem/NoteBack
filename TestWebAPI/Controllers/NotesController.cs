namespace TestWebAPI.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : Controller
    {
        private static List<NoteItem> Items;

        public int Mob { get; set; }

        public NotesController()
        {
            if (Items == null)
                Items = new List<NoteItem>();
        }

        #region Test

        [HttpGet("getall")]
        public object GetAll()
        {
            return Json(new[]
            {
                "note1",
                "note2"
            });
        }

        [HttpGet("massive")]
        public object massive()
        {
            int[] massive = new int[10];

            return Json(massive);
        }

        //5! == 1 * 2 * 3 * 4 * 5
        //5! == 5 * 4!
        //4! == 4 * 3!
        //3! == 3 * 2!
        //2! == 2 * 1!
        //1! == 1

        [HttpGet("factorial/{num}")]
        public object factorial(int num)
        {
            return Json(fact(num));
        }

        // bool ? true : false
        int fact(int num)
        {
            //      bool     ?     true              :  false
            return (num > 1) ? (num * fact(num - 1)) : (1);
        }

        #endregion

        [HttpGet("getNotes")]
        public object GetNotes()
        {
            return Items.ToArray();
        }

        [HttpPost("addNote")]
        public object AddNote([FromBody]NoteItem item)
        {
            Items.Add(item);
            return Ok();
        }

        [HttpDelete("delNotes")]
        public object DelNotes()
        {
            Items.Clear();
            return Ok();
        }

        [HttpDelete("delNote/{id}")]
        public object DelNote(int id)
        {
            var itemToRemove = Items.FirstOrDefault(x => x.Id == id);
            if (itemToRemove != null)
                Items.Remove(itemToRemove);
            return Ok();
        }

        [HttpGet("searchNote/{word}")]
        public object SearchNote(string word)
        {
            var wordforsearch = Items.FindAll(x => x.Title.Contains(word) || x.Body.Contains(word));
            return wordforsearch;
        }

        [HttpGet("someNote/{count}")]
        public object SomeNote(int count)
        {
            if (count <= 0)
                return BadRequest("Count must be positive");

            var notes = Items.Take(count);

            return notes;
        }

        [HttpGet("someNote/{count}/{skipCount}")]
        public object SomeNote(int count, int skipCount)
        {
            if (count <= 0)
                return BadRequest("Count must be positive");
            if (skipCount < 0)
                return BadRequest("Cannot skip negative count of notes");

            var notes = Items.Skip(skipCount).Take(count);

            return notes;
        }
    }
}