using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/MoveAndAttack")]
public class MoveAndAttack : MoveOrAttack
{
    protected override IEnumerator HandleMovement()
    {
        _passedSteps = 0;
        bool attackTriggered = false;
        for (int currentSteps = 0; currentSteps < _moveDistance; currentSteps++)
        {
            if (EnemiesInAttackRange() && !attackTriggered)
            {
                attackTriggered = true;
            }
            else
            {
                MakeStep();
            }
        }
        if (_passedSteps != 0)
        {
            var timeToWait = _passedSteps * 0.8f;
            _creature.Animator.SetTrigger("Walk");
            _creature.SetTurnTime(timeToWait);
            yield return new WaitForSeconds(timeToWait);
            _creature.Animator.SetTrigger("StopWalking");
            if (attackTriggered)
            {
                Attack();
            }
        }
    }
}
