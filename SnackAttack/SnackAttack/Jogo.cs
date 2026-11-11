namespace SnackAttack;

public enum Estado { Menu, Instrucoes, Jogando, Pausado, Quiz, Cidade, Vitoria, Derrota }

/// <summary>
/// GRUPO 3 | Controla o jogo: estados (menu, jogando, quiz...), fases, objetos que caem,
/// pontuação e condições de vitória e derrota. A Tela só desenha o que está aqui.
/// </summary>
public class Jogo
{
    public const int Largura = 800;
    public const int Altura = 600;

    public Estado Estado { get; private set; } = Estado.Menu;
    public Jogador Jogador { get; private set; } = null!;
    public List<Fase> Fases { get; }
    public int FaseIndex { get; private set; }
    public Fase FaseAtual => Fases[FaseIndex];
    public List<ObjetoQueCai> Objetos { get; } = new();
    public Boss? Boss { get; private set; }
    public string Mensagem { get; private set; } = "";
    public Pergunta PerguntaAtual { get; private set; } = null!;
    public int Direcao { get; set; }              // a Tela preenche: -1, 0 ou 1

    private readonly Random _rnd = new();
    private double _tempoSpawn;
    private double _tempoMensagem;
    private double _esperaChave;
    private bool _chaveEmJogo;

    public Jogo()
    {
        Fases = CriarFases();
    }

    private static List<Fase> CriarFases() => new()
    {
        new Fase("Fase 1 - Plantio", "Cuidado com os fast-foods e pegue a SEMENTE!", "Semente",
            new List<Pergunta>
            {
                new Pergunta("Qual vitamina é rica na cenoura?", new[] { "Vitamina A", "Vitamina K", "Vitamina D" }, 0),
                new Pergunta("Qual destes é uma fruta?", new[] { "Batata frita", "Banana", "Hambúrguer" }, 1),
                new Pergunta("Qual vitamina a laranja tem?", new[] { "Vitamina B12", "Vitamina K", "Vitamina C" }, 2),
                new Pergunta("A cenoura ajuda a proteger o quê?", new[] { "As unhas", "A visão", "Os cabelos" }, 1),
            },
            intervaloSpawn: 0.9, chanceFastFood: 0.30),
        new Fase("Fase 2 - Aprender", "Sobreviva e ache o MANUAL secreto para aprender a plantar!", "Manual",
            new List<Pergunta>
            {
                new Pergunta("Qual opção forma um lanche equilibrado?", new[] { "Refrigerante e salgadinho", "Só bala", "Fruta e iogurte" }, 2),
                new Pergunta("Qual bebida é melhor para matar a sede?", new[] { "Água", "Refrigerante", "Milk-shake" }, 0),
                new Pergunta("O que devemos fazer antes de comer?", new[] { "Pular o almoço", "Lavar as mãos", "Comer mais doces" }, 1),
                new Pergunta("Qual alimento costuma ter fibras?", new[] { "Biscoito recheado", "Bala", "Feijão" }, 2),
            },
            intervaloSpawn: 0.75, chanceFastFood: 0.45),
        new Fase("Fase 3 - Lago", "Encare o Rei do Combo e pegue ÁGUA!", "Água",
            new List<Pergunta>
            {
                new Pergunta("Qual destes costuma ter muito açúcar?", new[] { "Água", "Refrigerante", "Pepino" }, 1),
                new Pergunta("Comer fast food em excesso pode trazer o quê?", new[] { "Mais energia sempre", "Ossos mais fortes", "Problemas de saúde" }, 2),
                new Pergunta("Qual escolha tem menos gordura?", new[] { "Maçã fresca", "Batata frita", "Hambúrguer duplo" }, 0),
                new Pergunta("O que pode acontecer ao consumir muito açúcar?", new[] { "Melhora a visão", "Aumenta o risco de cáries", "Fortalece os dentes" }, 1),
            },
            intervaloSpawn: 0.65, chanceFastFood: 0.60, temBoss: true),
    };

    // ---------------- Navegação ----------------

    public void NovoJogo(string nome = "Herói")
    {
        var habilidades = Jogador?.Habilidades.ToList() ?? new List<string>();
        Jogador = new Jogador(nome, Largura, Altura);
        foreach (string habilidade in habilidades) Jogador.AdicionarHabilidade(habilidade);
        FaseIndex = 0;
        IniciarFase();
    }

    public void IrParaInstrucoes() => Estado = Estado.Instrucoes;
    public void VoltarAoMenu() => Estado = Estado.Menu;

    public void AlternarPausa()
    {
        if (Estado == Estado.Jogando) Estado = Estado.Pausado;
        else if (Estado == Estado.Pausado) Estado = Estado.Jogando;
    }

    private void IniciarFase()
    {
        Objetos.Clear();
        Boss = FaseAtual.TemBoss ? new Boss() : null;
        _tempoSpawn = 0.5;
        _esperaChave = 5;
        _chaveEmJogo = false;
        Mensagem = FaseAtual.Descricao;
        _tempoMensagem = 3;
        Estado = Estado.Jogando;
    }

    // ---------------- Loop (chamado ~60x por segundo pela Tela) ----------------

    public void Atualizar(double dt)
    {
        if (_tempoMensagem > 0) _tempoMensagem -= dt;
        if (Estado != Estado.Jogando) return;

        Jogador.Mover(Direcao, dt, Largura);
        Spawnar(dt);

        if (Boss != null)
        {
            FastFood? tiro = Boss.Atualizar(dt, _rnd, Largura);
            if (tiro != null) Objetos.Add(tiro);
        }

        for (int i = Objetos.Count - 1; i >= 0; i--)
        {
            ObjetoQueCai o = Objetos[i];
            o.Atualizar(dt);

            if (o.Colide(Jogador))
            {
                o.AoColetar(Jogador);
                Objetos.RemoveAt(i);
                Avisar(o is Inimigo ? $"Ai! {o.Nome}!" : $"Pegou {o.Nome}!");

                if (o is ItemChave)
                {
                    Objetos.Clear();
                    PerguntaAtual = FaseAtual.Perguntas[_rnd.Next(FaseAtual.Perguntas.Count)];
                    Estado = Estado.Quiz;
                    Mensagem = "";
                    return;
                }
            }
            else if (o.SaiuDaTela(Altura))
            {
                if (o is ItemChave) { _chaveEmJogo = false; _esperaChave = 3; } // perdeu: cai outro depois
                Objetos.RemoveAt(i);
            }
        }

        VerificarDerrota();
    }

    private void Spawnar(double dt)
    {
        _tempoSpawn -= dt;
        if (_tempoSpawn <= 0)
        {
            _tempoSpawn = FaseAtual.IntervaloSpawn;
            double x = _rnd.Next(10, Largura - 60);
            double sorte = _rnd.NextDouble();

            if (sorte < FaseAtual.ChanceFastFood) Objetos.Add(Fabrica.CriarFastFood(_rnd, x));
            else if (sorte > 0.94) Objetos.Add(Fabrica.CriarSucoDetox(x));
            else Objetos.Add(Fabrica.CriarAlimento(_rnd, x));
        }

        if (!_chaveEmJogo)
        {
            _esperaChave -= dt;
            if (_esperaChave <= 0)
            {
                Objetos.Add(Fabrica.CriarItemChave(FaseAtual.ItemChave, _rnd.Next(40, Largura - 100)));
                _chaveEmJogo = true;
            }
        }
    }

    private void VerificarDerrota()
    {
        if (Jogador.EstaVivo()) return;

        Mensagem = Jogador.Vida <= 0 ? "Sua vida acabou..." : "Seu estômago estourou de tanto fast food!";
        _tempoMensagem = 999;
        Estado = Estado.Derrota;
    }

    private void Avisar(string texto)
    {
        Mensagem = texto;
        _tempoMensagem = 1.2;
    }

    public string MensagemAtual => _tempoMensagem > 0 ? Mensagem : "";

    // ---------------- Quiz e cultivo ----------------

    /// <summary>Responde ao quiz da fase. Retorna true se acertou.</summary>
    public bool Responder(int alternativa)
    {
        if (Estado != Estado.Quiz) return false;

        if (alternativa == PerguntaAtual.Correta)
        {
            Jogador.GanharPontos(20);
            if (FaseIndex == Fases.Count - 1) Estado = Estado.Cidade;
            else
            {
                FaseIndex++;
                IniciarFase();
            }
            return true;
        }

        Jogador.PerderVida(10);
        Avisar("Resposta errada! -10 de vida");
        if (!Jogador.EstaVivo()) VerificarDerrota();
        return false;
    }

    /// <summary>De volta à cidade: usa Semente + Manual + Água para cultivar.</summary>
    public void Cultivar()
    {
        if (Estado != Estado.Cidade) return;

        if (Jogador.TemItem("Semente") && Jogador.TemItem("Manual") && Jogador.TemItem("Água"))
        {
            Jogador.AdicionarHabilidade("Visão aprimorada");
            Jogador.Curar(20);
            Estado = Estado.Vitoria;
        }
        else
        {
            Mensagem = "Faltaram itens para cultivar!";
            Estado = Estado.Derrota;
        }
    }
}
