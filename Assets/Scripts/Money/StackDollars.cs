using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackDollars : MonoBehaviour
{
    [SerializeField] private List<Dollar> _dollars;

    private MoneyPoint _moneyPoint;
    private WaitForSeconds _waitForSeconds;

    public bool IsUsed { get; private set; }

    private void Start()
    {
        _moneyPoint = FindObjectOfType<MoneyPoint>();
        _waitForSeconds = new WaitForSeconds(0.01f);
        IsUsed = false;
    }

    private void Update()
    {
        if (_dollars.Count == 0)
            Destroy(gameObject);

        for (int i = 0; i < _dollars.Count; i++)
            if (_dollars[i].IsEnd == true)
                _dollars.RemoveAt(i);
    }

    private IEnumerator StartMovement()
    {
        for (int i = 0; i < _dollars.Count; i++)
        {
            _dollars[i].SetMoneyPoint(_moneyPoint);
            _dollars[i].StartMove();

            yield return _waitForSeconds;
        }
    }

    public void StartMove()
    {
        IsUsed = true;
        StartCoroutine(StartMovement());
    }
}
