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

    public FastFood? Atualizar(double dt, Random rnd, int larguraTela)
    {
        X += _direcao * _velocidade * dt;
        if (X < 0) { X = 0; _direcao = 1; }
        if (X > larguraTela - Largura) { X = larguraTela - Largura; _direcao = -1; }

        _tempoAteTiro -= dt;
        if (_tempoAteTiro > 0) return null;

        _tempoAteTiro = _intervaloTiro;
        FastFood tiro = Fabrica.CriarFastFood(rnd, X + Largura / 2.0 - 22);
        tiro.Y = Y + Altura;
        return tiro;
    }

    // TODO (Grupo 2): ataque especial (ex.: chuva de 5 fast foods de uma vez a cada 10 segundos)
}
