using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "Abilities/LoseOnDeath")]
public class LoseOnDeath : Ability
{
    private Creature _creature;
    private float _deathAnimationTime = 1f;

    public override void Init(MonoBehaviour mono)
    {
        _creature = mono.GetComponent<Creature>();
        var clips = _creature.Animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "Death")
            {
                _deathAnimationTime = clip.length + 0.5f;
            }
        }
    }

    public override void Sub()
    {
        Unsub();
        _creature.Health.OnDeath += Action;
    }

    public override void Unsub()
    {
        _creature.Health.OnDeath -= Action;
    }

    private void Action()
    {
        _creature.Animator.Play("Death");
        _creature.Transform.DOScale(1f, _deathAnimationTime + 0.3f).OnComplete(() =>
        {
            var manager = BattleHandler.Instance;
            manager.StartCoroutine(manager.DefeatHero((Hero)_creature));
        });
    }
}
