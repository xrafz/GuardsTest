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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
