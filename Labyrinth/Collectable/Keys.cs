namespace Labyrinth.Collectable;

/// <summary>
/// Objet collectable représentant une clé du labyrinthe.
/// </summary>
/// <param name="guid">Identifiant unique de la clé.</param>
public class Key(Guid guid) : ICollectable
{
    public Guid Guid { get; set; } = guid;
}
