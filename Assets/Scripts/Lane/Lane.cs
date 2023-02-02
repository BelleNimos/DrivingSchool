using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField] private List<ConePoint> _conePoints;

    private int _counter = 0;

    private const float Delay = 0.5f;
    private const string UnlockPhysics = "UnlockPhysics";

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

        if (IsReady == true)
            foreach (var conePoint in _conePoints)
                conePoint.Invoke(UnlockPhysics, Delay);
    }

    private int GetValueCounter()
    {
        _counter = 0;

        foreach (var conePoint in _conePoints)
            if (conePoint.IsFree == false && conePoint.CheckForCone() == false)
                _counter++;

        return _counter;
    }
}
