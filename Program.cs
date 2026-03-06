using System;
using System.Collections.Generic;
using mySLMS_OOP_System.Services;
using mySLMS_OOP_System.Users;

namespace mySLMS_OOP_System
{
    internal class Program
    {
        private const string AdminUsername = "admin";
        private const string AdminPassword = "admin123";

        static void Main()
        {
            var library = new Library();
            library.SeedDemoData();

            // Load saved users
            List<UserStore.StoredUser> savedUsers = UserStore.LoadUsers();

            while (true)
            {
                ConsoleUI.Header("Smart Library Management System (SLMS)");

                Console.WriteLine("1) Login");
                Console.WriteLine("0) Exit");

                int choice = Input.ReadInt("Choice: ", 0, 1);

                if (choice == 0)
                {
                    ConsoleUI.Ok("System closed.");
                    break;
                }

                User? loggedIn = LoginOrRegister(savedUsers);

                if (loggedIn == null)
                    continue;

                ConsoleUI.Ok($"Welcome, {loggedIn.Name}!");

                bool stay = true;

                while (stay)
                {
                    loggedIn.ShowMenu(library);
                    stay = loggedIn.HandleChoice(library);
                }

                ConsoleUI.Warn("Logged out.");
            }
        }

        private static User? LoginOrRegister(List<UserStore.StoredUser> savedUsers)
        {
            ConsoleUI.SubHeader("Login");

            string username = Input.ReadNonEmpty("Username: ");
            string password = Input.ReadNonEmpty("Password: ");

            // Admin login
            if (username.Equals(AdminUsername, StringComparison.OrdinalIgnoreCase))
            {
                if (password == AdminPassword)
                {
                    ConsoleUI.Ok("Admin login successful.");
                    return new Admin(1, "System Admin");
                }

                ConsoleUI.Error("Admin login failed.");
                return null;
            }

            // Check if user exists
            var existing = savedUsers.Find(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (existing != null)
            {
                if (existing.Password != password)
                {
                    ConsoleUI.Error("Wrong password.");
                    return null;
                }

                ConsoleUI.Ok("Login successful.");
                return new Member(existing, savedUsers);
            }

            // Create new account
            ConsoleUI.Warn("User not found — creating a new account.");

            string name = Input.ReadNonEmpty("Enter your display name: ");

            int newId = UserStore.NextId(savedUsers);

            var created = new UserStore.StoredUser
            {
                Id = newId,
                Username = username,
                Password = password,
                Name = name,
                BorrowedBookIds = new List<int>()
            };

            savedUsers.Add(created);
            UserStore.SaveUsers(savedUsers);

            ConsoleUI.Ok("Account created successfully.");

            return new Member(created, savedUsers);
        }
    }
}