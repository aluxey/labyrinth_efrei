namespace Labyrinth.Items
{
    /// <summary>
    /// Inventory class that exposes the items it contains.
    /// </summary>
    /// <param name="item">Optional initial item in the inventory.</param>
    public class MyInventory(ICollectable? item = null) : Inventory(item)
    {
        /// <summary>
        /// Item in the inventory, or null if empty.
        /// </summary>
        public IEnumerable<ICollectable> Items => _items.AsReadOnly();
    }
}
