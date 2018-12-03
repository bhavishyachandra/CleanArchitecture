using CleanArchitecture.Core.Events;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Specifications;
using System.Linq;

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
            var notificationPolicy = new GuestbookNotificationPolicy(entryAddedEvent.Entry.Id);
            var emailsToNotify = repository.List(notificationPolicy).Select(e => e.EmailAddress);

            foreach (var emailAddress in emailsToNotify)
            {
                string messageBody = $"{emailAddress} left a new message {entryAddedEvent.Entry.Message}";
                messageSender.SendGuestBookNotificationEmail(emailAddress, messageBody);
            }
        }
    }
}
