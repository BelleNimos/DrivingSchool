using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private AudioSource _openSound;
    [SerializeField] private AudioSource _closeSound;

    private Animator _animator;
    private bool _isActive;

    private const string Open = "Open";
    private const string Close = "Close";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _isActive = false;
    }

    public void EnableOrDisableSound()
    {
        if (_isActive == false)
        {
            _animator.SetTrigger(Open);
            _isActive = true;
            _openSound.Play();
        }
        else
        {
            _animator.SetTrigger(Close);
            _isActive = false;
            _closeSound.Play();
        }
    }
}
