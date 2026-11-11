namespace SnackAttack;

/// <summary>GRUPO 1 | Base dos objetos "bons" (alimentos, itens da missão, debuff).</summary>
public abstract class Item : ObjetoQueCai
{
    public int Pontos { get; protected set; }

    protected Item(string nome, string cor, Forma forma, double x, double velocidade, int pontos)
        : base(nome, cor, forma, x, velocidade)
    {
        Pontos = pontos;
    }
}
