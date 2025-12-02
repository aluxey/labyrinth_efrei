using Labyrinth;
using Labyrinth.Build;

namespace LabyrinthTest.Build;

public class KeymasterTest
{
    [Test]
    public void PlacesKeysWhenDoorsComeFirst()
    {
        using var km = new Keymaster();
        var firstDoor = km.NewDoor();
        var secondDoor = km.NewDoor();

        var firstRoom = km.NewKeyRoom();
        var secondRoom = km.NewKeyRoom();

        var firstInventory = firstRoom.Pass();
        var secondInventory = secondRoom.Pass();

        using var all = Assert.EnterMultipleScope();
        Assert.That(firstInventory.HasItems, Is.True);
        Assert.That(secondInventory.HasItems, Is.True);
        Assert.That(firstDoor.Open(secondInventory), Is.False);
        Assert.That(firstDoor.IsLocked, Is.True);
        Assert.That(firstDoor.Open(firstInventory), Is.True);
        Assert.That(secondDoor.Open(secondInventory), Is.True);
    }

    [Test]
    public void PlacesKeysWhenRoomsComeFirst()
    {
        using var km = new Keymaster();

        var waitingRoom = km.NewKeyRoom();
        var anotherRoom = km.NewKeyRoom();

        var firstDoor = km.NewDoor();
        var secondDoor = km.NewDoor();

        var waitingInventory = waitingRoom.Pass();
        var anotherInventory = anotherRoom.Pass();

        using var all = Assert.EnterMultipleScope();
        Assert.That(waitingInventory.HasItems, Is.True);
        Assert.That(anotherInventory.HasItems, Is.True);
        Assert.That(firstDoor.Open(anotherInventory), Is.False);
        Assert.That(firstDoor.IsLocked, Is.True);
        Assert.That(firstDoor.Open(waitingInventory), Is.True);
        Assert.That(secondDoor.Open(anotherInventory), Is.True);
    }

    [Test]
    public void DisposeThrowsWhenDoorsLackKeyRooms()
    {
        var km = new Keymaster();
        km.NewDoor();

        Assert.That(() => km.Dispose(), Throws.InvalidOperationException);
    }

    [Test]
    public void DisposeThrowsWhenKeyRoomsHaveNoDoor()
    {
        var km = new Keymaster();
        km.NewKeyRoom();

        Assert.That(() => km.Dispose(), Throws.InvalidOperationException);
    }

    [Test]
    public void LabyrinthHandlesDoorsBeforeKeys()
    {
        Assert.That(() => new Labyrinth.Labyrinth("""
            |/ /|
            |x k|
            | k |
            """),
            Throws.Nothing);
    }

    [Test]
    public void LabyrinthHandlesKeysBeforeDoors()
    {
        Assert.That(() => new Labyrinth.Labyrinth("""
            |k k|
            |x /|
            |/  |
            """),
            Throws.Nothing);
    }

    [Test]
    public void LabyrinthThrowsWhenTooManyDoors()
    {
        Assert.That(() => new Labyrinth.Labyrinth("""
            |/ /|
            |x  |
            | k |
            """),
            Throws.InvalidOperationException);
    }

    [Test]
    public void LabyrinthThrowsWhenTooManyKeys()
    {
        Assert.That(() => new Labyrinth.Labyrinth("""
            |k k|
            |x /|
            |   |
            """),
            Throws.InvalidOperationException);
    }
}
