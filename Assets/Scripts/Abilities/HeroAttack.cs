using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "Abilities/HeroAttack")]
public class HeroAttack : BaseAttack
{
    public override void Sub()
    {
        base.Sub();
        _creature.Animator.runtimeAnimatorController = _creature.Data.Animator;
        _creature.Data.SetDamage(((HeroData)_creature.Data).FirstModeDamageValue);
        _creature.Data.SetDamageType(((HeroData)_creature.Data).FirstModeDamageType);
    }
}
