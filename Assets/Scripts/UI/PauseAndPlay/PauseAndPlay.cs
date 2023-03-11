using UnityEngine;
using UnityEngine.UI;

public class PauseAndPlay : MonoBehaviour
{
    [SerializeField] private Image _pausePanel;
    [SerializeField] private AudioSource _pauseSound;
    [SerializeField] private AudioSource _playSound;
    [SerializeField] private Pause _pause;
    [SerializeField] private Play _play;

    private void Start()
    {
        _pause.gameObject.SetActive(true);
        _pausePanel.gameObject.SetActive(false);
    }

    public void Pause()
    {
        _pauseSound.Play();
        _play.gameObject.SetActive(true);
        _pause.gameObject.SetActive(false);
        _pausePanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Play()
    {
        _playSound.Play();
        _pause.gameObject.SetActive(true);
        _play.gameObject.SetActive(false);
        _pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
