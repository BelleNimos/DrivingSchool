using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UpgradeConesHorizontalUI : MonoBehaviour
{
    [SerializeField] private AudioSource _departure;

    private Animator _animator;

    private const string Close = "Close";

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartAnimationClose()
    {
        _animator.SetTrigger(Close);
        _departure.Play();
    }
}
