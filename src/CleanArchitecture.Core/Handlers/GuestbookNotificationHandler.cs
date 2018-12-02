using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Events;
using CleanArchitecture.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanArchitecture.Core.Handlers
{
    public class GuestbookNotificationHandler : IHandle<EntryAddedEvent>
    {
        private readonly IRepository repository;
        private readonly IMessageSender messageSender;

        public GuestbookNotificationHandler(IRepository repository, IMessageSender messageSender)
        {
            this.repository = repository;
            this.messageSender = messageSender;
        }

        public void Handle(EntryAddedEvent entryAddedEvent)
        {
            var guestbook = repository.GetById<GuestBook>(entryAddedEvent.Id);

            var emailsToNotify = guestbook.Entries.Where(e => e.DateTimeCreated > DateTimeOffset.UtcNow.AddDays(-1) && e.Id != entryAddedEvent.Entry.Id).Select(e => e.EmailAddress);

            foreach(var emailAddress in emailsToNotify)
            {
                string messageBody = $"{entryAddedEvent.Entry.EmailAddress} left a new message {entryAddedEvent.Entry.Message}";
                messageSender.SendGuestBookNotificationEmail(entryAddedEvent.Entry.EmailAddress, messageBody);
            }
        }
    }
}
