using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/AlternativeHeroAttack")]
public class AlternativeHeroAttack : BaseAttack
{
    public override void Sub()
    {
        base.Sub();
        _creature.Animator.runtimeAnimatorController = ((HeroData)_creature.Data).SecondModeAnimator;
        _creature.Data.SetDamage(((HeroData)_creature.Data).SecondModeDamageValue);
        _creature.Data.SetDamageType(((HeroData)_creature.Data).SecondModeDamageType);
    }
}
