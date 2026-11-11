namespace SnackAttack;

/// <summary>
/// GRUPO 2 – Função 1
/// Classe base abstrata de todos os inimigos (FastFood e Boss herdam dela).
/// </summary>
public abstract class Inimigo
{
    public string Nome { get; protected set; }
    public int Vida { get; protected set; }
    public int Dano { get; protected set; }
    public int ValorEstomago { get; protected set; }
    public int PontosAoDerrotar { get; protected set; }

    protected Inimigo(string nome, int vida, int dano, int valorEstomago, int pontosAoDerrotar)
    {
        Nome = nome;
        Vida = vida;
        Dano = dano;
        ValorEstomago = valorEstomago;
        PontosAoDerrotar = pontosAoDerrotar;
    }

    public void PerderVida(int dano)
    {
        Vida = Math.Max(0, Vida - dano);
        Console.WriteLine($"{Nome} perdeu {dano} de vida. (Vida: {Vida})");
    }

    public bool EstaVivo() => Vida > 0;

    public abstract void Atacar(Jogador alvo);

    public virtual Item? SoltarItem()
    {
        // TODO (Grupo 2 – Função 1): chance de soltar item de debuff (use ItensFactory e Random)
        return null;
    }
}
