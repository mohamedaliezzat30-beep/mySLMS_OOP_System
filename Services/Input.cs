using System;

namespace mySLMS_OOP_System.Services
{
    public static class Input
    {
        public static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                ConsoleUI.Prompt(prompt);
                string? s = Console.ReadLine();

                if (int.TryParse(s, out int value) && value >= min && value <= max)
                    return value;

                ConsoleUI.Warn($"Enter a number between {min} and {max}.");
            }
        }

        public static string ReadNonEmpty(string prompt)
        {
            while (true)
            {
                ConsoleUI.Prompt(prompt);
                string text = (Console.ReadLine() ?? "").Trim();

                if (!string.IsNullOrWhiteSpace(text))
                    return text;

                ConsoleUI.Warn("This field cannot be empty.");
            }
        }
    }
}