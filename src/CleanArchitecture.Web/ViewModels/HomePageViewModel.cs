using CleanArchitecture.Core.Entities;
using System.Collections.Generic;

namespace CleanArchitecture.Web.ViewModels
{
    public class HomePageViewModel
    {
        public string GuestBookName { get; set; }
        public List<GuestBookEntry> PreviousEntries { get; set; } = new List<GuestBookEntry>();
        public GuestBookEntry NewEntry { get; set; }
    }
}
