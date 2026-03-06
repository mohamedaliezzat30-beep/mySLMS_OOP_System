using System;
using System.Collections.Generic;
using System.Linq;
using mySLMS_OOP_System.Models;

namespace mySLMS_OOP_System.Services
{
    public record OperationResult(bool Success, string Message);

    public class Library
    {
        private readonly List<Book> books = new();

        public int CurrentDay { get; private set; } = 0;

        public void SeedDemoData()
        {
            AddBook(new Book(1, "To Kill a Mockingbird", "Harper Lee"));
            AddBook(new Book(2, "1984", "George Orwell"));
            AddBook(new Book(3, "Pride and Prejudice", "Jane Austen"));
            AddBook(new Book(4, "The Great Gatsby", "F. Scott Fitzgerald"));
            AddBook(new Book(5, "The Catcher in the Rye", "J.D. Salinger"));
            AddBook(new Book(6, "The Hobbit", "J.R.R. Tolkien"));
            AddBook(new Book(7, "Harry Potter and the Philosopher’s Stone", "J.K. Rowling"));
            AddBook(new Book(8, "The Fellowship of the Ring", "J.R.R. Tolkien"));
            AddBook(new Book(9, "The Alchemist", "Paulo Coelho"));
            AddBook(new Book(10, "The Da Vinci Code", "Dan Brown"));
        }

        public void AddBook(Book book)
        {
            if (books.Any(b => b.Id == book.Id))
                throw new InvalidOperationException("Book ID already exists.");
            books.Add(book);
        }

        public IReadOnlyList<Book> AllBooks() => books;

        public Book? FindById(int id) => books.FirstOrDefault(b => b.Id == id);

        public List<Book> Search(string query)
        {
            query = (query ?? "").Trim();
            if (query.Length == 0) return new List<Book>();

            return books
                .Where(b =>
                    b.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    b.Author.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // --- Selection list (choose 1..N) ---
        public void PrintBookSelectionList()
        {
            Console.WriteLine("\n--- Choose a book ---");
            for (int i = 0; i < books.Count; i++)
            {
                var b = books[i];
                Console.WriteLine($"{i + 1}) {b.Title} — {b.Author} [{b.Status}]");
            }
            Console.WriteLine("---------------------");
        }

        public int? GetBookIdFromSelection(int selectionNumber)
        {
            if (selectionNumber < 1 || selectionNumber > books.Count) return null;
            return books[selectionNumber - 1].Id;
        }

        // --- Professional: specific result messages ---
        public OperationResult BorrowBook(int memberId, int bookId)
        {
            var book = FindById(bookId);
            if (book == null)
                return new OperationResult(false, "Invalid book.");

            if (!book.CanBeBorrowedBy(memberId))
            {
                if (book.Status == BookStatus.Borrowed)
                    return new OperationResult(false, "This book is currently borrowed by another member.");
                if (book.Status == BookStatus.Reserved && book.ReservedById != memberId)
                    return new OperationResult(false, "This book is reserved for another member.");
                return new OperationResult(false, "This book is not available.");
            }

            bool ok = book.Borrow(memberId, CurrentDay);
            if (!ok)
                return new OperationResult(false, "Borrow failed.");

            return new OperationResult(true, $"Borrowed successfully. Due on Day {book.DueDay}.");
        }

        public OperationResult ReturnBook(int memberId, int bookId)
        {
            var book = FindById(bookId);
            if (book == null)
                return new OperationResult(false, "Invalid book.");

            if (book.Status != BookStatus.Borrowed)
                return new OperationResult(false, "This book is not currently borrowed.");

            if (book.BorrowerId != memberId)
                return new OperationResult(false, "You can only return books you borrowed.");

            bool ok = book.Return(memberId, CurrentDay);
            if (!ok)
                return new OperationResult(false, "Return failed.");

            if (book.Status == BookStatus.Reserved)
                return new OperationResult(true, $"Returned. Book is now reserved for pickup until Day {book.PickupUntilDay}.");

            return new OperationResult(true, "Returned successfully. Book is now available.");
        }

        public OperationResult ReserveBook(int memberId, int bookId)
        {
            var book = FindById(bookId);
            if (book == null)
                return new OperationResult(false, "Invalid book.");

            if (book.Status != BookStatus.Borrowed)
                return new OperationResult(false, "You can only reserve books that are currently borrowed.");

            if (book.ReservedById.HasValue)
                return new OperationResult(false, "This book already has a reservation.");

            bool ok = book.Reserve(memberId);
            return ok
                ? new OperationResult(true, "Reservation placed. Pickup window starts when the book is returned (expires after 3 days).")
                : new OperationResult(false, "Reservation failed.");
        }

        public List<Book> OverdueBooks() =>
            books.Where(b => b.IsOverdue(CurrentDay)).ToList();

        // NEW: Admin summary report
        public (int Total, int Available, int Borrowed, int Reserved) Summary()
        {
            int total = books.Count;
            int available = books.Count(b => b.Status == BookStatus.Available);
            int borrowed = books.Count(b => b.Status == BookStatus.Borrowed);
            int reserved = books.Count(b => b.Status == BookStatus.Reserved);
            return (total, available, borrowed, reserved);
        }

        public void AdvanceDays(int days)
        {
            if (days <= 0) return;

            CurrentDay += days;

            foreach (var b in books)
                b.ExpireReservationIfNeeded(CurrentDay);
        }
    }
}