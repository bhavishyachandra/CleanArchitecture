using CleanArchitecture.Core.Events;
using CleanArchitecture.Core.SharedKernel;
using System;
using System.Collections.Generic;

namespace CleanArchitecture.Core.Entities
{
    public class GuestBook : BaseEntity
    {
        public string Name { get; set; }
        public List<GuestBookEntry> Entries { get; } = new List<GuestBookEntry>();
        public void AddEntry(GuestBookEntry entry)
        {
            Entries.Add(entry);
            Events.Add(new EntryAddedEvent(Id, entry));
        }
    }

    public class GuestBookEntry : BaseEntity
    {
        public string EmailAddress { get; set; }
        public string Message { get; set; }
        public DateTimeOffset DateTimeCreated { get; set; } = DateTime.UtcNow;
    }
}
