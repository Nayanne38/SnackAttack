namespace SnackAttack;

/// <summary>
/// GRUPO 2 – Função 3
/// Combate por turnos entre Jogador e Inimigo + condições de vitória/derrota.
/// </summary>
public static class Combate
{
    /// <summary>
    /// Roda o combate. Retorna true se o jogador sobreviveu, false se perdeu.
    /// </summary>
    public static bool Iniciar(Jogador jogador, Inimigo inimigo)
    {
        Console.WriteLine($"\n>>> Um {inimigo.Nome} apareceu! <<<");

        while (jogador.EstaVivo() && inimigo.EstaVivo())
        {
            // TODO (Grupo 2 – Função 3): adicionar "2 - Usar item" (debuff) e "3 - Fugir" (50% de chance)
            Console.WriteLine("\n1 - Atacar");
            Console.Write("Escolha: ");
            Console.ReadLine();

            jogador.Atacar(inimigo);

            if (inimigo.EstaVivo())
                inimigo.Atacar(jogador);
        }

        if (!jogador.EstaVivo())
        {
            VerificarDerrota(jogador);
            return false;
        }

        Console.WriteLine($"{inimigo.Nome} foi derrotado!");
        jogador.GanharPontos(inimigo.PontosAoDerrotar);

        Item? drop = inimigo.SoltarItem();
        if (drop != null) jogador.ColetarItem(drop);

        return true;
    }

    public static void VerificarDerrota(Jogador jogador)
    {
        // TODO (Grupo 2 – Função 3): mensagens diferentes (morreu por vida x estômago cheio)
        if (jogador.Vida <= 0)
            Console.WriteLine("Sua vida chegou a 0...");
        else if (jogador.Estomago >= jogador.EstomagoMaximo)
            Console.WriteLine("Seu estômago estourou de tanto fast food...");
    }

    public static bool VerificarVitoria(Boss boss) => !boss.EstaVivo();
}
