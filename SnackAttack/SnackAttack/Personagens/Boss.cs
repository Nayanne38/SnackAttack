namespace SnackAttack;

/// <summary>
/// GRUPO 2 | O chefão: fica no topo da tela andando de um lado para o outro
/// e joga fast food em cima do jogador (aparece na fase final).
/// </summary>
public class Boss
{
    public string Nome { get; } = "Rei do Combo";
    public double X { get; private set; } = 340;
    public double Y { get; } = 56;
    public int Largura { get; } = 120;
    public int Altura { get; } = 70;

    private double _direcao = 1;
    private double _velocidade = 130;
    private double _tempoAteTiro = 1.5;
    private double _intervaloTiro = 1.2;

    // ataque especial: a cada 10 segundos, chuva de 5 fast foods
    private double _tempoAteEspecial = 10;
    private readonly Queue<FastFood> _fila = new();

    /// <summary>Fica true no último 1,5 s antes do ataque (a Tela mostra um "!").</summary>
    public bool PreparandoEspecial => _tempoAteEspecial <= 1.5;

    public FastFood? Atualizar(double dt, Random rnd, int larguraTela)
    {
        X += _direcao * _velocidade * dt;
        if (X < 0) { X = 0; _direcao = 1; }
        if (X > larguraTela - Largura) { X = larguraTela - Largura; _direcao = -1; }

        _tempoAteEspecial -= dt;
        if (_tempoAteEspecial <= 0)
        {
            _tempoAteEspecial = 10;
            PrepararChuva(rnd, larguraTela);
        }
        if (_fila.Count > 0) return _fila.Dequeue();

        _tempoAteTiro -= dt;
        if (_tempoAteTiro > 0) return null;

        _tempoAteTiro = _intervaloTiro;
        FastFood tiro = Fabrica.CriarFastFood(rnd, X + Largura / 2.0 - 22);
        tiro.Y = Y + Altura;
        return tiro;
    }

    /// <summary>Cria 5 fast foods espalhados pela tela; eles saem um por quadro.</summary>
    private void PrepararChuva(Random rnd, int larguraTela)
    {
        for (int i = 0; i < 5; i++)
        {
            double x = (larguraTela - 60) * (i + 0.5) / 5.0;
            FastFood f = Fabrica.CriarFastFood(rnd, x);
            f.Y = Y + Altura;
            _fila.Enqueue(f);
        }
    }
}
