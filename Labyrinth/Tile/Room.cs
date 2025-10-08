using Labyrinth.Collectable;

namespace Labyrinth.Tile;

/// <summary>
/// Représente une salle du labyrinthe. Toujours traversable.
/// </summary>
/// <param name="item">Éventuel objet collectable présent dans la salle.</param>
public class Room(ICollectable? item = null) : Tile
{
    public override bool IsTraversable { get; } = true;
    public ICollectable? Item { get; set; } = item;

    public override void Pass()
    {
        throw new NotImplementedException();
    }
}
