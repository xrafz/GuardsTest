using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    [SerializeField]
    private AnimatorOverrideController _animatorOverride;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = _animatorOverride;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _animator.Play("Attack");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _animator.Play("Hit");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            _animator.Play("Walk");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _animator.Play("Cast");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _animator.Play("Death");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _animator.Play("Idle");
        }
    }
}
