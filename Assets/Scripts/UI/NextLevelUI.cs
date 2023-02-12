using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NextLevelUI : MonoBehaviour
{
    private Animator _animator;

    private const string Open = "Open";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.SetTrigger(Open);
    }
}
