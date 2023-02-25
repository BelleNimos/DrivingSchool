using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Animator))]
public class Bag : MonoBehaviour
{
    private Stack<Cone> _cones;
    private Animator _animator;

    private const string RockingText = "Rocking";
    private const int NumJumps = 1;
    private const int SurplusFactor = 1;
    private const float JumpPower = 0.1f;
    private const float Duration = 0.1f;
    private const float Distance = 0.7f;

    public int MaxConesCount { get; private set; }
    public int CurrentConesCount => _cones.Count;
    public float SpeedAnimator => _animator.speed;

    private void Start()
    {
        _cones = new Stack<Cone>();
        _animator = GetComponent<Animator>();

        if (SceneData.SpeedAnimatorBag > 0)
            _animator.speed = SceneData.SpeedAnimatorBag;
        else
            _animator.speed = 1f;

        if (SceneData.CapacityBag > 0)
            MaxConesCount = SceneData.CapacityBag;
        else
            MaxConesCount = 12;
    }

    public void AddCone(Cone cone)
    {
        cone.ResetState();
        cone.BlockPhysics();

        Vector3 nextPosition = new Vector3(0, Distance * CurrentConesCount, 0);
        Vector3 nextRotation = new Vector3(0, 0, 0);

        cone.transform.DOJump((transform.position + nextPosition), JumpPower, NumJumps, Duration)
            .SetLink(cone.gameObject)
            .OnKill(() =>
            {
                cone.transform.SetParent(transform, true);
                cone.transform.localPosition = nextPosition;
                cone.transform.localRotation = Quaternion.LookRotation(nextRotation);
            }
            );

        cone.PlayAddSound();
        _cones.Push(cone);
    }

    public void GiveAwayCone(ConePoint conePoint)
    {
        if (_cones.Count > 0)
            conePoint.AddCone(_cones.Pop());
    }

    public void GiveAwayCone(Utilizer utilizer)
    {
        if (_cones.Count > 0)
            utilizer.DestroyCone(_cones.Pop());
    }

    public void StartAnimationRocking()
    {
        _animator.SetBool(RockingText, true);
    }

    public void StopAnimationRocking()
    {
        _animator.SetBool(RockingText, false);
    }

    public void IncreaseCapacity()
    {
        MaxConesCount += SurplusFactor;
    }

    public void IncreaseSpeedAnimation()
    {
        _animator.speed += 0.02f;
    }
}
