using System;
using FinanceManagement;    // your Q1 namespace
using HealthCareManagement; // Q2 namespace

namespace Assignment3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Assignment Runner ===");
            Console.WriteLine("1. Finance Management System");
            Console.WriteLine("2. Health Care System");
            Console.Write("Enter choice: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    new FinanceApp().Run();
                    break;
                case "2":
                    new HealthCareApp().Run();
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
