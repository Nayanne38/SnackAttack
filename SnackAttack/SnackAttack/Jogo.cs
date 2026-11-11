namespace SnackAttack;

/// <summary>
/// GRUPO 3 – Função 2
/// Classe que controla o jogo inteiro: menu, fases, vitória e derrota.
/// </summary>
public class Jogo
{
    public Jogador? Jogador { get; private set; }
    public List<Fase> Fases { get; private set; } = new();
    public int FaseAtual { get; private set; }

    private bool _rodando;

    public void Iniciar()
    {
        _rodando = true;

        while (_rodando)
        {
            int opcao = Menu.Exibir();

            switch (opcao)
            {
                case 1: NovoJogo(); break;
                case 2: Menu.MostrarInstrucoes(); break;
                case 3:
                    _rodando = false;
                    Console.WriteLine("Até a próxima!");
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    private void NovoJogo()
    {
        Console.Write("\nNome do jogador: ");
        string? nome = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(nome)) nome = "Herói";

        Jogador = new Jogador(nome);
        Jogador.ArmaAtual = ItensFactory.CriarEspada(); // arma inicial
        Fases = CriarMissao1();

        // TODO (Grupo 3 – Função 1): usar o NpcPrincipal para contar a história e dar a missão
        var narrador = new NpcPrincipal("Guardião da Cidade");
        narrador.ContarHistoria();
        narrador.DarMissao();

        for (FaseAtual = 0; FaseAtual < Fases.Count; FaseAtual++)
        {
            bool passou = Fases[FaseAtual].Executar(Jogador);

            if (!passou || !Jogador.EstaVivo())
            {
                FimDeJogo(false);
                return;
            }
        }

        // De volta à cidade: cultivar o ingrediente (usa semente + manual + água)
        Console.WriteLine("\nVocê voltou à cidade principal para cultivar o alimento...");
        Alimento cenoura = ItensFactory.CriarCenoura();

        if (cenoura.Cultivar(Jogador))
        {
            cenoura.ConcederHabilidades(Jogador);
            cenoura.AplicarBuff(Jogador);
            FimDeJogo(true);
        }
        else
        {
            Console.WriteLine("Faltaram itens para cultivar!");
            FimDeJogo(false);
        }
    }

    /// <summary>
    /// TODO (Grupo 3 – Função 3): deixar as fases mais completas
    /// (mais itens, NPCs, inimigos, perguntas). Hoje é só um exemplo mínimo.
    /// </summary>
    private List<Fase> CriarMissao1()
    {
        // ----- Fase 1: Parkour (semente) -----
        var fase1 = new Fase("Fase 1 - Parkour", "Você salta entre plataformas até a sala final.");
        fase1.Npcs.Add(new NpcSabio("Velho Agricultor", fase: 1));
        fase1.Itens.Add(ItensFactory.CriarSementeCenoura());
        fase1.Inimigos.Add(FastFood.CriarHamburguerZumbi());
        fase1.Perguntas.Add(new Pergunta(
            "Qual vitamina é rica na cenoura?",
            new[] { "Vitamina A", "Vitamina K", "Vitamina D" },
            respostaCorreta: 1));

        // ----- Fase 2: Labirinto (manual) -----
        var fase2 = new Fase("Fase 2 - Labirinto", "Você atravessa um labirinto e encontra um pergaminho.");
        fase2.Itens.Add(ItensFactory.CriarManual());
        fase2.Inimigos.Add(FastFood.CriarBatataMaligna());

        // ----- Fase 3: Lago (água + Boss) -----
        var fase3 = new Fase("Fase 3 - Lago", "Você chega ao lago cuja água acelera o cultivo.");
        fase3.Itens.Add(ItensFactory.CriarAgua());
        fase3.Inimigos.Add(FastFood.CriarRefriToxico());
        // TODO (Grupo 2 + 3): o Boss aparece no final da fase 3, captura o jogador e o empurra de volta (Boss.Capturar)

        return new List<Fase> { fase1, fase2, fase3 };
    }

    public void Pausar()
    {
        // TODO (Grupo 3 – Função 2): chamar o Pausar() durante as fases (RF3)
        Console.WriteLine("\n[JOGO PAUSADO] Pressione ENTER para continuar...");
        Console.ReadLine();
    }

    private void FimDeJogo(bool venceu)
    {
        Console.WriteLine("\n=================================");
        Console.WriteLine(venceu ? "          VITÓRIA!" : "          DERROTA...");
        Console.WriteLine("=================================");
        Jogador?.MostrarStatus();
    }
}
