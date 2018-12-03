using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace CleanArchitecture.Web
{
    public static class SeedData
    {
        public static void PopulateTestData(AppDbContext dbContext)
        {
            var toDos = dbContext.ToDoItems;
            foreach (var item in toDos)
            {
                dbContext.Remove(item);
            }
            dbContext.SaveChanges();
            dbContext.ToDoItems.Add(new ToDoItem()
            {
                Title = "Test Item 1",
                Description = "Test Description One"
            });
            dbContext.ToDoItems.Add(new ToDoItem()
            {
                Title = "Test Item 2",
                Description = "Test Description Two"
            });
            dbContext.SaveChanges();

            var guestBook = new GuestBook()
            {
                Name = "Test Guestbook",
                Id = 1
            };

            dbContext.GuestBooks.Add(guestBook);

            guestBook.Entries.Add(new GuestBookEntry()
            {
                EmailAddress = "test@test.com",
                Message = "test message"
            });

            dbContext.SaveChanges();
        }

    }
}
