using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private float _startSpeedAnimator = 1f;

    private const string Carry = "Carry";
    private const string Run = "Run";
    private const string CarryIdle = "Carry Idle";

    public float SpeedAnimator => _animator.speed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetDefaultValues()
    {
        _animator.speed = _startSpeedAnimator;
    }

    public void SetStartValues(float speedAnimator)
    {
        _animator.speed = speedAnimator;
    }

    public void IncreaseSpeedAnimator()
    {
        _animator.speed += 0.02f;
    }

    public void StartRunAnimation()
    {
        _animator.SetBool(Run, true);
        _animator.SetBool(Carry, false);
        _animator.SetBool(CarryIdle, false);
    }

    public void StartCarryAnimation()
    {
        _animator.SetBool(Carry, true);
        _animator.SetBool(Run, false);
        _animator.SetBool(CarryIdle, false);
    }

    public void StartIdleAnimation()
    {
        _animator.SetBool(Run, false);
        _animator.SetBool(Carry, false);
        _animator.SetBool(CarryIdle, false);
    }

    public void StartCarryIdleAnimation()
    {
        _animator.SetBool(CarryIdle, true);
        _animator.SetBool(Carry, false);
        _animator.SetBool(Run, false);
    }
}
