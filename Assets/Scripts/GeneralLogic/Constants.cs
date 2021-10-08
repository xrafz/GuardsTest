public class Constants
{
    public const int InitialBudget = 260; // изначальный бюджет
    public const int InitialMithril = 10;
    public const int InitialStars = 0;
    public static readonly string[] InitialItems = 
    {
        "SmallHealthPotion",
        "BigHealthPotion",
        "DamagePotionTest",
        "Runes_Reverse",
        "Runes_Resurrect",
        "Runes_Restart",
        "Treasures_Add200Gold",
        "Treasures_RefreshUpgrades"
    };

    public enum DamageTypes
    {
        Slashing,
        Piercing,
        Blunt,
        DarkMagic,
        ElementalMagic,
        AstralMagic
    }
}
