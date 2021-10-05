using UnityEngine;

[CreateAssetMenu(menuName =("Item Action/AddHealth"))]
public class AddHealth : ItemAction
{
    [SerializeField]
    private int _duration;

    private Creature _hero;
    private int _buffValue;
    private int _turnsAfterBuff = 0;

    public override void Init(MonoBehaviour mono, int value)
    {
        _hero = mono.GetComponent<Hero>();
        _buffValue = value;
        _hero.Health.Change(_buffValue, _buffValue);
        BattleHandler.Instance.OnTurn += UpdateTurns;
        Debug.Log($"Buffed {_hero.name} for {_buffValue}");
    }

    private void UpdateTurns()
    {
        _turnsAfterBuff++;
        if (_turnsAfterBuff >= _duration)
        {
            _hero.Health.Change(-_buffValue, -_buffValue);
            BattleHandler.Instance.OnTurn -= UpdateTurns;
        }
    }
}
