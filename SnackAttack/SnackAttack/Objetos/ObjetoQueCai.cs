namespace SnackAttack;

public enum Forma { Circulo, Retangulo, Triangulo }

/// <summary>
/// Base de tudo que cai do céu (alimentos, fast foods, itens da missão...).
/// Cada filho define o que acontece quando o jogador pega: AoColetar().
/// </summary>
public abstract class ObjetoQueCai
{
    public double X { get; set; }
    public double Y { get; set; }
    public int Tamanho { get; protected set; } = 44;
    public double Velocidade { get; protected set; }   // pixels por segundo
    public string Nome { get; protected set; }
    public string Cor { get; protected set; }          // ex.: "#F28C28"
    public Forma Forma { get; protected set; }

    protected ObjetoQueCai(string nome, string cor, Forma forma, double x, double velocidade)
    {
        Nome = nome;
        Cor = cor;
        Forma = forma;
        X = x;
        Y = -Tamanho;
        Velocidade = velocidade;
    }

    public void Atualizar(double dt) => Y += Velocidade * dt;

    public bool SaiuDaTela(int altura) => Y > altura;

    public bool Colide(Jogador j) =>
        X < j.X + j.Largura && X + Tamanho > j.X &&
        Y + Tamanho > j.Y && Y < j.Y + j.Altura;

    /// <summary>O que acontece quando o jogador pega este objeto.</summary>
    public abstract void AoColetar(Jogador jogador);
}
