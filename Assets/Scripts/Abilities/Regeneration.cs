using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Regeneration")]
public class Regeneration : Ability
{
    [SerializeField]
    private int _regenerationValue = 5;

    private Health _creature;

    public override void Init(MonoBehaviour mono)
    {
        _creature = ((Creature)mono).Health;
    }

    private void Action()
    {
        _creature.Change(_regenerationValue);
    }

    public override void Sub()
    {
        Unsub();
        BattleHandler.Instance.OnTurn += Action;
    }

    public override void Unsub()
    {
        BattleHandler.Instance.OnTurn -= Action;
    }
}
