namespace SnackAttack;

/// <summary>GRUPO 3 | Uma fase: qual item da missão pegar, dificuldade, quiz e se tem Boss.</summary>
public class Fase
{
    public string Nome { get; }
    public string Descricao { get; }
    public string ItemChave { get; }          // "Semente", "Manual" ou "Água"
    public Pergunta Pergunta { get; }
    public double IntervaloSpawn { get; }     // segundos entre cada objeto (menor = mais difícil)
    public double ChanceFastFood { get; }     // 0 a 1
    public bool TemBoss { get; }

    public Fase(string nome, string descricao, string itemChave, Pergunta pergunta,
                double intervaloSpawn, double chanceFastFood, bool temBoss = false)
    {
        Nome = nome;
        Descricao = descricao;
        ItemChave = itemChave;
        Pergunta = pergunta;
        IntervaloSpawn = intervaloSpawn;
        ChanceFastFood = chanceFastFood;
        TemBoss = temBoss;
    }
}
