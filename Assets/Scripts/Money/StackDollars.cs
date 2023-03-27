using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackDollars : MonoBehaviour
{
    [SerializeField] private List<Dollar> _dollars;

    private MoneyPoint _moneyPoint;

    public bool IsReady { get; private set; }

    private void Start()
    {
        IsReady = true;

        for (int i = 0; i < _dollars.Count; i++)
        {
            _dollars[i].DisableKinematic();
            _dollars[i].DisableTrigger();
        }
    }

    private IEnumerator DestroyThis()
    {
        while (_dollars.Count > 0)
        {
            if (_dollars[0] == null)
                _dollars.RemoveAt(0);

            yield return new WaitForSeconds(0.1f);
        }

        if (_dollars.Count == 0)
            Destroy(gameObject);
    }

    private IEnumerator StartMovement()
    {
        for (int i = 0; i < _dollars.Count; i++)
        {
            _dollars[i].SetMoneyPoint(_moneyPoint);
            _dollars[i].EnableTrigger();
            _dollars[i].EnableKinematic();
            _dollars[i].StartMove();

            yield return new WaitForSeconds(0.04f);
        }

        StartCoroutine(DestroyThis());
    }

    public void SetMoneyPoint(MoneyPoint moneyPoint)
    {
        _moneyPoint = moneyPoint;
    }

    public void StartMove()
    {
        IsReady = false;
        StartCoroutine(StartMovement());
    }
}
