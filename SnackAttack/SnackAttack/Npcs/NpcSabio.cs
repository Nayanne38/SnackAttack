namespace SnackAttack;

/// <summary>GRUPO 3 – Função 1 | Herança de Npc: ensina sobre alimentação saudável e cultivo.</summary>
public class NpcSabio : Npc
{
    public int Fase { get; private set; }

    public NpcSabio(string nome, int fase) : base(nome)
    {
        Fase = fase;
        // TODO (Grupo 3 – Função 1): colocar falas com o conteúdo que cai no quiz
        Falas.Add("A cenoura é rica em vitamina A e faz bem para a visão!");
    }

    public void Ensinar() => Falar();

    public void DarDica()
    {
        // TODO (Grupo 3 – Função 1): dicas diferentes por fase
        Console.WriteLine($"[{Nome}]: Dica: explore tudo, itens podem estar escondidos!");
    }
}
