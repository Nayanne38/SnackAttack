namespace SnackAttack;

/// <summary>GRUPO 3 | Pergunta de múltipla escolha do quiz (RF12). Resposta correta começa em 0.</summary>
public class Pergunta
{
    public string Enunciado { get; }
    public string[] Alternativas { get; }
    public int Correta { get; }

    public Pergunta(string enunciado, string[] alternativas, int correta)
    {
        Enunciado = enunciado;
        Alternativas = alternativas;
        Correta = correta;
    }
}
