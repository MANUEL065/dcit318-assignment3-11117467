using System;
using FinanceManagement;       // Q1
using HealthCareManagement;    // Q2
using WarehouseManagement;     // Q3
using SchoolGradingSystem;     // Q4 - this was missing

namespace Assignment3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Assignment Runner ===");
            Console.WriteLine("1. Finance Management System");
            Console.WriteLine("2. Health Care System");
            Console.WriteLine("3. Warehouse Inventory Management");
            Console.WriteLine("4. School Grading System");
            Console.Write("Enter choice: ");
            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    new FinanceApp().Run();
                    break;

                case "2":
                    new HealthCareApp().Run();
                    break;

                case "3":
                    new WareHouseManager().Run();
                    break;

                case "4":
                    var resultProcessor = new StudentResultProcessor();
                    resultProcessor.Run();
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
