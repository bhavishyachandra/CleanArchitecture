using CleanArchitecture.Core.SharedKernel;
using System;
using System.Collections.Generic;

namespace CleanArchitecture.Core.Entities
{
    public class GuestBook : BaseEntity
    {
        public string Name { get; set; }
        public List<GuestBookEntry> Entries { get; } = new List<GuestBookEntry>();
    }

    public class GuestBookEntry : BaseEntity
    {
        public string EmailAddress { get; set; }
        public string Message { get; set; }
        public DateTimeOffset DateTimeCreated { get; set; } = DateTime.UtcNow;
    }
}
