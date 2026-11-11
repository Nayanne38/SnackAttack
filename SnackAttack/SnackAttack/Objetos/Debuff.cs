namespace SnackAttack;

/// <summary>GRUPO 1 | Item que remove efeito negativo: reduz a barra de estômago (RF11).</summary>
public class Debuff : Item
{
    public int ReducaoEstomago { get; private set; }

    public Debuff(string nome, string cor, Forma forma, double x, double velocidade, int reducaoEstomago)
        : base(nome, cor, forma, x, velocidade, pontos: 5)
    {
        ReducaoEstomago = reducaoEstomago;
    }

    public override void AoColetar(Jogador jogador)
    {
        jogador.GanharPontos(Pontos);
        jogador.ReduzirEstomago(ReducaoEstomago);
    }
}
