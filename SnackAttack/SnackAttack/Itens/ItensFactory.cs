namespace SnackAttack;

/// <summary>
/// GRUPO 1 – Função 3
/// "Fábrica" que devolve itens prontos. Os outros grupos usam isso para não
/// precisar montar cada item. Adicionem novos itens aqui quando precisar.
/// </summary>
public static class ItensFactory
{
    public static Item CriarSementeCenoura() =>
        new Item("Semente de Cenoura", TipoItem.Semente, "Semente para cultivar cenoura.");

    public static Item CriarManual() =>
        new Item("Manual de Cultivo", TipoItem.Manual, "Explica como cultivar de forma sustentável.");

    public static Item CriarAgua() =>
        new Item("Água do Lago", TipoItem.Agua, "Acelera o crescimento da planta.");

    public static Item CriarItemExtra() =>
        new Item("Adubo Natural", TipoItem.Extra, "Item extra de cada alimento.");

    public static Item CriarItemDebuff() =>
        new Item("Suco Detox", TipoItem.Debuff, "Remove efeitos negativos.", "Reduz o estômago");

    public static Espada CriarEspada() =>
        new Espada("Espada de Madeira", dano: 15, alcance: 1);

    public static Alimento CriarCenoura() =>
        new Alimento("Cenoura", "Rica em vitamina A.", "Visão aprimorada", buffVida: 20);
}
