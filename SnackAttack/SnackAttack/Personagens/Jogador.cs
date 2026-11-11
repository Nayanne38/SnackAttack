namespace SnackAttack;

/// <summary>
/// GRUPO 1 | O personagem que anda de um lado para o outro pegando as comidas.
/// </summary>
public class Jogador
{
    public string Nome { get; private set; }
    public double X { get; private set; }
    public double Y { get; private set; }
    public int Largura { get; private set; } = 90;
    public int Altura { get; private set; } = 70;
    public double Velocidade { get; private set; } = 420;   // pixels por segundo

    public int Vida { get; private set; } = 100;
    public int VidaMaxima { get; private set; } = 100;
    public int Estomago { get; private set; } = 0;
    public int EstomagoMaximo { get; private set; } = 100;
    public int Pontos { get; private set; }

    // pulo (RF4)
    private readonly double _chao;      // altura do chão (Y de quando está parado)
    private double _velY;               // velocidade vertical (negativo = subindo)
    private const double ForcaPulo = 650;
    private const double Gravidade = 1800;

    public List<string> Inventario { get; } = new();
    public List<string> Habilidades { get; } = new();

    public Jogador(string nome, int larguraTela, int alturaTela)
    {
        Nome = nome;
        X = (larguraTela - Largura) / 2.0;
        Y = alturaTela - Altura - 50;
        _chao = Y;
    }

    /// <summary>direcao: -1 esquerda, 0 parado, 1 direita.</summary>
    public void Mover(int direcao, double dt, int larguraTela)
    {
        X += direcao * Velocidade * dt;
        X = Math.Clamp(X, 0, larguraTela - Largura);

        // pulo: a gravidade puxa para baixo até voltar ao chão
        _velY += Gravidade * dt;
        Y += _velY * dt;
        if (Y >= _chao) { Y = _chao; _velY = 0; }
    }

    /// <summary>Só pula se estiver no chão (sem pulo duplo).</summary>
    public void Pular()
    {
        if (Y >= _chao) _velY = -ForcaPulo;
    }

    public void PerderVida(int dano) => Vida = Math.Max(0, Vida - dano);
    public void Curar(int valor) => Vida = Math.Min(VidaMaxima, Vida + valor);
    public void AumentarEstomago(int valor) => Estomago = Math.Min(EstomagoMaximo, Estomago + valor);
    public void ReduzirEstomago(int valor) => Estomago = Math.Max(0, Estomago - valor);
    public void GanharPontos(int pontos) => Pontos += pontos;

    /// <summary>Derrota quando a vida zera OU o estômago enche.</summary>
    public bool EstaVivo() => Vida > 0 && Estomago < EstomagoMaximo;

    public bool TemItem(string nome) => Inventario.Contains(nome);
    public bool TemHabilidade(string nome) => Habilidades.Contains(nome);

    public void AdicionarHabilidade(string nome)
    {
        // se o jogador já tem essa habilidade, não faz nada
        if (Habilidades.Contains(nome)) return;

        // guarda a habilidade na lista
        Habilidades.Add(nome);

        // Visão aprimorada: o personagem fica mais largo e pega mais comida
        if (nome == "Visão aprimorada")
        {
            Largura += 40;
            X -= 20;   // mantém o personagem centralizado
        }
    }
}
