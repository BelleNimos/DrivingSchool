using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioSource _open;
    [SerializeField] private AudioSource _close;
    [SerializeField] private Sound _sound;
    [SerializeField] private LanguageButton _languageButton;

    private bool _isActive;

    private void Awake()
    {
        _isActive = false;
    }

    public void EnableOrDisableSound()
    {
        if (_isActive == false)
        {
            _sound.PlayOpen();
            _languageButton.PlayOpen();
            _isActive = true;
            _open.Play();
        }
        else
        {
            _sound.PlayClose();
            _languageButton.PlayClose();
            _isActive = false;
            _close.Play();
        }
    }
}
