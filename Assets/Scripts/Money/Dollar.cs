using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Dollar : MonoBehaviour
{
    [SerializeField] private List<DollarPoint> _points;

    private Animator _animator;
    private MoneyPoint _moneyPoint;
    private int _index;

    private const string MoveHorizontal = "MoveHorizontal";
    private const string MoveVertical = "MoveVertical";
    private const string Idle = "Idle";

    private const float PowerFall = 1f;
    private const float DurationFall = 0.6f;
    private const int NumsFalls = 1;

    private const float PowerFlight = 1f;
    private const float DurationFlight = 1f;
    private const int NumFlights = 1;

    private const string DeleteText = "Delete";
    private const float Delay = 5f;

    public bool IsEnd { get; private set; }

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        IsEnd = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<MoneyPoint>(out MoneyPoint moneyPoint))
        {
            transform.SetParent(moneyPoint.transform, true);
            IsEnd = true;
            Invoke(DeleteText, Delay);
        }
    }

    private void Delete()
    {
        Destroy(gameObject);
    }

    private Vector3 GetTarget()
    {
        _index = Random.Range(0, _points.Count);

        return _points[_index].transform.position;
    }

    private void StartMoveHorizontalAnimation()
    {
        _animator.SetBool(MoveHorizontal, true);
        _animator.SetBool(MoveVertical, false);
        _animator.SetBool(Idle, false);
    }

    private void StartMoveVerticalAnimation()
    {
        _animator.SetBool(MoveVertical, true);
        _animator.SetBool(MoveHorizontal, false);
        _animator.SetBool(Idle, false);
    }

    private void StopMoveAnimation()
    {
        _animator.SetBool(Idle, true);
        _animator.SetBool(MoveHorizontal, false);
        _animator.SetBool(MoveVertical, false);
    }

    public void SetMoneyPoint(MoneyPoint moneyPoint)
    {
        _moneyPoint = moneyPoint;
    }

    public void StartMove()
    {
        StartMoveHorizontalAnimation();

        transform.DOJump(GetTarget(), PowerFall, NumsFalls, DurationFall)
            .SetLink(gameObject)
            .OnKill(() =>
            {
                StartMoveVerticalAnimation();

                transform.DOJump(_moneyPoint.transform.position, PowerFlight, NumFlights, DurationFlight)
                    .SetLink(gameObject)
                    .OnKill(() =>
                    {
                        StopMoveAnimation();
                        _moneyPoint.AddDollar();
                    }
                    );
            }
            );
    }
}