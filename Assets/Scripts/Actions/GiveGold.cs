using UnityEngine;

[CreateAssetMenu(menuName = ("Item Action/AddGold"))]
public class GiveGold : ItemAction
{
    public override void Init(MonoBehaviour mono, int value)
    {
        GameSession.ChangeGold(value);
        Destroy(this);
    }
}
