using System.Collections.Generic;
using UnityEngine;

public class TargetsCar : MonoBehaviour
{
    [SerializeField] private List<Checkpoint> _checkpoints;

    public void AssignTarget(ref Transform transform)
    {
        int index = Random.Range(0, _checkpoints.Count);
        transform = _checkpoints[index].transform;
    }
}
