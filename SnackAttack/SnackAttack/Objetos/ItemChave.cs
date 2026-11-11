namespace SnackAttack;

/// <summary>
/// GRUPO 1 | Item da missão (Semente, Manual ou Água). Pegar o item da fase
/// termina a fase e chama o quiz. Fica guardado no inventário do jogador.
/// </summary>
public class ItemChave : Item
{
    public ItemChave(string nome, string cor, Forma forma, double x)
        : base(nome, cor, forma, x, velocidade: 120, pontos: 50)
    {
        Tamanho = 52;
    }

    public override void AoColetar(Jogador jogador)
    {
        jogador.Inventario.Add(Nome);
        jogador.GanharPontos(Pontos);
    }
}
