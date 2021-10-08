using UnityEngine;
using DG.Tweening;

public class UnitSelector : MonoBehaviour
{
    [SerializeField]
    private TextOutput _hp, _damage, _attackRange;

    [SerializeField]
    private GameObject _highlight;

    [SerializeField]
    private Transform _highlightTransform;

    private void Start()
    {
        Deselect();
    }

    public void Select(Creature creature)
    {
        if (creature != null)
        {
            gameObject.SetActive(true);
            _highlight.SetActive(true);
            _highlightTransform.position = creature.Transform.position;

            _hp.Output(creature.Health.Current.ToString());
            _damage.Output($"{creature.Data.Damage + creature.Data.AdditionalDamage}");
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
        _highlight.SetActive(false);
    }
}
