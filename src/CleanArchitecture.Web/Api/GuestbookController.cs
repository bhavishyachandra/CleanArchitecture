using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Api
{
    [Route("api/[controller]")]
    public class GuestbookController : Controller
    {
        private readonly IRepository repository;

        public GuestbookController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetGuestbookById(int id)
        {
            var guestBook = repository.GetById<GuestBook>(id);
            if (guestBook != null)
            {
                var entries = repository.List<GuestBookEntry>();
                guestBook.Entries.Clear();
                guestBook.Entries.AddRange(entries);
                return Ok(guestBook);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("{id:int}/NewEntry")]
        public IActionResult NewEntry(int id, [FromBody] GuestBookEntry entry)
        {
            var guestbook = repository.GetById<GuestBook>(id);
            if(guestbook == null)
            {
                return NotFound();
            }
            var entries = repository.List<GuestBookEntry>();
            guestbook.Entries.Clear();
            guestbook.Entries.AddRange(entries);
            guestbook.AddEntry(entry);
            repository.Update(guestbook);
            return Ok(guestbook);
        }
    }
}