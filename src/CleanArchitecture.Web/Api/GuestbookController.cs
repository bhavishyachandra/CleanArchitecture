using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Web.Filters;
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
        [VerifyGuestBookExists]
        public IActionResult GetGuestbookById(int id)
        {
            var guestBook = repository.GetById<GuestBook>(id);
            var entries = repository.List<GuestBookEntry>();
            guestBook.Entries.Clear();
            guestBook.Entries.AddRange(entries);
            return Ok(guestBook);

        }

        [HttpPost("{id:int}/NewEntry")]
        [VerifyGuestBookExists]
        public IActionResult NewEntry(int id, [FromBody] GuestBookEntry entry)
        {
            var guestbook = repository.GetById<GuestBook>(id);
            var entries = repository.List<GuestBookEntry>();
            guestbook.Entries.Clear();
            guestbook.Entries.AddRange(entries);
            guestbook.AddEntry(entry);
            repository.Update(guestbook);
            return Ok(guestbook);
        }
    }
}