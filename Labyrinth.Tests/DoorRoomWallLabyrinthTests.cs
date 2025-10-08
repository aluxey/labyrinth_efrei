using System;
using Labyrinth.Collectable;
using Labyrinth.Tile;
using Xunit;

namespace Labyrinth.Tests
{
    public class DoorTests
    {
        [Fact]
        public void Door_Default_IsClosed_And_Pass_Throws_NotImplemented()
        {
            var key = new Key(Guid.NewGuid());
            var door = new Door(key);

            Assert.False(door.IsTraversable);
            Assert.Throws<NotImplementedException>(() => door.Pass());
        }

        [Fact]
        public void Door_Close_WithRightKey_Then_Pass_Throws_NotImplemented_InCurrentImplementation()
        {
            var key = new Key(Guid.NewGuid());
            var door = new Door(key);

            door.CloseDoor(key);

            Assert.False(door.IsTraversable);
            Assert.Throws<NotImplementedException>(() => door.Pass());
        }

        [Fact]
        public void Door_Open_WithWrongKey_DoesNotChangeState_WhenClosed()
        {
            var key = new Key(Guid.NewGuid());
            var wrong = new Key(Guid.NewGuid());
            var door = new Door(key);

            door.CloseDoor(key);
            Assert.False(door.IsTraversable);

            door.OpenDoor(wrong);
            Assert.False(door.IsTraversable);
        }
    }

    public class RoomTests
    {
        [Fact]
        public void Room_IsAlwaysTraversable_PassDoesNotThrow()
        {
            var room = new Room(null);
            Assert.True(room.IsTraversable);
            var ex = Record.Exception(() => room.Pass());
            Assert.Null(ex);
        }

        [Fact]
        public void Room_Item_ReadWrite_Works()
        {
            var item = new Key(Guid.NewGuid());
            var room = new Room(null);
            Assert.Null(room.Item);
            room.Item = item;
            Assert.Equal(item, room.Item);
        }
    }

    public class WallTests
    {
        [Fact]
        public void Wall_IsNotTraversable_PassDoesNotThrow_InCurrentImplementation()
        {
            var wall = new Wall();
            Assert.False(wall.IsTraversable);

            var ex = Record.Exception(() => wall.Pass());
            Assert.Null(ex);
        }
    }
}
