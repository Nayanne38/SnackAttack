namespace SnackAttack;

/// <summary>
/// GRUPO 1 – Função 1
/// Personagem controlado pelo usuário.
/// Os métodos básicos já funcionam para o jogo rodar; melhorem os que têm TODO.
/// </summary>
public class Jogador
{
    public string Nome { get; private set; }
    public int Vida { get; private set; }
    public int VidaMaxima { get; private set; } = 100;
    public int Estomago { get; private set; }
    public int EstomagoMaximo { get; private set; } = 100;
    public int Pontos { get; private set; }
    public int Posicao { get; private set; } // TODO (Grupo 1): usar em Mover()

    public List<Item> Inventario { get; } = new();
    public List<string> Habilidades { get; } = new();
    public Espada? ArmaAtual { get; set; }

    public Jogador(string nome)
    {
        Nome = nome;
        Vida = VidaMaxima;
    }

    // ---------- Já implementados (contrato com os outros grupos: NÃO mudar assinatura) ----------

    public void PerderVida(int dano)
    {
        Vida = Math.Max(0, Vida - dano);
        Console.WriteLine($"{Nome} perdeu {dano} de vida. (Vida: {Vida}/{VidaMaxima})");
    }

    public void AumentarEstomago(int valor)
    {
        Estomago = Math.Min(EstomagoMaximo, Estomago + valor);
        Console.WriteLine($"Estômago de {Nome}: {Estomago}/{EstomagoMaximo}");
    }

    public void Curar(int valor)
    {
        Vida = Math.Min(VidaMaxima, Vida + valor);
    }

    public void GanharPontos(int pontos)
    {
        Pontos += pontos;
    }

    /// <summary>Retorna false se não tiver pontos suficientes.</summary>
    public bool GastarPontos(int pontos)
    {
        if (Pontos < pontos) return false;
        Pontos -= pontos;
        return true;
    }

    public bool EstaVivo() => Vida > 0 && Estomago < EstomagoMaximo;

    public void ColetarItem(Item item)
    {
        Inventario.Add(item);
        item.SerColetado(this);
    }

    // ---------- Para o Grupo 1 completar ----------

    public void Atacar(Inimigo alvo)
    {
        // TODO (Grupo 1 – Função 1): usar ArmaAtual?.CalcularDano(), alcance, habilidades...
        int dano = ArmaAtual?.CalcularDano() ?? 5;
        Console.WriteLine($"{Nome} ataca {alvo.Nome} causando {dano} de dano!");
        alvo.PerderVida(dano);
    }

    public bool TemItem(TipoItem tipo) => Inventario.Any(i => i.Tipo == tipo);

    public void RemoverItem(TipoItem tipo)
    {
        // TODO (Grupo 1 – Função 3): tratar vários itens do mesmo tipo
        var item = Inventario.FirstOrDefault(i => i.Tipo == tipo);
        if (item != null) Inventario.Remove(item);
    }

    public void Mover(int passos)
    {
        // TODO (Grupo 1 – Função 1): andar para frente/trás e pular (RF4), atualizando Posicao
        Posicao += passos;
    }

    public void MostrarStatus()
    {
        // TODO (Grupo 1 – Função 1): deixar bonito (barras de vida/estômago, itens, habilidades) - RF16
        Console.WriteLine($"\n--- STATUS DE {Nome} ---");
        Console.WriteLine($"Vida: {Vida}/{VidaMaxima} | Estômago: {Estomago}/{EstomagoMaximo} | Pontos: {Pontos}");
        Console.WriteLine($"Habilidades: {(Habilidades.Count == 0 ? "nenhuma" : string.Join(", ", Habilidades))}");
        Console.WriteLine($"Inventário: {(Inventario.Count == 0 ? "vazio" : string.Join(", ", Inventario.Select(i => i.Nome)))}");
    }
}
