// WarehouseModels.cs
using System;
using System.Collections.Generic;

namespace WarehouseManagement
{
    // === Marker Interface ===
    public interface IInventoryItem
    {
        int Id { get; }
        string Name { get; }
        int Quantity { get; set; }
    }

    // === Product Classes ===
    public class ElectronicItem : IInventoryItem
    {
        public int Id { get; }
        public string Name { get; }
        public int Quantity { get; set; }
        public string Brand { get; }
        public int WarrantyMonths { get; }

        public ElectronicItem(int id, string name, int quantity, string brand, int warrantyMonths)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Quantity = quantity;
            Brand = brand ?? throw new ArgumentNullException(nameof(brand));
            WarrantyMonths = warrantyMonths;
        }

        public override string ToString()
            => $"[Electronic] Id: {Id}, Name: {Name}, Qty: {Quantity}, Brand: {Brand}, Warranty: {WarrantyMonths} months";
    }

    public class GroceryItem : IInventoryItem
    {
        public int Id { get; }
        public string Name { get; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; }

        public GroceryItem(int id, string name, int quantity, DateTime expiryDate)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Quantity = quantity;
            ExpiryDate = expiryDate;
        }

        public override string ToString()
            => $"[Grocery] Id: {Id}, Name: {Name}, Qty: {Quantity}, Expiry: {ExpiryDate:yyyy-MM-dd}";
    }

    // === Custom Exceptions ===
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException(string message) : base(message) { }
    }

    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message) : base(message) { }
    }

    public class InvalidQuantityException : Exception
    {
        public InvalidQuantityException(string message) : base(message) { }
    }

    // === Generic Repository ===
    public class InventoryRepository<T> where T : IInventoryItem
    {
        private readonly Dictionary<int, T> _items = new();

        public void AddItem(T item)
        {
            if (_items.ContainsKey(item.Id))
                throw new DuplicateItemException($"Item with Id {item.Id} already exists.");
            _items[item.Id] = item;
        }

        public T GetItemById(int id)
        {
            if (!_items.TryGetValue(id, out var item))
                throw new ItemNotFoundException($"Item with Id {id} not found.");
            return item;
        }

        public void RemoveItem(int id)
        {
            if (!_items.Remove(id))
                throw new ItemNotFoundException($"Item with Id {id} not found.");
        }

        public List<T> GetAllItems() => new List<T>(_items.Values);

        public void UpdateQuantity(int id, int newQuantity)
        {
            if (newQuantity < 0)
                throw new InvalidQuantityException("Quantity cannot be negative.");

            var item = GetItemById(id);
            item.Quantity = newQuantity;
        }
    }
}
