using Labyrinth.Collectable;
using Labyrinth.Tile;

namespace Labyrinth;

public class Labyrinth
{
    private int Width { get; }
    private int Height { get; }
    private Tile.Tile[][] Tiles { get; set; }

    public Labyrinth(string labyrinth)
    {
        if (string.IsNullOrEmpty(labyrinth))
            throw new IndexOutOfRangeException("La carte du labyrinthe ne peut pas être vide.");

        Key key = new Key(Guid.NewGuid());
        string[] rows = labyrinth.Split('\n');
        if (rows.Length == 0)
            throw new IndexOutOfRangeException("Carte invalide : aucune ligne.");

        Width = rows[0].Length;
        Height = rows.Length;
        Console.WriteLine($"Width={Width}, Height={Height}");
        InitLab();
        for (var i = 0; i < Height; i++)
        {
            Console.Write("\n");
            string row = rows[i] ?? string.Empty;
            for (var j = 0; j < row.Length; j++)
            {
                Console.Write(row[j]);
                switch (row[j])
                {
                    case ' ': Tiles[i][j] = new Room(null); break;
                    case 'k': Tiles[i][j] = new Room(key); break;
                    case '/': Tiles[i][j] = new Door(key); break;
                    default: Tiles[i][j] = new Wall(); break;
                }
            }
        }

        Console.WriteLine();
    }

    /// <summary>
    /// Initialise la grille interne des tuiles selon la largeur et la hauteur détectées.
    /// </summary>
    private void InitLab()
    {
        Tiles = new Tile.Tile[Height][];
        for (int i = 0; i < Height; i++)
        {
            Tiles[i] = new Tile.Tile[Width];
        }
    }
}
