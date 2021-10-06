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

    private Vector2Int _cellIndexes;

    public Vector2Int CellIndexes => _cellIndexes;

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

    public void SetCellIndexes(Vector2Int indexes)
    {
        _cellIndexes = indexes;
    }

    private void OnMouseDown()
    {
        if (_containedCreature != null) //debug
        {
            if (!BattleHandler.Instance.ItemUsed)
            {
                BattleHandler.Instance.SelectedItem?.TargetedUse(_containedCreature);
            }
            print(string.Format("Clicked on {0}, hp: {1}, damage: {2}, atk range: {3}", _containedCreature.name, _containedCreature.Health.Current,
                _containedCreature.Data.Damage, _containedCreature.Data.AttackRange));
        }
        BattleHandler.Instance.SetItem(null);
        BattleHandler.Instance.SetSelectedCreature(_containedCreature);
    }
}
