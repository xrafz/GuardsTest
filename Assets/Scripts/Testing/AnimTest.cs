using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    [SerializeField]
    private AnimatorOverrideController _animatorOverride;
    [SerializeField]
    private GameObject _body;
    [SerializeField]
    private Texture _texture;
    private SkinnedMeshRenderer _renderer;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = _animatorOverride;
        /*
        var body = Instantiate(_body, gameObject.transform);
        _renderer = body.GetComponent<SkinnedMeshRenderer>();
        */
        _renderer = _body.GetComponent<SkinnedMeshRenderer>();
    }
    void Start()
    {
        //_renderer.sharedMaterial.mainTexture = _texture;
        _renderer.sharedMaterial = _renderer.material;
        _renderer.sharedMaterial.mainTexture = _texture;
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
