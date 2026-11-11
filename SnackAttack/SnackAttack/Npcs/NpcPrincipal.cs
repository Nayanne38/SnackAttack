namespace SnackAttack;

/// <summary>GRUPO 3 – Função 1 | Herança de Npc: conta a história e entrega missões.</summary>
public class NpcPrincipal : Npc
{
    public int Fase { get; private set; }

    public NpcPrincipal(string nome, int fase = 1) : base(nome)
    {
        Fase = fase;
    }

    public void ContarHistoria()
    {
        // TODO (Grupo 3 – Função 1): escrever a história do jogo
        Console.WriteLine($"[{Nome}]: Os fast foods ganharam vida e dominaram o mundo!");
    }

    public void DarMissao()
    {
        // TODO (Grupo 3 – Função 1): descrever a missão atual conforme a fase
        Console.WriteLine($"[{Nome}]: Encontre a semente, o manual e a água do lago!");
    }
}
