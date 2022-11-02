using System.Collections;
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

    public void StartMove()
    {
        foreach (var dollar in _dollars)
        {
            dollar.StartTurn();

            dollar.transform.DOJump(dollar.GetTarget(), PowerFallDollar, NumsFallDollar, DurationFallDollar)
            .OnComplete(() =>
            {
                dollar.transform.DOJump(dollar.GetCollectionPoint(), PowerFlightDollar, NumsFlightDollar, DurationFlightDollar)
                    .OnComplete(() =>
                    {
                        Destroy(dollar.gameObject);
                        _dollars.Remove(dollar);
                    }
                    );
            }
            );
        }
    }

    public void ChangeValue()
    {
        IsReady = false;
    }
}
