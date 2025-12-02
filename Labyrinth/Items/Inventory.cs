using System.Diagnostics.CodeAnalysis;

namespace Labyrinth.Items
{
    /// <summary>
    /// Inventory of collectable items for rooms and players.
    /// </summary>
    /// <param name="item">Optional initial item in the inventory.</param>
    public abstract class Inventory(ICollectable? item = null)
    {
        /// <summary>
        /// True if the room has an item, false otherwise.
        /// </summary>
        public bool HasItems => _items.Count > 0;

        /// <summary>
        /// Gets the type of the item in the room.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the room has no item (check with
        /// <see cref="HasItems"/>).</exception>
        public IEnumerable<Type> ItemTypes => HasItems
            ? _items.Select(item => item.GetType())
            : throw new InvalidOperationException("No item in the inventory");

        /// <summary>
        /// Places an item in the inventory, removing it from another one.
        /// </summary>
        /// <param name="from">The inventory from which the item is taken. The item is removed from this inventory.</param>
        /// <param name="nth">Index of the item to take from the source inventory.</param>
        /// <exception cref="InvalidOperationException">Thrown if the source inventory has no item (check with <see cref="HasItems"/>).</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the requested item index is outside the source inventory.</exception>
        public void MoveItemFrom(Inventory from, int nth = 0)
        {
            if (!from.HasItems)
            {
                throw new InvalidOperationException("No item to take from the source inventory");
            }
            if (nth < 0 || nth >= from._items.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(nth), "No item to take at the specified index");
            }
            _items.Add(from._items[nth]);
            from._items.RemoveAt(nth);
        }

        /// <summary>
        /// Swaps items between inventories (if any)
        /// </summary>
        /// <param name="from">The inventory to swap items with.</param>
        public void SwapItems(Inventory from)
        {
            (_items, from._items) = (from._items, _items);
        }

        protected List<ICollectable> _items = item is null ? [] : new List<ICollectable> { item };
    }
}
