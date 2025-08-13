using System;

namespace InventoryManagement
{
    // Marker interface for all inventory entities
    public interface IInventoryEntity
    {
        int Id { get; }
    }

    // Immutable record for inventory items
    public record InventoryItem(int Id, string Name, int Quantity, DateTime DateAdded) : IInventoryEntity;
}
