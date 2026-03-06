using System;
using System.Collections.Generic;
using System.Linq;
using mySLMS_OOP_System.Services;

namespace mySLMS_OOP_System.Users
{
    public class Member : User
    {
        private readonly UserStore.StoredUser profile;
        private readonly List<UserStore.StoredUser> allUsers;

        private readonly HashSet<int> borrowedBookIds;
        private const int BorrowLimit = 5;

        // NEW: Member is built from the saved user record (so it persists)
        public Member(UserStore.StoredUser profile, List<UserStore.StoredUser> allUsers)
            : base(profile.Id, profile.Name)
        {
            this.profile = profile;
            this.allUsers = allUsers;
            borrowedBookIds = new HashSet<int>(profile.BorrowedBookIds ?? new List<int>());
        }

        public override void ShowMenu(Library lib)
        {
            Console.WriteLine($"\n(Member) {Name} | Day {lib.CurrentDay}");
            Console.WriteLine($"Borrowed: {borrowedBookIds.Count}/{BorrowLimit}");
            Console.WriteLine("1) View all books");
            Console.WriteLine("2) Search books");
            Console.WriteLine("3) Borrow book (choose from list)");
            Console.WriteLine("4) Return book (choose from my list)");
            Console.WriteLine("5) Reserve book (only if currently Borrowed)");
            Console.WriteLine("6) My borrowed list");
            Console.WriteLine("0) Logout");
            Console.Write("Choice: ");
        }

        public override bool HandleChoice(Library lib)
        {
            string? choice = Console.ReadLine();
            if (choice == "0") return false;

            switch (choice)
            {
                case "1":
                    Console.WriteLine("\n--- Catalogue ---");
                    foreach (var b in lib.AllBooks())
                        Console.WriteLine(b.DisplayLine(lib.CurrentDay));
                    break;

                case "2":
                    Console.Write("Search query: ");
                    var q = Console.ReadLine() ?? "";
                    var results = lib.Search(q);
                    Console.WriteLine($"\nResults ({results.Count}):");
                    foreach (var b in results)
                        Console.WriteLine(b.DisplayLine(lib.CurrentDay));
                    break;

                case "3":
                    BorrowFlow(lib);
                    break;

                case "4":
                    ReturnFlow(lib);
                    break;

                case "5":
                    ReserveFlow(lib);
                    break;

                case "6":
                    Console.WriteLine("\nMy borrowed books:");
                    if (borrowedBookIds.Count == 0) Console.WriteLine("(none)");
                    foreach (var id in borrowedBookIds)
                    {
                        var b = lib.FindById(id);
                        Console.WriteLine(b == null ? $"- {id}" : $"- {id}: {b.Title}");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            return true;
        }

        private void BorrowFlow(Library lib)
        {
            if (borrowedBookIds.Count >= BorrowLimit)
            {
                Console.WriteLine($"Borrow denied: limit reached ({BorrowLimit}). Return something first.");
                return;
            }

            lib.PrintBookSelectionList();
            Console.Write("Enter selection number: ");

            if (!int.TryParse(Console.ReadLine(), out int selection))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            int? bookId = lib.GetBookIdFromSelection(selection);
            if (bookId == null)
            {
                Console.WriteLine("Invalid selection number.");
                return;
            }

            // REQUIRED RULE: cannot borrow the same book twice
            if (borrowedBookIds.Contains(bookId.Value))
            {
                Console.WriteLine("You already have this book borrowed.");
                return;
            }

            var result = lib.BorrowBook(Id, bookId.Value);
            Console.WriteLine(result.Message);

            if (result.Success)
            {
                borrowedBookIds.Add(bookId.Value);
                SaveBorrowedState();
            }
        }

        private void ReturnFlow(Library lib)
        {
            if (borrowedBookIds.Count == 0)
            {
                Console.WriteLine("You have no borrowed books to return.");
                return;
            }

            Console.WriteLine("\n--- Your Borrowed Books ---");
            var borrowedList = borrowedBookIds.ToList();

            for (int i = 0; i < borrowedList.Count; i++)
            {
                var b = lib.FindById(borrowedList[i]);
                string title = b?.Title ?? "Unknown Book";
                Console.WriteLine($"{i + 1}) {title} (ID: {borrowedList[i]})");
            }

            Console.Write("Choose which one to return: ");

            if (!int.TryParse(Console.ReadLine(), out int selection) ||
                selection < 1 || selection > borrowedList.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            int bookId = borrowedList[selection - 1];
            var result = lib.ReturnBook(Id, bookId);
            Console.WriteLine(result.Message);

            if (result.Success)
            {
                borrowedBookIds.Remove(bookId);
                SaveBorrowedState();
            }
        }

        private void ReserveFlow(Library lib)
        {
            lib.PrintBookSelectionList();
            Console.Write("Enter selection number to reserve: ");

            if (!int.TryParse(Console.ReadLine(), out int selection))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            int? bookId = lib.GetBookIdFromSelection(selection);
            if (bookId == null)
            {
                Console.WriteLine("Invalid selection number.");
                return;
            }

            var result = lib.ReserveBook(Id, bookId.Value);
            Console.WriteLine(result.Message);
        }

        private void SaveBorrowedState()
        {
            profile.BorrowedBookIds = borrowedBookIds.OrderBy(x => x).ToList();
            UserStore.SaveUsers(allUsers);
        }
    }
}