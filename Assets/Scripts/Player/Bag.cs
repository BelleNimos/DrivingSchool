using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bag : MonoBehaviour
{
    [SerializeField] private Transform _bagPosition;

    private Stack<Cone> _cones;

    private const float JumpPower = 2f;
    private const float Duration = 0.2f;
    private const int NumsJump = 1;

    public int MaxCones { get; private set; } = 12;
    public int CurrentCones => _cones.Count;

    private void Start()
    {
        _cones = new Stack<Cone>();
    }

    public void AddCone(Cone cone)
    {
        Vector3 nextPosition = new Vector3(0, 0.5f * CurrentCones, 0);

        cone.transform.DOJump((_bagPosition.position + nextPosition), JumpPower, NumsJump, Duration)
            .OnComplete(() =>
            {
                cone.transform.SetParent(_bagPosition, true);
                cone.transform.localPosition = nextPosition;
                cone.transform.localRotation = Quaternion.identity;
            }
            );
        
        _cones.Push(cone);
    }
}