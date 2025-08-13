using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace InventoryManagement
{
    // Generic Inventory Logger
    public class InventoryLogger<T> where T : IInventoryEntity
    {
        private List<T> _log = new();
        private readonly string _filePath;

        public InventoryLogger(string filePath)
        {
            _filePath = filePath;
        }

        public void Add(T item) => _log.Add(item);

        public List<T> GetAll() => new List<T>(_log);

        public void SaveToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(_log, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
                Console.WriteLine("Data saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }

        public void LoadFromFile()
        {
            try
            {
                if (!File.Exists(_filePath))
                    throw new FileNotFoundException("Inventory data file not found.");

                string json = File.ReadAllText(_filePath);
                var items = JsonSerializer.Deserialize<List<T>>(json);
                _log = items ?? new List<T>();
                Console.WriteLine("Data loaded successfully.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }
    }

    // Main Inventory Application
    public class InventoryApp
    {
        private InventoryLogger<InventoryItem> _logger;

        public InventoryApp()
        {
            _logger = new InventoryLogger<InventoryItem>("inventory.json");
        }

        public void SeedSampleData()
        {
            _logger.Add(new InventoryItem(1, "Laptop", 10, DateTime.Now));
            _logger.Add(new InventoryItem(2, "Keyboard", 50, DateTime.Now));
            _logger.Add(new InventoryItem(3, "Mouse", 30, DateTime.Now));
            _logger.Add(new InventoryItem(4, "Monitor", 20, DateTime.Now));
            _logger.Add(new InventoryItem(5, "Printer", 5, DateTime.Now));
        }

        public void SaveData() => _logger.SaveToFile();

        public void LoadData() => _logger.LoadFromFile();

        public void PrintAllItems()
        {
            var items = _logger.GetAll();
            Console.WriteLine("\n--- Inventory Items ---");
            foreach (var item in items)
            {
                Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Qty: {item.Quantity}, Date Added: {item.DateAdded}");
            }
        }

        public void Run()
        {
            // Seed and save data
            SeedSampleData();
            SaveData();

            // Simulate new session
            _logger = new InventoryLogger<InventoryItem>("inventory.json");
            LoadData();
            PrintAllItems();
        }
    }
}
