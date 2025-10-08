namespace Labyrinth.Collectable;

/// <summary>
/// Objet collectable représentant une clé du labyrinthe.
/// </summary>
/// <param name="guid">Identifiant unique de la clé.</param>
public class Key : ICollectable
{
    public Guid Id { get; }

    public Key(Guid id) => Id = id;

    public override string ToString() => $"Key({Id})";
}
