using UnityEngine.SceneManagement;
using UnityEngine;

[CreateAssetMenu(menuName = ("Item Action/RestartLocation"))]
public class RestartLocation : ItemAction
{
    public override void Init(MonoBehaviour mono, int value)
    {
        BattleHandler.Instance.OnLose -= BattleHandler.Instance.Restart;
        BattleHandler.Instance.OnLose += Restart;
    }

    public void Restart()
    {
        BattleHandler.Instance.OnLose += BattleHandler.Instance.Restart;
        BattleHandler.Instance.OnLose -= Restart;
        RemoveItem();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
