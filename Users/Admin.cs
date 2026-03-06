using System;
using mySLMS_OOP_System.Models;
using mySLMS_OOP_System.Services;

namespace mySLMS_OOP_System.Users
{
    public class Admin : User
    {
        public Admin(int id, string name) : base(id, name) { }

        public override void ShowMenu(Library lib)
        {
            Console.WriteLine($"\n(Admin) {Name} | Day {lib.CurrentDay}");
            Console.WriteLine("1) View all books");
            Console.WriteLine("2) Add a book");
            Console.WriteLine("3) Search books");
            Console.WriteLine("4) Overdue report");
            Console.WriteLine("5) Advance time (days)");
            Console.WriteLine("6) Library summary report");
            Console.WriteLine("0) Logout");
            Console.Write("Choice: ");
        }

        public override bool HandleChoice(Library lib)
        {
            string? c = Console.ReadLine();
            if (c == "0") return false;

            switch (c)
            {
                case "1":
                    Console.WriteLine("\n--- Catalogue ---");
                    foreach (var b in lib.AllBooks())
                        Console.WriteLine(b.DisplayLine(lib.CurrentDay));
                    break;

                case "2":
                    AddBookFlow(lib);
                    break;

                case "3":
                    Console.Write("Search query: ");
                    var q = Console.ReadLine() ?? "";
                    var results = lib.Search(q);
                    Console.WriteLine($"\nResults ({results.Count}):");
                    foreach (var b in results)
                        Console.WriteLine(b.DisplayLine(lib.CurrentDay));
                    break;

                case "4":
                    var overdue = lib.OverdueBooks();
                    Console.WriteLine($"\n--- Overdue Report (count={overdue.Count}) ---");
                    if (overdue.Count == 0) Console.WriteLine("No overdue books.");
                    foreach (var b in overdue)
                        Console.WriteLine(b.DisplayLine(lib.CurrentDay));
                    break;

                case "5":
                    Console.Write("Advance by how many days? ");
                    if (int.TryParse(Console.ReadLine(), out int days))
                    {
                        lib.AdvanceDays(days);
                        Console.WriteLine($"Day is now {lib.CurrentDay}.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid number.");
                    }
                    break;

                case "6":
                    var s = lib.Summary();
                    Console.WriteLine("\n--- Library Summary ---");
                    Console.WriteLine($"Total:     {s.Total}");
                    Console.WriteLine($"Available: {s.Available}");
                    Console.WriteLine($"Borrowed:  {s.Borrowed}");
                    Console.WriteLine($"Reserved:  {s.Reserved}");
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            return true;
        }

        private void AddBookFlow(Library lib)
        {
            Console.Write("New book ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            Console.Write("Title: ");
            string title = Console.ReadLine() ?? "";

            Console.Write("Author: ");
            string author = Console.ReadLine() ?? "";

            try
            {
                lib.AddBook(new Book(id, title, author));
                Console.WriteLine("Book added.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Add failed: {ex.Message}");
            }
        }
    }
}