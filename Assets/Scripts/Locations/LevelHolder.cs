using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHolder : MonoBehaviour
{
    [SerializeField]
    private LevelData _data;

    public LevelData Data => _data;

    [SerializeField]
    private Collider2D _collider;

    public void ChooseLevel()
    {
        GameSession.SetCurrentLevel(_data);
        MapHandler.Instance.LoadCharacterSelection();
    }

    private void OnMouseDown()
    {
        ChooseLevel();
    }

    public void SetColliderStatus(bool value)
    {
        _collider.enabled = value;
    }
}
