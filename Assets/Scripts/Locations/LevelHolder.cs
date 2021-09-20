using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHolder : MonoBehaviour
{
    [SerializeField]
    private LevelData _data;
    public void ChooseLocation()
    {
        GameSession.SetCurrentLevel(_data);
        print(GameSession.CurrentLevel.name);
        SceneManager.LoadScene(1);
    }

    private void OnMouseDown()
    {
        ChooseLocation();
    }
}
