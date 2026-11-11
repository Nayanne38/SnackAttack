namespace SnackAttack;

/// <summary>GRUPO 2 – Função 2 | Herança de Inimigo: chefão que persegue e captura o jogador.</summary>
public class Boss : Inimigo
{
    public int FaseAtual { get; private set; }
    public string AtaqueEspecialNome { get; private set; }

    public Boss(string nome, int vida, int dano, int faseAtual = 1)
        : base(nome, vida, dano, valorEstomago: 0, pontosAoDerrotar: 100)
    {
        FaseAtual = faseAtual;
        AtaqueEspecialNome = "Combo Calórico";
    }

    public override void Atacar(Jogador alvo)
    {
        // TODO (Grupo 2 – Função 2): 30% de chance de chamar UsarAtaqueEspecial()
        Console.WriteLine($"{Nome} ataca {alvo.Nome}!");
        alvo.PerderVida(Dano);
    }

    public void UsarAtaqueEspecial(Jogador alvo)
    {
        // TODO (Grupo 2 – Função 2): efeito especial (ex.: dano alto + aumenta estômago)
        Console.WriteLine($"{Nome} usa {AtaqueEspecialNome}!");
        alvo.PerderVida(Dano * 2);
    }

    public void Perseguir(Jogador alvo)
    {
        // TODO (Grupo 2 – Função 2): cena de perseguição (fases finais das missões 1 e 2)
        Console.WriteLine($"{Nome} está perseguindo {alvo.Nome}!");
    }

    public void Capturar(Jogador alvo)
    {
        // TODO (Grupo 2 – Função 2): evento da missão 1: captura e empurra o jogador de volta à cidade
        Console.WriteLine($"{Nome} capturou {alvo.Nome}!");
        alvo.PerderVida(10);
    }
}
