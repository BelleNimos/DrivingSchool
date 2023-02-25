using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private const string Carry = "Carry";
    private const string Run = "Run";
    private const string CarryIdle = "Carry Idle";

    public float SpeedAnimator => _animator.speed;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        if (SceneData.SpeedAnimatorPlayer > 0f)
            _animator.speed = SceneData.SpeedAnimatorPlayer;
        else
            _animator.speed = 1f;
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

    public void IncreaseSpeedAnimation()
    {
        _animator.speed += 0.02f;
    }
}
