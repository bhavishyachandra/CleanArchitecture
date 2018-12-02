using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.SharedKernel;

namespace CleanArchitecture.Core.Events
{
    public class EntryAddedEvent : BaseDomainEvent
    {
        public int Id { get; }
        public GuestBookEntry Entry { get; }

        public EntryAddedEvent(int id, GuestBookEntry entry)
        {
            this.Id = id;
            this.Entry = entry;
        }
    }
}