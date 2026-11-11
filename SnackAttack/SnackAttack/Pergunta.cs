namespace SnackAttack;

/// <summary>
/// GRUPO 3 – Função 3
/// Pergunta de múltipla escolha usada no quiz ao final das fases.
/// </summary>
public class Pergunta
{
    public string Enunciado { get; }
    public string[] Alternativas { get; }
    public int RespostaCorreta { get; } // número da alternativa (começando em 1)

    public Pergunta(string enunciado, string[] alternativas, int respostaCorreta)
    {
        Enunciado = enunciado;
        Alternativas = alternativas;
        RespostaCorreta = respostaCorreta;
    }

    /// <summary>Mostra a pergunta e retorna true se o jogador acertou.</summary>
    public bool Perguntar()
    {
        Console.WriteLine($"\nQUIZ: {Enunciado}");
        for (int i = 0; i < Alternativas.Length; i++)
            Console.WriteLine($"{i + 1} - {Alternativas[i]}");
        Console.Write("Resposta: ");

        return int.TryParse(Console.ReadLine(), out int resp) && resp == RespostaCorreta;
    }
}
