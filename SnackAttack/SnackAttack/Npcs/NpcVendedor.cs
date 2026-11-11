namespace SnackAttack;

/// <summary>GRUPO 3 – Função 1 | Herança de Npc: vende itens ao jogador usando os pontos.</summary>
public class NpcVendedor : Npc
{
    // item -> preço (em pontos)
    public Dictionary<Item, int> ItensAVenda { get; } = new();

    public NpcVendedor(string nome) : base(nome) { }

    public void AbrirLoja()
    {
        // TODO (Grupo 3 – Função 1): mostrar a lista numerada e deixar o jogador escolher
        Console.WriteLine($"[{Nome}]: Bem-vindo à minha loja!");
        foreach (var (item, preco) in ItensAVenda)
            Console.WriteLine($" - {item.Nome}: {preco} pontos");
    }

    public bool VenderItem(Jogador jogador, Item item)
    {
        // TODO (Grupo 3 – Função 1): validar se o item existe e usar jogador.GastarPontos(preco)
        if (!ItensAVenda.TryGetValue(item, out int preco)) return false;
        if (!jogador.GastarPontos(preco)) return false;

        jogador.ColetarItem(item);
        return true;
    }
}
