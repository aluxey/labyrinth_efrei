using Labyrinth.Collectable;

namespace Labyrinth.Tile;

/// <summary>
/// Tuile représentant une porte. Traversable uniquement lorsqu'elle est ouverte.
/// </summary>
public class Door(Key requiredKey) : Tile
{
    private readonly Key _requiredKey = requiredKey;

    public bool IsOpened { get; private set; }

    public override bool IsTraversable => IsOpened;

    public void Open(Key key)
    {
        if (key.Id != _requiredKey.Id)
            throw new InvalidOperationException("Cette clé ne correspond pas à cette porte !");
        IsOpened = true;
    }

    public void Close() => IsOpened = false;

    public override void Pass()
    {
        if (!IsOpened)
            throw new InvalidOperationException("Impossible de traverser une porte fermée !");
    }
}
