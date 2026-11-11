namespace SnackAttack;

/// <summary>GRUPO 1 | Alimento saudável: dá pontos e cura um pouco de vida.</summary>
public class Alimento : Item
{
    public int BuffVida { get; private set; }

    public Alimento(string nome, string cor, Forma forma, double x, double velocidade, int pontos, int buffVida)
        : base(nome, cor, forma, x, velocidade, pontos)
    {
        BuffVida = buffVida;
    }

    public override void AoColetar(Jogador jogador)
    {
        jogador.GanharPontos(Pontos);
        jogador.Curar(BuffVida);
    }
}
