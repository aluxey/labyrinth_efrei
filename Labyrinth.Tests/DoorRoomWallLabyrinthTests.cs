using System;
using Labyrinth.Collectable;
using Labyrinth.Tile;
using Xunit;

namespace Labyrinth.Tests
{
    public class DoorTests
    {
        [Fact]
        public void Door_Default_IsClosed_And_Pass_Throws_InvalidOperation()
        {
            var key = new Key(Guid.NewGuid());
            var door = new Door(key);

            Assert.False(door.IsTraversable);
            Assert.Throws<InvalidOperationException>(() => door.Pass());
        }

        [Fact]
        public void Door_Close_WithRightKey_Then_Pass_Throws_InvalidOperation()
        {
            var key = new Key(Guid.NewGuid());
            var door = new Door(key);

            door.Close(key);
            Assert.False(door.IsTraversable);
            Assert.Throws<InvalidOperationException>(() => door.Pass());
        }

        [Fact]
        public void Door_Open_WithWrongKey_DoesNotChangeState_WhenClosed()
        {
            var key = new Key(Guid.NewGuid());
            var wrong = new Key(Guid.NewGuid());
            var door = new Door(key);

            door.Close(key);
            Assert.False(door.IsTraversable);

            door.Open(wrong);
            Assert.False(door.IsTraversable);
        }

        [Fact]
        public void Door_Open_WithRightKey_AllowsPass()
        {
            var key = new Key(Guid.NewGuid());
            var door = new Door(key);

            door.Open(key);
            Assert.True(door.IsTraversable);
            var ex = Record.Exception(() => door.Pass());
            Assert.Null(ex);
        }
    }

    public class RoomTests
    {
        [Fact]
        public void Room_IsTraversable_But_Pass_Throws_NotImplemented_In_Current_Impl()
        {
            var room = new Room(null);
            Assert.True(room.IsTraversable);
            Assert.Throws<NotImplementedException>(() => room.Pass());
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
        public void Wall_IsNotTraversable_And_Pass_Throws_NotImplemented_In_Current_Impl()
        {
            var wall = new Wall();
            Assert.False(wall.IsTraversable);
            Assert.Throws<NotImplementedException>(() => wall.Pass());
        }
    }
}
