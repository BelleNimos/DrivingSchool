using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField] private List<ConePoint> _conePoints;

    private int _counter = 0;

    public bool IsReady { get; private set; }

    private void Start()
    {
        IsReady = false;
    }

    private void Update()
    {
        if (GetValueCounter() == _conePoints.Count)
            IsReady = true;
        else
            IsReady = false;
    }

    private int GetValueCounter()
    {
        _counter = 0;

        foreach (var conePoint in _conePoints)
            if (conePoint.IsFree == false)
                _counter++;

        return _counter;
    }
}
