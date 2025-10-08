namespace Labyrinth.Tile;

/// <summary>
/// Tuile représentant un mur (non traversable).
/// </summary>
public class Wall: Tile
{
    public override bool IsTraversable { get; } = false;
    public override void Pass()
    {
        throw new NotImplementedException();
    }
}
