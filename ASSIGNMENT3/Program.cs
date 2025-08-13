using System;
using FinanceManagement;      // Q1
using HealthCareManagement;   // Q2
using WarehouseManagement;    // Q3

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
                case "3":
                    new WareHouseManager().Run();
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
