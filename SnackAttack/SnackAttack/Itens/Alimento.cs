namespace SnackAttack;

/// <summary>GRUPO 1 – Função 3 | Herança de Item: alimento saudável cultivado pelo jogador.</summary>
public class Alimento : Item
{
    public string HabilidadeConcedida { get; private set; }
    public int BuffVida { get; private set; }

    public Alimento(string nome, string descricao, string habilidade, int buffVida)
        : base(nome, TipoItem.Alimento, descricao)
    {
        HabilidadeConcedida = habilidade;
        BuffVida = buffVida;
    }

    /// <summary>Consome Semente + Manual + Água do inventário. Retorna false se faltar algo.</summary>
    public bool Cultivar(Jogador jogador)
    {
        // TODO (Grupo 1 – Função 3): bônus se tiver o item Extra; mensagens mais detalhadas
        if (!jogador.TemItem(TipoItem.Semente) ||
            !jogador.TemItem(TipoItem.Manual) ||
            !jogador.TemItem(TipoItem.Agua))
            return false;

        jogador.RemoverItem(TipoItem.Semente);
        jogador.RemoverItem(TipoItem.Manual);
        jogador.RemoverItem(TipoItem.Agua);
        Console.WriteLine($"Você cultivou {Nome} com sucesso!");
        return true;
    }

    public void ConcederHabilidades(Jogador jogador)
    {
        // TODO (Grupo 1 – Função 3): a habilidade deve ter efeito real (ex.: revelar NPCs ocultos)
        jogador.Habilidades.Add(HabilidadeConcedida);
        Console.WriteLine($"Nova habilidade: {HabilidadeConcedida}!");
    }

    public void AplicarBuff(Jogador jogador)
    {
        jogador.Curar(BuffVida);
    }
}
