namespace CleanArchitecture.Core.Interfaces
{
    public interface IMessageSender
    {
        void SendGuestBookNotificationEmail(string emailAddress, string body);
    }
}
