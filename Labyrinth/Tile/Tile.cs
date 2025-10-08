namespace Labyrinth.Tile;

/// <summary>
/// Tuile abstraite représentant une case du labyrinthe.
/// </summary>
public abstract class Tile
{
    public abstract bool IsTraversable { get; }
    public abstract void Pass();
}
