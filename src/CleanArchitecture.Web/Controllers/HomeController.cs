using CleanArchitecture.Core.Entities;
using CleanArchitecture.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CleanArchitecture.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var guestBook = new GuestBook()
            {
                Name = "My guestbook"
            };
            guestBook.Entries.Add(new GuestBookEntry()
            {
                EmailAddress = "steve@deviq.com",
                Message = "hi!",
                DateTimeCreated = DateTime.UtcNow.AddHours(-2)
            });

            guestBook.Entries.Add(new GuestBookEntry()
            {
                EmailAddress = "mark@deviq.com",
                Message = "hi again!",
                DateTimeCreated = DateTime.UtcNow.AddHours(-1)
            });

            guestBook.Entries.Add(new GuestBookEntry()
            {
                EmailAddress = "michelle@deviq.com",
                Message = "hello!"
            });

            var viewModel = new HomePageViewModel();
            viewModel.GuestBookName = guestBook.Name;
            viewModel.PreviousEntries = guestBook.Entries;
            return View(viewModel);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
