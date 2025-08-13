using System;
using System.Collections.Generic;

namespace WarehouseManagement
{
    public class WareHouseManager
    {
        private readonly InventoryRepository<ElectronicItem> _electronics = new();
        private readonly InventoryRepository<GroceryItem> _groceries = new();

        public void SeedData()
        {
            // Electronics
            _electronics.AddItem(new ElectronicItem(1, "Laptop", 10, "Dell", 24));
            _electronics.AddItem(new ElectronicItem(2, "Smartphone", 25, "Samsung", 12));
            _electronics.AddItem(new ElectronicItem(3, "Headphones", 15, "Sony", 6));

            // Groceries
            _groceries.AddItem(new GroceryItem(101, "Apples", 50, DateTime.Now.AddDays(7)));
            _groceries.AddItem(new GroceryItem(102, "Milk", 20, DateTime.Now.AddDays(5)));
            _groceries.AddItem(new GroceryItem(103, "Bread", 30, DateTime.Now.AddDays(2)));
        }

        public void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
        {
            var items = repo.GetAllItems();
            if (items.Count == 0)
            {
                Console.WriteLine("No items found.");
                return;
            }

            foreach (var item in items)
                Console.WriteLine(item);
        }

        public void IncreaseStock<T>(InventoryRepository<T> repo, int id, int quantity) where T : IInventoryItem
        {
            try
            {
                var item = repo.GetItemById(id);
                repo.UpdateQuantity(id, item.Quantity + quantity);
                Console.WriteLine($"Stock updated for item Id {id}. New Qty: {item.Quantity}");
            }
            catch (Exception ex) when (ex is ItemNotFoundException || ex is InvalidQuantityException)
            {
                Console.WriteLine($"Error updating stock: {ex.Message}");
            }
        }

        public void RemoveItemById<T>(InventoryRepository<T> repo, int id) where T : IInventoryItem
        {
            try
            {
                repo.RemoveItem(id);
                Console.WriteLine($"Item Id {id} removed successfully.");
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"Error removing item: {ex.Message}");
            }
        }

        public void TestExceptionScenarios()
        {
            Console.WriteLine("\n=== Testing Exception Scenarios ===");

            try
            {
                _electronics.AddItem(new ElectronicItem(1, "Tablet", 5, "Apple", 12)); // duplicate
            }
            catch (DuplicateItemException ex)
            {
                Console.WriteLine($"Duplicate test: {ex.Message}");
            }

            try
            {
                _groceries.RemoveItem(999); // non-existent
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"Not found test: {ex.Message}");
            }

            try
            {
                _electronics.UpdateQuantity(2, -5); // invalid quantity
            }
            catch (InvalidQuantityException ex)
            {
                Console.WriteLine($"Invalid qty test: {ex.Message}");
            }
        }

        public void Run()
        {
            SeedData();

            Console.WriteLine("\n=== Grocery Items ===");
            PrintAllItems(_groceries);

            Console.WriteLine("\n=== Electronic Items ===");
            PrintAllItems(_electronics);

            // Exception scenarios
            TestExceptionScenarios();
        }
    }
}
