using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/DamageReturn")]
public class DamageReturn : Ability
{
    [SerializeField]
    private int _returnedDamage = 3;

    private Creature _creature;
    public override void Init(MonoBehaviour mono)
    {
        _creature = (Creature)mono;
    }

    private void Action(Creature enemy)
    {
        enemy.Health.Change(-_returnedDamage);
    }

    public override void Sub()
    {
        Unsub();
        _creature.OnHit += Action;
    }

    public override void Unsub()
    {
        _creature.OnHit -= Action;
    }
}
