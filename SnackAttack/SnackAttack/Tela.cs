using System.Drawing.Drawing2D;

namespace SnackAttack;

/// <summary>
/// GRUPO 3 | Janela do jogo (Windows Forms): só DESENHA o que está em Jogo e lê o teclado.
/// Toda a regra do jogo fica em Jogo.cs, Jogador.cs etc.
/// </summary>
public class Tela : Form
{
    private readonly Jogo _jogo = new();
    private readonly System.Windows.Forms.Timer _timer = new() { Interval = 16 };
    private DateTime _ultimo = DateTime.Now;
    private bool _esquerda, _direita;

    private readonly Font _fTitulo = new("Segoe UI", 36, FontStyle.Bold);
    private readonly Font _fTexto = new("Segoe UI", 16, FontStyle.Bold);
    private readonly Font _fPequena = new("Segoe UI", 9, FontStyle.Bold);

    public Tela()
    {
        Text = "Snack Attack";
        ClientSize = new Size(Jogo.Largura, Jogo.Altura);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        DoubleBuffered = true;
        KeyPreview = true;

        _timer.Tick += (s, e) =>
        {
            DateTime agora = DateTime.Now;
            double dt = Math.Min(0.05, (agora - _ultimo).TotalSeconds);
            _ultimo = agora;

            _jogo.Direcao = (_direita ? 1 : 0) - (_esquerda ? 1 : 0);
            _jogo.Atualizar(dt);
            Invalidate();
        };
        _timer.Start();
    }

    // ---------------- Teclado ----------------

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (e.KeyCode is Keys.Left or Keys.A) _esquerda = true;
        if (e.KeyCode is Keys.Right or Keys.D) _direita = true;

        switch (_jogo.Estado)
        {
            case Estado.Menu:
                if (e.KeyCode is Keys.Enter or Keys.D1) _jogo.NovoJogo();
                else if (e.KeyCode == Keys.D2) _jogo.IrParaInstrucoes();
                else if (e.KeyCode == Keys.Escape) Close();
                break;
            case Estado.Instrucoes:
                _jogo.VoltarAoMenu();
                break;
            case Estado.Jogando:
            case Estado.Pausado:
                if (e.KeyCode == Keys.P) _jogo.AlternarPausa();
                break;
            case Estado.Quiz:
                if (e.KeyCode is >= Keys.D1 and <= Keys.D3) _jogo.Responder(e.KeyCode - Keys.D1);
                break;
            case Estado.Cidade:
                if (e.KeyCode == Keys.Enter) _jogo.Cultivar();
                break;
            case Estado.Vitoria:
            case Estado.Derrota:
                if (e.KeyCode == Keys.Enter) _jogo.VoltarAoMenu();
                break;
        }
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
        base.OnKeyUp(e);
        if (e.KeyCode is Keys.Left or Keys.A) _esquerda = false;
        if (e.KeyCode is Keys.Right or Keys.D) _direita = false;
    }

    // ---------------- Desenho ----------------

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

        DesenharFundo(g);

        switch (_jogo.Estado)
        {
            case Estado.Menu: DesenharMenu(g); break;
            case Estado.Instrucoes: DesenharInstrucoes(g); break;
            case Estado.Quiz: DesenharJogo(g); DesenharQuiz(g); break;
            case Estado.Cidade: DesenharJogo(g); DesenharCidade(g); break;
            case Estado.Vitoria: DesenharFim(g, true); break;
            case Estado.Derrota: DesenharFim(g, false); break;
            default: DesenharJogo(g); break;
        }
    }

    private void DesenharFundo(Graphics g)
    {
        g.Clear(ColorTranslator.FromHtml("#C9F2F7"));

        // chão quadriculado (estilo toalha de mesa)
        int tam = 25, y = Jogo.Altura - 50;
        using var vermelho = new SolidBrush(ColorTranslator.FromHtml("#C0392B"));
        for (int linha = 0; linha < 2; linha++)
            for (int col = 0; col < Jogo.Largura / tam; col++)
            {
                Brush b = (col + linha) % 2 == 0 ? vermelho : Brushes.White;
                g.FillRectangle(b, col * tam, y + linha * tam, tam, tam);
            }
    }

    private void DesenharJogo(Graphics g)
    {
        foreach (ObjetoQueCai o in _jogo.Objetos) DesenharObjeto(g, o);
        if (_jogo.Boss != null) DesenharBoss(g, _jogo.Boss);
        DesenharJogador(g, _jogo.Jogador);
        DesenharHud(g);
    }

    private void DesenharObjeto(Graphics g, ObjetoQueCai o)
    {
        using var brush = new SolidBrush(ColorTranslator.FromHtml(o.Cor));
        var r = new Rectangle((int)o.X, (int)o.Y, o.Tamanho, o.Tamanho);

        switch (o.Forma)
        {
            case Forma.Circulo: g.FillEllipse(brush, r); break;
            case Forma.Retangulo: g.FillRectangle(brush, r.X + 6, r.Y, r.Width - 12, r.Height); break;
            case Forma.Triangulo:
                g.FillPolygon(brush, new[]
                {
                    new Point(r.X + r.Width / 2, r.Y + r.Height),
                    new Point(r.X, r.Y),
                    new Point(r.X + r.Width, r.Y)
                });
                break;
        }

        // item da missão ganha contorno dourado
        if (o is ItemChave)
        {
            using var pen = new Pen(Color.Gold, 4);
            g.DrawEllipse(pen, r.X - 6, r.Y - 6, r.Width + 12, r.Height + 12);
        }

        g.DrawString(o.Nome, _fPequena, Brushes.Black, (float)o.X - 6, (float)o.Y + o.Tamanho + 2);
    }

    private void DesenharJogador(Graphics g, Jogador j)
    {
        // corpo marrom (estilo "bolinha")
        using var corpo = new SolidBrush(ColorTranslator.FromHtml("#C58B55"));
        g.FillEllipse(corpo, (float)j.X, (float)j.Y, j.Largura, j.Altura + 20);

        // olhos
        float ox = (float)j.X + 18, oy = (float)j.Y + 10;
        g.FillEllipse(Brushes.White, ox, oy, 24, 26);
        g.FillEllipse(Brushes.White, ox + 30, oy, 24, 26);
        g.FillEllipse(Brushes.Black, ox + 8, oy + 5, 9, 11);
        g.FillEllipse(Brushes.Black, ox + 38, oy + 5, 9, 11);

        // boca aberta (esperando a comida)
        using var boca = new SolidBrush(ColorTranslator.FromHtml("#7A2E1D"));
        g.FillEllipse(boca, (float)j.X + 28, (float)j.Y + 42, 34, 24);
    }

    private void DesenharBoss(Graphics g, Boss b)
    {
        using var corpo = new SolidBrush(ColorTranslator.FromHtml("#8E2C2C"));
        g.FillEllipse(corpo, (float)b.X, (float)b.Y, b.Largura, b.Altura);
        g.FillEllipse(Brushes.Yellow, (float)b.X + 30, (float)b.Y + 18, 18, 18);
        g.FillEllipse(Brushes.Yellow, (float)b.X + 72, (float)b.Y + 18, 18, 18);
        g.DrawString(b.Nome, _fPequena, Brushes.White, (float)b.X + 22, (float)b.Y + 44);
    }

    private void DesenharHud(Graphics g)
    {
        Jogador j = _jogo.Jogador;
        Barra(g, 12, 12, "Vida", j.Vida, j.VidaMaxima, "#2F7D4F");
        Barra(g, 12, 38, "Estômago", j.Estomago, j.EstomagoMaximo, "#D6452A");
        g.DrawString($"Pontos: {j.Pontos}", _fTexto, Brushes.Black, 12, 64);
        g.DrawString(_jogo.FaseAtual.Nome, _fTexto, Brushes.Black, Jogo.Largura - 250, 12);

        string inv = j.Inventario.Count == 0 ? "-" : string.Join(", ", j.Inventario);
        g.DrawString($"Itens: {inv}", _fPequena, Brushes.Black, Jogo.Largura - 250, 40);

        string msg = _jogo.MensagemAtual;
        if (msg != "") Centro(g, msg, 100, _fTexto, Brushes.DarkSlateBlue);
        if (_jogo.Estado == Estado.Pausado) Centro(g, "PAUSADO (P para continuar)", 260, _fTitulo, Brushes.Black);
    }

    private void Barra(Graphics g, int x, int y, string nome, int valor, int max, string cor)
    {
        g.DrawString(nome, _fPequena, Brushes.Black, x, y + 2);
        g.FillRectangle(Brushes.White, x + 70, y, 160, 18);
        using var b = new SolidBrush(ColorTranslator.FromHtml(cor));
        g.FillRectangle(b, x + 70, y, 160f * valor / max, 18);
        g.DrawRectangle(Pens.Black, x + 70, y, 160, 18);
    }

    private void DesenharMenu(Graphics g)
    {
        Centro(g, "SNACK ATTACK", 120, _fTitulo, Brushes.DarkGreen);
        Centro(g, "Pegue as comidas saudáveis. Fuja dos fast foods!", 190, _fTexto, Brushes.Black);
        Centro(g, "1 / Enter  -  Novo jogo", 300, _fTexto, Brushes.Black);
        Centro(g, "2  -  Instruções", 340, _fTexto, Brushes.Black);
        Centro(g, "Esc  -  Sair", 380, _fTexto, Brushes.Black);
    }

    private void DesenharInstrucoes(Graphics g)
    {
        // TODO (Grupo 3): deixar as instruções mais completas e bonitas
        Centro(g, "INSTRUÇÕES", 80, _fTitulo, Brushes.DarkGreen);
        Centro(g, "Setas ou A / D: mover o personagem", 190, _fTexto, Brushes.Black);
        Centro(g, "Pegue alimentos saudáveis: dão pontos e vida", 230, _fTexto, Brushes.Black);
        Centro(g, "Fast foods tiram vida e enchem o estômago", 270, _fTexto, Brushes.Black);
        Centro(g, "Pegue o item dourado da fase e responda o quiz", 310, _fTexto, Brushes.Black);
        Centro(g, "P: pausar", 350, _fTexto, Brushes.Black);
        Centro(g, "Aperte qualquer tecla para voltar", 460, _fTexto, Brushes.Gray);
    }

    private void DesenharQuiz(Graphics g)
    {
        using var fundo = new SolidBrush(Color.FromArgb(225, 255, 255, 255));
        g.FillRectangle(fundo, 60, 120, Jogo.Largura - 120, 300);

        Pergunta p = _jogo.FaseAtual.Pergunta;
        Centro(g, "QUIZ", 135, _fTitulo, Brushes.DarkGreen);
        Centro(g, p.Enunciado, 215, _fTexto, Brushes.Black);
        for (int i = 0; i < p.Alternativas.Length; i++)
            Centro(g, $"{i + 1} - {p.Alternativas[i]}", 265 + i * 38, _fTexto, Brushes.Black);

        string msg = _jogo.MensagemAtual;
        if (msg != "") Centro(g, msg, 385, _fTexto, Brushes.Firebrick);
    }

    private void DesenharCidade(Graphics g)
    {
        using var fundo = new SolidBrush(Color.FromArgb(225, 255, 255, 255));
        g.FillRectangle(fundo, 60, 150, Jogo.Largura - 120, 240);
        Centro(g, "De volta à cidade!", 170, _fTitulo, Brushes.DarkGreen);
        Centro(g, "Use a Semente, o Manual e a Água para cultivar.", 260, _fTexto, Brushes.Black);
        Centro(g, "Aperte Enter para cultivar a cenoura", 320, _fTexto, Brushes.Black);
    }

    private void DesenharFim(Graphics g, bool venceu)
    {
        Jogador j = _jogo.Jogador;
        Centro(g, venceu ? "VITÓRIA!" : "DERROTA...", 110, _fTitulo, venceu ? Brushes.DarkGreen : Brushes.Firebrick);
        if (venceu) Centro(g, "Você cultivou a cenoura e ganhou: Visão aprimorada", 200, _fTexto, Brushes.Black);
        else Centro(g, _jogo.Mensagem, 200, _fTexto, Brushes.Black);
        Centro(g, $"Pontos: {j.Pontos}", 260, _fTexto, Brushes.Black);
        Centro(g, "Enter - voltar ao menu", 380, _fTexto, Brushes.Gray);
    }

    private void Centro(Graphics g, string texto, float y, Font fonte, Brush cor)
    {
        var formato = new StringFormat { Alignment = StringAlignment.Center };
        g.DrawString(texto, fonte, cor, new RectangleF(0, y, Jogo.Largura, 80), formato);
    }
}
