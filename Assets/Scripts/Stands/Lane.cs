using System.Collections;
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
        _counter = 0;

        if (IsReady == true)
            foreach (var conePoint in _conePoints)
                conePoint.UnlockPhysics();
        else
            for (int i = 0; i < _conePoints.Count; i++)
                if (_conePoints[i].IsFree == false)
                    _counter++;

        if (_counter == _conePoints.Count)
            IsReady = true;
    }

    public void ResetState()
    {
        for (int i = 0; i < _conePoints.Count; i++)
            _conePoints[i].ResetConePosition();
    }
}
