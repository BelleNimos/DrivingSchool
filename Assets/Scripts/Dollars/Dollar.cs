using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dollar : MonoBehaviour
{
    [SerializeField] private List<TargetMoney> _targetsMoney;
    [SerializeField] private float _x;
    [SerializeField] private float _y;
    [SerializeField] private float _z;

    private MoneyPoint _moneyPoint;
    private bool _isReady;
    private int _index;

    private void Update()
    {
        _moneyPoint = FindObjectOfType<MoneyPoint>();

        if (_isReady == true)
            transform.rotation *= Quaternion.Euler(_x, _y, _z);
    }

    public void StartTurn()
    {
        _isReady = true;
    }

    public Vector3 GetTarget()
    {
        _index = Random.Range(0, _targetsMoney.Count);

        return _targetsMoney[_index].transform.position;
    }

    public Vector3 GetCollectionPoint()
    {
        return _moneyPoint.transform.position;
    }
}