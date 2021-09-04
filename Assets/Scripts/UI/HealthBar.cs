using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image _bar;

    [SerializeField]
    private Health _health;

    private Transform _transform, _camera;
    private void Awake()
    {
        _health.OnChange += SetBar;
        _transform = transform;
        _camera = Camera.main.transform;
        
    }

    private void Update()
    {
        _transform.LookAt(_camera);
        _transform.rotation = Quaternion.Euler(_transform.rotation.x, 0, _transform.rotation.z);
    }

    public void SetBar()
    {
        var fillAmount = (float)_health.Current / (float)_health.Maximum;
        _bar.DOFillAmount(fillAmount, 0.5f);
    }

    private void OnDestroy()
    {
        _health.OnHit -= SetBar;
    }
}
