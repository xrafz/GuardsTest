using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Item Action/ResurrectHero"))]
public class ResurrectHero : ItemAction
{
    public override void Init(MonoBehaviour mono, int value)
    {
        var manager = BattleHandler.Instance;
        manager.OnHeroDefeat += Resurrect;
    }

    private void Resurrect(Hero hero)
    {
        hero.Health.Set(hero.Health.Maximum);
        hero.Play("Spawn");
        BattleHandler.Instance.OnHeroDefeat -= Resurrect;
        RemoveItem();
    }

    private void RemoveItem()
    {
        var items = GameSession.Items;
        foreach (ItemData item in items)
        {
            var actions = item.Actions;
            foreach (ItemAction action in actions)
            {
                if (action == this)
                {
                    GameSession.Items.Remove(item);
                    break;
                }
            }
        }
        Destroy(this);
    }
}
