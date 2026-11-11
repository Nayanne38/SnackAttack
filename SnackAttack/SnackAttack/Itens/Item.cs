namespace SnackAttack;

public enum TipoItem { Semente, Manual, Agua, Extra, Debuff, Arma, Alimento }

/// <summary>
/// GRUPO 1 – Função 2
/// Classe base de todos os itens (Espada e Alimento herdam dela).
/// </summary>
public class Item
{
    public string Nome { get; protected set; }
    public TipoItem Tipo { get; protected set; }
    public string Descricao { get; protected set; }
    public string Efeito { get; protected set; }

    public Item(string nome, TipoItem tipo, string descricao, string efeito = "")
    {
        Nome = nome;
        Tipo = tipo;
        Descricao = descricao;
        Efeito = efeito;
    }

    public virtual void AplicarEfeito(Jogador jogador)
    {
        // TODO (Grupo 1 – Função 2): itens de Debuff devem reduzir o estômago ou curar vida (RF11)
        Console.WriteLine($"{Nome} não tem efeito especial.");
    }

    public void SerColetado(Jogador jogador)
    {
        Console.WriteLine($"Você coletou: {Nome}!");
        jogador.GanharPontos(10);
    }
}
