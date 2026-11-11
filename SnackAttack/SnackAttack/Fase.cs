namespace SnackAttack;

/// <summary>
/// GRUPO 3 – Função 3
/// Representa uma fase do jogo: itens, inimigos, NPCs e quiz.
/// </summary>
public class Fase
{
    public string Nome { get; }
    public string Descricao { get; }
    public List<Inimigo> Inimigos { get; } = new();
    public List<Item> Itens { get; } = new();
    public List<Npc> Npcs { get; } = new();
    public List<Pergunta> Perguntas { get; } = new();

    private bool _concluida;

    public Fase(string nome, string descricao)
    {
        Nome = nome;
        Descricao = descricao;
    }

    public bool Concluida() => _concluida;

    /// <summary>
    /// Roda a fase inteira. Retorna true se o jogador passou, false se perdeu.
    /// </summary>
    public bool Executar(Jogador jogador)
    {
        Console.WriteLine($"\n=== {Nome} ===");
        Console.WriteLine(Descricao);

        // 1) NPCs falam
        foreach (var npc in Npcs) npc.Falar();

        // 2) Jogador coleta os itens
        // TODO (Grupo 3 – Função 3): dar escolhas ao jogador (ex.: esquerda/direita no labirinto)
        foreach (var item in Itens) jogador.ColetarItem(item);

        // 3) Combates
        foreach (var inimigo in Inimigos)
        {
            bool sobreviveu = Combate.Iniciar(jogador, inimigo);
            if (!sobreviveu) return false;
        }

        // 4) Quiz final (RF12): só avança se acertar
        foreach (var pergunta in Perguntas)
        {
            while (!pergunta.Perguntar())
            {
                Console.WriteLine("Resposta errada! Você perde 10 de vida. Tente de novo.");
                jogador.PerderVida(10);
                if (!jogador.EstaVivo()) return false;
            }
            jogador.GanharPontos(20);
        }

        _concluida = true;
        Console.WriteLine($"Fase concluída! Pontos: {jogador.Pontos}");
        return true;
    }
}
