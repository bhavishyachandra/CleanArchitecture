using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Tests
{
    public class GuestBookNotificationPolicyShould
    {
        private readonly GuestbookNotificationPolicy notificationPolicy;

        public GuestBookNotificationPolicyShould()
        {
            notificationPolicy = new GuestbookNotificationPolicy(1);
        }

        [Fact]
        public void PickEmailsWithNewIds()
        {
            var guestbookEntries = new List<GuestBookEntry>()
            {
                new GuestBookEntry()
                {
                    Id = 1,
                    EmailAddress = "test1@test1.com",
                    DateTimeCreated = DateTime.Now.AddDays(0),
                    Message = "test1"
                },

                new GuestBookEntry()
                {
                    Id = 2,
                    EmailAddress = "test2@test2.com",
                    DateTimeCreated = DateTime.Now.AddHours(-1),
                    Message = "test2"
                },
                new GuestBookEntry()
                {
                    Id = 3,
                    EmailAddress = "test3@test3.com",
                    DateTimeCreated = DateTime.Now.AddDays(-2),
                    Message = "test3"
                }
            };
            var actualOutput = guestbookEntries.AsQueryable().Where(notificationPolicy.Criteria);
            Assert.Equal(1, actualOutput.Count());
        }
        
    }
}
