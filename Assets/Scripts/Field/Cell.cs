using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private Creature _containedCreature;

    public Creature ContainedCreature => _containedCreature;

    [SerializeField]
    private SpriteRenderer _renderer;

    public void SetRendererStatus(bool enabledStatus)
    {
        _renderer.enabled = enabledStatus;
    }

    public void SetContainedCreature(Creature creature)
    {
        _containedCreature = creature;
    }

    public void DisableRenderer()
    {
        _renderer.enabled = false;
    }

    private void OnMouseDown()
    {
        //проверка, если был выбран герой, то смена местами и смена хода
    }
}
