using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    [SerializeField]
    private TextOutput _hp, _damage, _attackRange;

    public void Select(Creature creature)
    {
        if (creature != null)
        {
            gameObject.SetActive(true);
            _hp.Output(creature.Health.Current.ToString());
            _damage.Output(creature.Data.Damage.ToString());
            _attackRange.Output(creature.Data.AttackRange.ToString());
        }
        else
        {
            Deselect();
        }
    }

    public void Deselect()
    {
        gameObject.SetActive(false);
    }
}
