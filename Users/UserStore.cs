using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace mySLMS_OOP_System.Services
{
    public static class UserStore
    {
        private const string FileName = "users.json";

        public class StoredUser
        {
            public int Id { get; set; }
            public string Username { get; set; } = "";
            public string Password { get; set; } = "";
            public string Name { get; set; } = "";

            // NEW: Persist borrowed books per user
            public List<int> BorrowedBookIds { get; set; } = new List<int>();
        }

        public static List<StoredUser> LoadUsers()
        {
            try
            {
                if (!File.Exists(FileName))
                    return new List<StoredUser>();

                string json = File.ReadAllText(FileName);
                return JsonSerializer.Deserialize<List<StoredUser>>(json) ?? new List<StoredUser>();
            }
            catch
            {
                return new List<StoredUser>();
            }
        }

        public static void SaveUsers(List<StoredUser> users)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(users, options);
            File.WriteAllText(FileName, json);
        }

        public static int NextId(List<StoredUser> users)
        {
            if (users.Count == 0) return 100;
            return users.Max(u => u.Id) + 1;
        }
    }
}