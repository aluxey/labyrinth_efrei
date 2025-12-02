using Labyrinth.Items;
using Labyrinth.Tiles;
using System.Collections.Generic;

namespace Labyrinth.Build
{
    /// <summary>
    /// Manage the creation of doors and key rooms ensuring each door has a corresponding key room.
    /// </summary>
    public sealed class Keymaster : IDisposable
    {
        /// <summary>
        /// Ensure all created doors have a corresponding key room and vice versa.
        /// </summary>
        /// <exception cref="InvalidOperationException">Some keys are missing or are not placed.</exception>
        public void Dispose()
        {
            if (unplacedKeys.HasItems || emptyKeyRooms.Count > 0)
            {
                throw new InvalidOperationException("Unmatched key/door creation");
            }
        }

        /// <summary>
        /// Create a new door and queue its key for placement in a key room.
        /// </summary>
        /// <returns>Created door</returns>
        public Door NewDoor()
        {
            var door = new Door();

            door.LockAndTakeKey(unplacedKeys);
            PlaceKeys();
            return door;
        }

        /// <summary>
        /// Create a new room for a key and place a queued key if available.
        /// </summary>
        /// <returns>Created key room</returns>
        public Room NewKeyRoom()
        {
            var room = new Room();
            emptyKeyRooms.Enqueue(room);
            PlaceKeys();
            return room;
        }

        private void PlaceKeys()
        {
            while (unplacedKeys.HasItems && emptyKeyRooms.Count > 0)
            {
                emptyKeyRooms.Dequeue().Pass().MoveItemFrom(unplacedKeys);
            }
        }

        private readonly MyInventory unplacedKeys = new();
        private readonly Queue<Room> emptyKeyRooms = new();
    }
}
