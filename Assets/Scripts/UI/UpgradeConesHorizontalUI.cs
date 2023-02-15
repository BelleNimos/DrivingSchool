using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UpgradeConesHorizontalUI : MonoBehaviour
{
    private Animator _animator;

    private const string Close = "Close";

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartAnimationClose()
    {
        _animator.SetTrigger(Close);
    }
}
