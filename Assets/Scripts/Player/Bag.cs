using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Animator))]
public class Bag : MonoBehaviour
{
    private Stack<Cone> _cones;
    private Animator _animator;

    private const float JumpPower = 0.1f;
    private const float Duration = 0.1f;
    private const float Distance = 0.7f;
    private const int NumJumps = 1;

    private const string Idle = "Idle";
    private const string Rocking = "Rocking";

    public int MaxConesCount { get; private set; } = 12;
    public int CurrentConesCount => _cones.Count;

    private void Start()
    {
        _cones = new Stack<Cone>();
        _animator = GetComponent<Animator>();
    }

    public void AddCone(Cone cone)
    {
        Vector3 nextPosition = new Vector3(0, Distance * CurrentConesCount, 0);

        cone.transform.DOJump((transform.position + nextPosition), JumpPower, NumJumps, Duration)
            .OnComplete(() =>
            {
                cone.transform.SetParent(transform, true);
                cone.transform.localPosition = nextPosition;
                cone.transform.localRotation = Quaternion.identity;
            }
            );

        _cones.Push(cone);
    }

    public Cone GetCone()
    {
        return _cones.Pop();
    }

    public void StartAnimation()
    {
        _animator.SetBool(Rocking, true);
        _animator.SetBool(Idle, false);
    }

    public void StopAnimation()
    {
        _animator.SetBool(Idle, true);
        _animator.SetBool(Rocking, false);
    }
}
