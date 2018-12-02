using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CleanArchitecture.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repository;
        private readonly IMessageSender messageSender;

        public HomeController(IRepository repository, IMessageSender messageSender)
        {
            _repository = repository;
            this.messageSender = messageSender;
        }

        public IActionResult Index()
        {
            if (!_repository.List<GuestBook>().Any())
            {
                var newGuestBook = new GuestBook()
                {
                    Name = "My guestbook"
                };
                newGuestBook.Entries.Add(new GuestBookEntry()
                {
                    EmailAddress = "steve@deviq.com",
                    Message = "hi!",
                    DateTimeCreated = DateTime.UtcNow.AddHours(-2)
                });

                newGuestBook.Entries.Add(new GuestBookEntry()
                {
                    EmailAddress = "mark@deviq.com",
                    Message = "hi again!",
                    DateTimeCreated = DateTime.UtcNow.AddHours(-1)
                });

                newGuestBook.Entries.Add(new GuestBookEntry()
                {
                    EmailAddress = "michelle@deviq.com",
                    Message = "hello!"
                });
                _repository.Add(newGuestBook);
            }

            var guestBook = _repository.GetById<GuestBook>(1);
            var guestBookEntries = _repository.List<GuestBookEntry>();
            guestBook.Entries.Clear();

            guestBook.Entries.AddRange(guestBookEntries);

            var viewModel = new HomePageViewModel
            {
                GuestBookName = guestBook.Name,
                PreviousEntries = guestBook.Entries
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(HomePageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var guestBook = _repository.GetById<GuestBook>(1);
                var guestBookEntries = _repository.List<GuestBookEntry>();
                guestBook.Entries.Clear();
                guestBook.Entries.AddRange(guestBookEntries);
                foreach (var entry in guestBook.Entries)
                {
                    messageSender.SendGuestBookNotificationEmail(entry.EmailAddress, entry.Message);
                }
                guestBook.Entries.Add(model.NewEntry);
                _repository.Update(guestBook);

                model.PreviousEntries.Clear();
                model.PreviousEntries.AddRange(guestBook.Entries);
            }
            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
