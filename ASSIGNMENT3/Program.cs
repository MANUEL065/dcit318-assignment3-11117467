using System;

namespace FinanceManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Assignment Runner ===");
            Console.WriteLine("1. Finance Management System");
            Console.WriteLine("2. Another Assignment (placeholder)");
            Console.Write("Enter choice: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    new FinanceApp().Run();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
