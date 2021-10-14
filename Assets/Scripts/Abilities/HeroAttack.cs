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
        HeroData data = (HeroData)_creature.Data;
        if (((Hero)_creature).InFirstMode)
        {
            _creature.Animator.runtimeAnimatorController = data.FirstModeAnimator;
            _creature.Data.SetDamage(data.FirstModeDamageValue);
            _creature.Data.SetDamageType(data.FirstModeDamageType);
        }
        else
        {
            _creature.Animator.runtimeAnimatorController = data.SecondModeAnimator;
            _creature.Data.SetDamage(data.SecondModeDamageValue);
            _creature.Data.SetDamageType(data.SecondModeDamageType);
        }
    }
}
