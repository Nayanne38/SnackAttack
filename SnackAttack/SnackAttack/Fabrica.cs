namespace SnackAttack;

/// <summary>
/// Cria os objetos prontos. Para adicionar um alimento ou fast food novo,
/// é só incluir uma linha nas listas abaixo.
/// </summary>
public static class Fabrica
{
    // GRUPO 1: (nome, cor, forma, pontos, vida que cura)
    private static readonly (string Nome, string Cor, Forma Forma, int Pontos, int Vida)[] _alimentos =
    {
        ("Cenoura",  "#F28C28", Forma.Triangulo, 10, 3),
        ("Brócolis", "#3E9B4F", Forma.Circulo,   10, 3),
        ("Maçã",     "#D9363E", Forma.Circulo,   10, 3),
        // TODO (Grupo 1): adicionar mais 2 alimentos saudáveis (ex.: Banana, Uva)
    };

    // GRUPO 2: (nome, cor, forma, dano, estômago)
    private static readonly (string Nome, string Cor, Forma Forma, int Dano, int Estomago)[] _fastFoods =
    {
        ("Hambúrguer", "#B5651D", Forma.Retangulo, 8, 12),
        ("Batata Frita", "#E8B923", Forma.Retangulo, 6, 10),
        ("Refrigerante", "#6A1B9A", Forma.Retangulo, 5, 15),
        // TODO (Grupo 2): adicionar mais 2 fast foods (ex.: Pizza, Donut)
    };

    public static Alimento CriarAlimento(Random rnd, double x)
    {
        var a = _alimentos[rnd.Next(_alimentos.Length)];
        return new Alimento(a.Nome, a.Cor, a.Forma, x, 150 + rnd.Next(70), a.Pontos, a.Vida);
    }

    public static FastFood CriarFastFood(Random rnd, double x)
    {
        var f = _fastFoods[rnd.Next(_fastFoods.Length)];
        return new FastFood(f.Nome, f.Cor, f.Forma, x, 170 + rnd.Next(90), f.Dano, f.Estomago);
    }

    public static Debuff CriarSucoDetox(double x) =>
        new Debuff("Suco Detox", "#7ED957", Forma.Retangulo, x, 150, reducaoEstomago: 30);

    /// <summary>tipo: "Semente", "Manual" ou "Água".</summary>
    public static ItemChave CriarItemChave(string tipo, double x) => tipo switch
    {
        "Semente" => new ItemChave("Semente", "#8B5A2B", Forma.Circulo, x),
        "Manual"  => new ItemChave("Manual", "#4A6FA5", Forma.Retangulo, x),
        _         => new ItemChave("Água", "#35A7E8", Forma.Circulo, x),
    };
}
