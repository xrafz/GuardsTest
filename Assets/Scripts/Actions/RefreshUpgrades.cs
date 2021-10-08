using UnityEngine;

[CreateAssetMenu(menuName = ("Item Action/RefreshUpgrades"))]
public class RefreshUpgrades : ItemAction
{
    public override void Init(MonoBehaviour mono, int value)
    {
        var slots = UpgradeScreen.Instance.HeroSlots;
        foreach (HeroUpgradeHolder upgrade in slots)
        {
            var randomHero = GameSession.Heroes[Random.Range(0, GameSession.Heroes.Length)];
            upgrade.Init(randomHero);
        }
        Destroy(this);
    }
}
