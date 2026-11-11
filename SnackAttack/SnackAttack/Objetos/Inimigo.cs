namespace SnackAttack;

/// <summary>GRUPO 2 | Base dos objetos "ruins": ao encostar, machucam o jogador.</summary>
public abstract class Inimigo : ObjetoQueCai
{
    public int Dano { get; protected set; }
    public int ValorEstomago { get; protected set; }

    protected Inimigo(string nome, string cor, Forma forma, double x, double velocidade, int dano, int valorEstomago)
        : base(nome, cor, forma, x, velocidade)
    {
        Dano = dano;
        ValorEstomago = valorEstomago;
    }

    public override void AoColetar(Jogador jogador)
    {
        jogador.PerderVida(Dano);
        jogador.AumentarEstomago(ValorEstomago);
    }
}
