using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CleanArchitecture.Core.Specifications
{
    public class GuestbookNotificationPolicy : ISpecification<GuestBookEntry>
    {
        public GuestbookNotificationPolicy(int entryAddedId = 0)
        {
            Criteria = e => e.DateTimeCreated > DateTimeOffset.UtcNow.AddDays(-1) && e.Id != entryAddedId;
        }

        public Expression<Func<GuestBookEntry, bool>> Criteria { get; }
    }
}
