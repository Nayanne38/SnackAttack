namespace SnackAttack;

/// <summary>
/// GRUPO 3 – Função 2
/// Menu inicial (RF1) e instruções (RF2).
/// </summary>
public static class Menu
{
    public static int Exibir()
    {
        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("          SNACK ATTACK           ");
        Console.WriteLine("=================================");
        Console.WriteLine("1 - Novo jogo");
        Console.WriteLine("2 - Instruções");
        Console.WriteLine("3 - Sair");
        Console.Write("Escolha: ");

        string? entrada = Console.ReadLine();
        if (entrada == null) return 3; // fim da entrada (Ctrl+Z / Ctrl+D): sai do jogo
        return int.TryParse(entrada, out int opcao) ? opcao : 0;
    }

    public static void MostrarInstrucoes()
    {
        // TODO (Grupo 3 – Função 2): escrever instruções completas
        // (objetivo do jogo, barra de estômago, como coletar itens, como funciona o quiz...)
        Console.WriteLine("\n--- INSTRUÇÕES ---");
        Console.WriteLine("Explore as fases, colete sementes, manuais e água,");
        Console.WriteLine("derrote os fast foods e responda o quiz para avançar.");
        Console.WriteLine("Se sua vida chegar a 0 ou o estômago encher, você perde!");
        Console.WriteLine("Pressione ENTER para voltar.");
        Console.ReadLine();
    }
}
