using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackDollars : MonoBehaviour
{
    [SerializeField] private List<Dollar> _dollars;

    private const float PowerFallDollar = 3f;
    private const float DurationFallDollar = 0.6f;
    private const int NumsFallDollar = 1;

    private const float PowerFlightDollar = 2f;
    private const float DurationFlightDollar = 1f;
    private const int NumsFlightDollar = 1;

    public bool IsReady { get; private set; }

    private void Start()
    {
        IsReady = true;
    }

    private void Update()
    {
        if (_dollars.Count == 0)
            Destroy(gameObject);
    }

    public void StartMove(MoneyPoint moneyPoint)
    {
        foreach (var dollar in _dollars)
        {
            dollar.StartMoveHorizontalAnimation();

            dollar.transform.DOJump(dollar.GetTarget(), PowerFallDollar, NumsFallDollar, DurationFallDollar)
            .OnComplete(() =>
            {
                dollar.StartMoveVerticalAnimation();

                dollar.transform.DOJump(moneyPoint.transform.position, PowerFlightDollar, NumsFlightDollar, DurationFlightDollar)
                    .OnComplete(() =>
                    {
                        moneyPoint.AddDollar(dollar);
                        _dollars.Remove(dollar);
                    }
                    );
            }
            );
        }
    }

    public void ChangeReadyValue()
    {
        IsReady = false;
    }
}
