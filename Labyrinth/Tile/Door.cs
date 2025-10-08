using Labyrinth.Collectable;

namespace Labyrinth.Tile;

/// <summary>
/// Tuile représentant une porte. Traversable uniquement lorsqu'elle est ouverte.
/// </summary>
public class Door : Tile
{
    public override bool IsTraversable => IsOpened;
    public Key Key { get; }
    public bool IsOpened { get; private set; } = false;

    public override void Pass()
    {
        if (!IsTraversable)
            throw new InvalidOperationException("La porte est fermée : vous ne pouvez pas passer.");
    }

    public Door(Key key)
    {
        Key = key;
    }

    public Door(Guid keyId, bool isOpened = false)
    {
        Key = new Key(keyId);
        IsOpened = isOpened;
    }

    public void Open(Key key)
    {
        if (key.Guid == Key.Guid) IsOpened = true;
    }

    public void Close(Key key)
    {
        if (key.Guid == Key.Guid) IsOpened = false;
    }
}
