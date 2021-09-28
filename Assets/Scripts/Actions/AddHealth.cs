using UnityEngine;

[CreateAssetMenu(menuName =("Item Action/AddHealth"))]
public class AddHealth : ItemAction
{
    private Creature _hero;
    private int _buffValue;
    private int _turnsAfterBuff = 0;
    [SerializeField]
    private int _turnsBeforeDebuff;

    public override void Init(MonoBehaviour mono, int value)
    {
        _hero = mono.GetComponent<Hero>();
        _buffValue = value;
        _hero.Health.Change(_buffValue, _buffValue);
        BattleState.Instance.OnTurn += UpdateTurns;
    }

    private void UpdateTurns()
    {
        _turnsAfterBuff++;
        if (_turnsAfterBuff >= _turnsBeforeDebuff)
        {
            _hero.Health.Change(-_buffValue, -_buffValue);
            BattleState.Instance.OnTurn -= UpdateTurns;
        }
    }
}
