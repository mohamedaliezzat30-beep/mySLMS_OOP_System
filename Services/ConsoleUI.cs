using System;

namespace mySLMS_OOP_System.Services
{
    public static class ConsoleUI
    {
        public static void Header(string title)
        {
            Console.WriteLine();
            Console.WriteLine(new string('=', 50));
            Console.WriteLine(title);
            Console.WriteLine(new string('=', 50));
        }

        public static void SubHeader(string title)
        {
            Console.WriteLine();
            Console.WriteLine($"--- {title} ---");
        }

        public static void Ok(string message)
        {
            Console.WriteLine($"[OK] {message}");
        }

        public static void Warn(string message)
        {
            Console.WriteLine($"[WARN] {message}");
        }

        public static void Error(string message)
        {
            Console.WriteLine($"[ERROR] {message}");
        }

        public static void Prompt(string message)
        {
            Console.Write(message);
        }
    }
}