namespace SnackAttack;

/// <summary>
/// GRUPO 3 – Função 1
/// Classe base de todos os NPCs.
/// </summary>
public class Npc
{
    public string Nome { get; protected set; }
    public List<string> Falas { get; protected set; } = new();
    public bool Oculto { get; protected set; }

    public Npc(string nome, bool oculto = false)
    {
        Nome = nome;
        Oculto = oculto;
    }

    public virtual void Falar()
    {
        if (Oculto) return;
        foreach (var fala in Falas)
            Console.WriteLine($"[{Nome}]: {fala}");
    }

    public void Revelar()
    {
        Oculto = false;
        Console.WriteLine($"{Nome} foi revelado!");
    }
}
