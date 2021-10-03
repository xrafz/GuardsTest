using UnityEngine;
using DG.Tweening;

public class YourTurnNotification : MonoBehaviour
{
    [SerializeField]
    private Transform _notification;

    private void Awake()
    {
        BattleState.Instance.OnTurn += Notify;
    }

    private void Start()
    {
        _notification.DOScale(0f, 0f);
    }

    public void Notify()
    {
        _notification.DOScale(1f, 0.5f).OnComplete(() =>
        {
            _notification.DOScale(1f, 0.5f).OnComplete(() =>
            {
                _notification.DOScale(0f, 0.5f);
            });
        });
    }

}
