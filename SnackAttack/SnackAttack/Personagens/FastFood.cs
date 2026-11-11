namespace SnackAttack;

/// <summary>GRUPO 2 – Função 1 | Herança de Inimigo: monstros de comida ultraprocessada.</summary>
public class FastFood : Inimigo
{
    public FastFood(string nome, int vida, int dano, int valorEstomago)
        : base(nome, vida, dano, valorEstomago, pontosAoDerrotar: 15) { }

    public override void Atacar(Jogador alvo)
    {
        // TODO (Grupo 2 – Função 1): chance de errar o ataque, ataque crítico...
        Console.WriteLine($"{Nome} ataca {alvo.Nome}!");
        alvo.PerderVida(Dano);
        alvo.AumentarEstomago(ValorEstomago);
    }

    // Tipos prontos (adicionem mais!)
    public static FastFood CriarHamburguerZumbi() => new("Hambúrguer Zumbi", vida: 30, dano: 8, valorEstomago: 10);
    public static FastFood CriarBatataMaligna()   => new("Batata Maligna",   vida: 25, dano: 6, valorEstomago: 15);
    public static FastFood CriarRefriToxico()     => new("Refri Tóxico",     vida: 20, dano: 10, valorEstomago: 20);
}
