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
        _transform.rotation = Quaternion.Euler(50f, 0, 0);
    }

    private void Start()
    {
        _transform.localPosition = _health.GetComponent<Creature>().Data.HealthBarOffset;
        _transform.rotation = Quaternion.Euler(50f, 0, 0);
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
