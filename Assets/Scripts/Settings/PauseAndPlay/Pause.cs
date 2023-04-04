using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private PlayAndPauseButtonsSwitcher _pauseAndPlay;
    [SerializeField] private AudioListener _audioListener;
    [SerializeField] private Image _pausePanel;

    private void OnEnable()
    {
        Time.timeScale = 1;
        _pausePanel.gameObject.SetActive(false);
        AudioListener.volume = 1f;
        AudioListener.pause = false;
    }
}
