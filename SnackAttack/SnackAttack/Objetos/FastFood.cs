namespace SnackAttack;

/// <summary>GRUPO 2 | Comida ultraprocessada: tira vida e enche a barra de estômago (RF14, RF19).</summary>
public class FastFood : Inimigo
{
    public FastFood(string nome, string cor, Forma forma, double x, double velocidade, int dano, int valorEstomago)
        : base(nome, cor, forma, x, velocidade, dano, valorEstomago) { }
}
