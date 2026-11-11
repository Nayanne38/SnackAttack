namespace SnackAttack;

/// <summary>GRUPO 1 – Função 2 | Herança de Item: arma do jogador.</summary>
public class Espada : Item
{
    public int Dano { get; private set; }
    public int Alcance { get; private set; }

    public Espada(string nome, int dano, int alcance)
        : base(nome, TipoItem.Arma, "Arma usada para enfrentar os inimigos.", "Aumenta o dano")
    {
        Dano = dano;
        Alcance = alcance;
    }

    public int CalcularDano()
    {
        // TODO (Grupo 1 – Função 2): adicionar bônus aleatório (Random) ou críticos
        return Dano;
    }

    public override void AplicarEfeito(Jogador jogador)
    {
        jogador.ArmaAtual = this;
        Console.WriteLine($"{jogador.Nome} equipou {Nome}!");
    }
}
